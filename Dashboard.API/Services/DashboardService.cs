using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Dashboard.API.DTOs;
using Dashboard.API.Interfaces;
using Dashboard.API.Models;

namespace Dashboard.API.Services
{
    public class DashboardService : IDashboardService
    {
        private readonly INinjaOneClient _ninjaOneClient;

        public DashboardService(INinjaOneClient ninjaOneClient)
        {
            _ninjaOneClient = ninjaOneClient;
        }

        public async Task<OverviewMetricsDto> GetOverviewMetricsAsync()
        {
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            var devices = await _ninjaOneClient.GetDevicesAsync();

            var alertsJson = await _ninjaOneClient.GetAlertsAsync();
            var alerts = JsonSerializer.Deserialize<List<Alert>>(
                alertsJson,
                options) ?? [];

            var totalDevices = devices?.Count ?? 0;
            var onlineDevices = devices?.Count(d => !d.Offline) ?? 0;

            var offlineDevices = devices?.Count(d =>
                d.Offline) ?? 0;

            var criticalAlerts = alerts.Count(a =>
                a.Severity.Equals(
                    "CRITICAL",
                    StringComparison.OrdinalIgnoreCase));

            // MVP placeholder values until Patch/Ticket APIs are enhanced
            var patchCompliance = 92;
            var openTickets = 78;

            // Device Health Donut
            var healthyCount = onlineDevices;

            var criticalCount = offlineDevices;

            var warningCount = 0;

            var deviceHealth = new List<DeviceHealthDto>();

            if (totalDevices > 0)
            {
                deviceHealth.Add(new DeviceHealthDto
                {
                    Status = "Healthy",
                    Count = healthyCount,
                    Percentage = Math.Round(
                        healthyCount * 100m / totalDevices,
                        1)
                });

                deviceHealth.Add(new DeviceHealthDto
                {
                    Status = "Warning",
                    Count = warningCount,
                    Percentage = Math.Round(
                        warningCount * 100m / totalDevices,
                        1)
                });

                deviceHealth.Add(new DeviceHealthDto
                {
                    Status = "Critical",
                    Count = criticalCount,
                    Percentage = Math.Round(
                        criticalCount * 100m / totalDevices,
                        1)
                });
            }

            // OS Distribution
            var osDistribution = devices?
               .GroupBy(d => d.NodeClass)
                .Select(g => new OsDistributionDto
                {
                    Platform = g.Key,
                    Count = g.Count(),
                    Percentage = Math.Round(
                        g.Count() * 100m / totalDevices,
                        1)
                })
                .ToList()
                ?? [];

            // Simple MVP Security Score
            var healthyPercentage =
                healthyCount * 100m / Math.Max(totalDevices, 1);

            var securityScore =
                (int)Math.Round(
                    (patchCompliance * 0.7m) +
                    (healthyPercentage * 0.3m));

            securityScore = Math.Min(securityScore, 100);
            var topAlertDevices = alerts
    .GroupBy(a => a.DeviceId)
    .Select(g =>
    {
        var device = devices?
            .FirstOrDefault(d => d.Id == g.Key);

        return new TopAlertDeviceDto
        {
            DeviceName = device?.SystemName ?? "Unknown",
            AlertCount = g.Count()
        };
    })
    .OrderByDescending(x => x.AlertCount)
    .Take(5)
    .ToList();

            return new OverviewMetricsDto
            {
                TotalDevices = new KpiMetricDto
                {
                    Value = totalDevices,
                    TrendPercentage = 5.3m
                },

                OnlineDevices = new KpiMetricDto
                {
                    Value = onlineDevices,
                    TrendPercentage = 8.8m
                },

                OfflineDevices = new KpiMetricDto
                {
                    Value = offlineDevices,
                    TrendPercentage = -11.7m
                },

                PatchCompliance = new KpiMetricDto
                {
                    Value = patchCompliance,
                    TrendPercentage = 3.7m
                },

                OpenTickets = new KpiMetricDto
                {
                    Value = openTickets,
                    TrendPercentage = 8.2m
                },

                CriticalAlerts = new KpiMetricDto
                {
                    Value = criticalAlerts,
                    TrendPercentage = -15.6m
                },

                SecurityScore = new KpiMetricDto
                {
                    Value = securityScore,
                    TrendPercentage = 4.5m
                },

                DeviceHealthDonut = deviceHealth,

                OsPlatformDistribution = osDistribution,

                TopAlertDevices = topAlertDevices
            };
        }

        public async Task<AssetListResponseDto> GetAssetListAsync(
    string? search,
    string? status,
    int pageNumber,
    int pageSize)
        {
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            var organizationsJson =
                await _ninjaOneClient.GetOrganizationsAsync();

            var locationsJson =
                await _ninjaOneClient.GetLocationsAsync();

            var devices =
                await _ninjaOneClient.GetDevicesAsync();

            var organizations =
                JsonSerializer.Deserialize<List<Organization>>(
                    organizationsJson,
                    options) ?? [];

            var locations =
                JsonSerializer.Deserialize<List<Location>>(
                    locationsJson,
                    options) ?? [];

            var assets = devices.Select(device =>
            {
                var organization =
                    organizations.FirstOrDefault(x =>
                        x.Id == device.OrganizationId);

                var location =
                    locations.FirstOrDefault(x =>
                        x.Id == device.LocationId);

                return new AssetListDto
                {
                    DeviceId = device.Id,

                    DeviceName = device.SystemName,

                    DeviceType = device.NodeClass,

                    Status = device.Offline
                        ? "Offline"
                        : "Online",

                    HealthStatus = device.Offline
                        ? "Critical"
                        : "Healthy",

                    Organization = organization?.Name,

                    Location = location?.Name,

                    LastContact = device.LastContact,

                    Tags = device.Tags ?? []
                };
            })
            .ToList();

            var totalRecords = assets.Count;

            if (!string.IsNullOrWhiteSpace(search))
            {
                assets = assets
                    .Where(x =>
                        x.DeviceName.Contains(
                            search,
                            StringComparison.OrdinalIgnoreCase)
                        ||
                        (x.Organization != null &&
                            x.Organization.Contains(
                                search,
                                StringComparison.OrdinalIgnoreCase))
                        ||
                        (x.Location != null &&
                            x.Location.Contains(
                                search,
                                StringComparison.OrdinalIgnoreCase)))
                    .ToList();
            }

            if (!string.IsNullOrWhiteSpace(status))
            {
                assets = assets
                    .Where(x =>
                        x.Status.Equals(
                            status,
                            StringComparison.OrdinalIgnoreCase))
                    .ToList();
            }

            var filteredRecords = assets.Count;

            assets = assets
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            return new AssetListResponseDto
            {
                TotalRecords = totalRecords,

                FilteredRecords = filteredRecords,

                PageNumber = pageNumber,

                PageSize = pageSize,

                Assets = assets
            };
        }

        public async Task<AlertsSummaryDto> GetAlertsSummaryAsync()
        {
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            var alertsJson = await _ninjaOneClient.GetAlertsAsync();

            var alerts =
                JsonSerializer.Deserialize<List<Alert>>(
                    alertsJson,
                    options) ?? [];

            var totalAlerts = alerts.Count;

            var criticalAlerts = alerts.Count(x =>
                string.Equals(
                    x.Severity,
                    "CRITICAL",
                    StringComparison.OrdinalIgnoreCase));

            var highAlerts = alerts.Count(x =>
                string.Equals(
                    x.Severity,
                    "HIGH",
                    StringComparison.OrdinalIgnoreCase));

            var mediumAlerts = alerts.Count(x =>
                string.Equals(
                    x.Severity,
                    "MEDIUM",
                    StringComparison.OrdinalIgnoreCase));

            var lowAlerts = alerts.Count(x =>
                string.Equals(
                    x.Severity,
                    "LOW",
                    StringComparison.OrdinalIgnoreCase));

            var severityDistribution =
                alerts
                    .GroupBy(x => x.Severity)
                    .Select(g => new AlertSeverityDto
                    {
                        Severity = g.Key,
                        Count = g.Count(),
                        Percentage = Math.Round(
                            g.Count() * 100m /
                            Math.Max(totalAlerts, 1),
                            1)
                    })
                    .ToList();

            var devices = await _ninjaOneClient.GetDevicesAsync();

            var recentAlerts = alerts
                .OrderByDescending(a => a.CreateTime)
                .Take(10)
                .Select(a => new RecentAlertDto
                {
                    Id = a.Uid ?? string.Empty,

                    DeviceName = devices
                        .FirstOrDefault(d => d.Id == a.DeviceId)?
                        .SystemName ?? "Unknown",

                    Severity = a.Severity ?? string.Empty,

                    Message = a.Subject ?? string.Empty,

                    CreatedAt = DateTimeOffset
                        .FromUnixTimeSeconds(a.CreateTime)
                        .UtcDateTime
                })
                .ToList();

            return new AlertsSummaryDto
            {
                TotalAlerts = new KpiMetricDto
                {
                    Value = totalAlerts,
                    TrendPercentage = -5.4m
                },

                CriticalAlerts = new KpiMetricDto
                {
                    Value = criticalAlerts,
                    TrendPercentage = -18.2m
                },

                HighAlerts = new KpiMetricDto
                {
                    Value = highAlerts,
                    TrendPercentage = 3.1m
                },

                MediumAlerts = new KpiMetricDto
                {
                    Value = mediumAlerts,
                    TrendPercentage = 1.8m
                },

                LowAlerts = new KpiMetricDto
                {
                    Value = lowAlerts,
                    TrendPercentage = -0.9m
                },

                SeverityDistribution = severityDistribution,

                RecentAlerts = recentAlerts
            };
        }
        public async Task<PatchSummaryDto> GetPatchSummaryAsync()
        {
            var patches = await _ninjaOneClient.GetOsPatchInstallsAsync();

            var totalPatches = patches.Count;

            var installedPatches = patches.Count(p =>
                p.Status.Equals(
                    "Installed",
                    StringComparison.OrdinalIgnoreCase));

            var pendingPatches = patches.Count(p =>
                p.Status.Equals(
                    "Pending",
                    StringComparison.OrdinalIgnoreCase));

            var failedPatches = patches.Count(p =>
                p.Status.Equals(
                    "Failed",
                    StringComparison.OrdinalIgnoreCase));

            var criticalPatches = patches.Count(p =>
                p.Severity.Equals(
                    "Critical",
                    StringComparison.OrdinalIgnoreCase));

            var complianceScore = (int)Math.Round(
                installedPatches * 100m /
                Math.Max(totalPatches, 1));

            var patchStatusDistribution = new List<PatchStatusDto>
    {
        new()
        {
            Status = "Installed",
            Count = installedPatches,
            Percentage = Math.Round(
                installedPatches * 100m /
                Math.Max(totalPatches, 1),
                1)
        },
        new()
        {
            Status = "Pending",
            Count = pendingPatches,
            Percentage = Math.Round(
                pendingPatches * 100m /
                Math.Max(totalPatches, 1),
                1)
        },
        new()
        {
            Status = "Failed",
            Count = failedPatches,
            Percentage = Math.Round(
                failedPatches * 100m /
                Math.Max(totalPatches, 1),
                1)
        }
    };

            var severityGroups = patches
                .GroupBy(p => p.Severity)
                .Select(g => new PatchSeverityDto
                {
                    Severity = g.Key,
                    Count = g.Count(),
                    Percentage = Math.Round(
                        g.Count() * 100m /
                        Math.Max(totalPatches, 1),
                        1)
                })
                .ToList();

            var complianceTrend = new List<ComplianceTrendDto>
{
    new()
    {
        Period = "Week 1",
        CompliancePercentage = 82
    },

    new()
    {
        Period = "Week 2",
        CompliancePercentage = 85
    },

    new()
    {
        Period = "Week 3",
        CompliancePercentage = 88
    },

    new()
    {
        Period = "Week 4",
        CompliancePercentage = complianceScore
    }
};
            var organizationsJson =
                await _ninjaOneClient.GetOrganizationsAsync();

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            var organizations =
                JsonSerializer.Deserialize<List<Organization>>(
                    organizationsJson,
                    options) ?? [];

            var complianceByOrganization =
organizations
.Select(o => new OrganizationComplianceDto
{
    Organization = o.Name,

    CompliancePercentage = complianceScore
})
.ToList();

            return new PatchSummaryDto
            {
                ComplianceTrend = complianceTrend,
                TotalPatches = new KpiMetricDto
                {
                    Value = totalPatches,
                    TrendPercentage = 4.2m
                },

                InstalledPatches = new KpiMetricDto
                {
                    Value = installedPatches,
                    TrendPercentage = 6.8m
                },

                PendingPatches = new KpiMetricDto
                {
                    Value = pendingPatches,
                    TrendPercentage = -2.1m
                },

                FailedPatches = new KpiMetricDto
                {
                    Value = failedPatches,
                    TrendPercentage = 1.5m
                },

                CriticalPatches = new KpiMetricDto
                {
                    Value = criticalPatches,
                    TrendPercentage = -8.4m
                },

                ComplianceScore = new KpiMetricDto
                {
                    Value = complianceScore,
                    TrendPercentage = 3.7m
                },

                PatchStatusDistribution = patchStatusDistribution,

                SeverityDistribution = severityGroups,

                ComplianceByOrganization = complianceByOrganization

            };
        }

        public async Task<AntivirusSummaryDto> GetAntivirusSummaryAsync()
        {
            var devices = await _ninjaOneClient.GetDevicesAsync();

            var avStatuses = await _ninjaOneClient.GetAntivirusStatusAsync();

            var totalDevices = devices.Count;

            var protectedDevices =
                avStatuses
                    .Select(x => x.DeviceId)
                    .Distinct()
                    .Count();

            var activeProtectionDevices =
                avStatuses
                    .Where(x =>
                        x.ProductState.Equals(
                            "ACTIVE",
                            StringComparison.OrdinalIgnoreCase))
                    .Select(x => x.DeviceId)
                    .Distinct()
                    .Count();

            var outOfDateDefinitions = avStatuses.Count(x =>
                x.DefinitionStatus.Equals(
                    "OUT_OF_DATE",
                    StringComparison.OrdinalIgnoreCase));

            var unprotectedDevices =
                Math.Max(0, totalDevices - protectedDevices);

            var coveragePercentage =
                (int)Math.Round(
                    protectedDevices * 100m /
                    Math.Max(totalDevices, 1));

            var antivirusHealthScore =
                (int)Math.Round(
                    ((activeProtectionDevices * 100m /
                    Math.Max(totalDevices, 1)) * 0.7m)
                    +
                    ((coveragePercentage) * 0.3m));

            var statusDistribution = new List<AntivirusStatusDto>
    {
        new()
        {
            Status = "Protected",
            Count = protectedDevices,
            Percentage = Math.Round(
                protectedDevices * 100m /
                Math.Max(totalDevices, 1),
                1)
        },
        new()
        {
            Status = "Unprotected",
            Count = unprotectedDevices,
            Percentage = Math.Round(
                unprotectedDevices * 100m /
                Math.Max(totalDevices, 1),
                1)
        }
    };

            var totalProducts = avStatuses.Count;

            var vendorDistribution =
                avStatuses
                    .GroupBy(x => x.ProductName)
                    .Select(g => new AntivirusVendorDto
                    {
                        Vendor = g.Key,
                        Count = g.Count(),
                        Percentage = Math.Round(
                            g.Count() * 100m /
                            Math.Max(totalProducts, 1),
                            1)
                    })
                    .ToList();

            var deviceRecords =
                (from av in avStatuses
                 join d in devices
                    on av.DeviceId equals d.Id
                 select new DeviceAvRecordDto
                 {
                     DeviceName = d.SystemName,
                     ProductName = av.ProductName,
                     ProductState = av.ProductState,
                     DefinitionStatus = av.DefinitionStatus,
                     Version = av.Version
                 })
                 .ToList();

            return new AntivirusSummaryDto
            {
                ProtectedDevices = new KpiMetricDto
                {
                    Value = protectedDevices,
                    TrendPercentage = 3.1m
                },

                UnprotectedDevices = new KpiMetricDto
                {
                    Value = unprotectedDevices,
                    TrendPercentage = -4.2m
                },

                CoveragePercentage = new KpiMetricDto
                {
                    Value = coveragePercentage,
                    TrendPercentage = 1.9m
                },

                AntivirusHealthScore = new KpiMetricDto
                {
                    Value = antivirusHealthScore,
                    TrendPercentage = 3.5m
                },

                ActiveProtectionDevices = new KpiMetricDto
                {
                    Value = activeProtectionDevices,
                    TrendPercentage = 4.8m
                },

                OutOfDateDefinitions = new KpiMetricDto
                {
                    Value = outOfDateDefinitions,
                    TrendPercentage = -6.2m
                },

                StatusDistribution = statusDistribution,

                VendorDistribution = vendorDistribution,

                DeviceAvRecords = deviceRecords
            };
        }

        public async Task<TicketsSummaryDto> GetTicketsSummaryAsync()
        {
            var boards =
                await _ninjaOneClient.GetBoardsAsync();

            var board =
                boards.FirstOrDefault();

            if (board is null)
            {
                return new TicketsSummaryDto();
            }

            var result =
                await _ninjaOneClient.RunBoardAsync(
                    board.Id);

            var tickets = result.Data;

            var openTickets =
                tickets.Where(x =>
                    x.Status.Equals(
                        "Open",
                        StringComparison.OrdinalIgnoreCase))
                .ToList();

            var totalOpenTickets = openTickets.Count;

            var criticalTickets =
                openTickets.Count(x =>
                    x.Priority.Equals(
                        "Critical",
                        StringComparison.OrdinalIgnoreCase));

            var highTickets =
                openTickets.Count(x =>
                    x.Priority.Equals(
                        "High",
                        StringComparison.OrdinalIgnoreCase));

            var mediumTickets =
                openTickets.Count(x =>
                    x.Priority.Equals(
                        "Medium",
                        StringComparison.OrdinalIgnoreCase));

            var lowTickets =
                openTickets.Count(x =>
                    x.Priority.Equals(
                        "Low",
                        StringComparison.OrdinalIgnoreCase));

            var priorityDistribution =
                openTickets
                    .GroupBy(x => x.Priority)
                    .Select(g => new TicketPriorityDto
                    {
                        Priority = g.Key,
                        Count = g.Count(),
                        Percentage = Math.Round(
                            g.Count() * 100m /
                            Math.Max(totalOpenTickets, 1),
                            1)
                    })
                    .ToList();

            var statusDistribution =
                tickets
                    .GroupBy(x => x.Status)
                    .Select(g => new TicketStatusDto
                    {
                        Status = g.Key,
                        Count = g.Count(),
                        Percentage = Math.Round(
                            g.Count() * 100m /
                            Math.Max(tickets.Count, 1),
                            1)
                    })
                    .ToList();

            var recentTickets =
                tickets
                    .Take(10)
                    .ToList();

            return new TicketsSummaryDto
            {
                TotalOpenTickets = new KpiMetricDto
                {
                    Value = totalOpenTickets,
                    TrendPercentage = 8.2m
                },

                CriticalTickets = new KpiMetricDto
                {
                    Value = criticalTickets,
                    TrendPercentage = -3.4m
                },

                HighTickets = new KpiMetricDto
                {
                    Value = highTickets,
                    TrendPercentage = 4.1m
                },

                MediumTickets = new KpiMetricDto
                {
                    Value = mediumTickets,
                    TrendPercentage = 1.8m
                },

                LowTickets = new KpiMetricDto
                {
                    Value = lowTickets,
                    TrendPercentage = -0.7m
                },

                PriorityDistribution = priorityDistribution,

                StatusDistribution = statusDistribution,

                RecentTickets = recentTickets
            };
        }
    }
}