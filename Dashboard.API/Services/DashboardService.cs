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
            var alertsJson = await _ninjaOneClient.GetAlertsAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            var devices = await _ninjaOneClient.GetDevicesAsync();

            var alerts =
                JsonSerializer.Deserialize<List<Alert>>(
                    alertsJson,
                    options);
            var totalDevices = devices?.Count;

            var onlineDevices =
                devices?.Count(x => !x.Offline);

            var offlineDevices =
                devices?.Count(x => x.Offline);

            var criticalAlerts =
                alerts?.Count(x =>
                    x.Severity == "CRITICAL");

            return new OverviewMetricsDto
            {
                TotalDevices = totalDevices ?? 0,
                OnlineDevices = onlineDevices ?? 0,
                OfflineDevices = offlineDevices ?? 0,
                CriticalAlerts = criticalAlerts ?? 0
            };
        }

        public async Task<List<AssetListDto>> GetAssetListAsync()
        {
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };


            var organizationsJson = await _ninjaOneClient.GetOrganizationsAsync();
            var locationsJson = await _ninjaOneClient.GetLocationsAsync();
            var devices = await _ninjaOneClient.GetDevicesAsync();

            var organizations =
                JsonSerializer.Deserialize<List<Organization>>(organizationsJson, options)
                ?? new List<Organization>();

            var locations =
                JsonSerializer.Deserialize<List<Location>>(locationsJson, options)
                ?? new List<Location>();

            var result = devices.Select(device =>
            {
                var organization = organizations
                    .FirstOrDefault(x => x.Id == device.OrganizationId);

                var location = locations
                    .FirstOrDefault(x => x.Id == device.LocationId);

                return new AssetListDto
                {
                    DeviceId = device.Id,
                    DeviceName = device.SystemName,
                    Organization = organization?.Name,
                    Location = location?.Name,
                    Status = device.Offline ? "Offline" : "Online"
                };
            }).ToList();

            return result;
        }

        public async Task<AlertsSummaryDto> GetAlertsSummaryAsync()
        {
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            var alertsJson = await _ninjaOneClient.GetAlertsAsync();
            var activitiesJson = await _ninjaOneClient.GetActivitiesAsync();

            var alerts =
                JsonSerializer.Deserialize<List<Alert>>(alertsJson, options)
                ?? new List<Alert>();

            var activities =
                JsonSerializer.Deserialize<ActivityResponse>(activitiesJson, options)
                ?? new ActivityResponse();

            return new AlertsSummaryDto
            {
                TotalAlerts = alerts.Count,

                CriticalAlerts = alerts.Count(x =>
                    string.Equals(
                        x.Severity,
                        "CRITICAL",
                        StringComparison.OrdinalIgnoreCase)),

                HighAlerts = alerts.Count(x =>
                    string.Equals(
                        x.Severity,
                        "HIGH",
                        StringComparison.OrdinalIgnoreCase)),

                MediumAlerts = alerts.Count(x =>
                    string.Equals(
                        x.Severity,
                        "MEDIUM",
                        StringComparison.OrdinalIgnoreCase)),

                LowAlerts = alerts.Count(x =>
                    string.Equals(
                        x.Severity,
                        "LOW",
                        StringComparison.OrdinalIgnoreCase)),

                RecentActivities = activities.Activities.Count
            };
        }
        public async Task<PatchSummaryDto> GetPatchSummaryAsync()
        {
            var patches = await _ninjaOneClient.GetOsPatchInstallsAsync();

            return new PatchSummaryDto
            {
                TotalPatches = patches.Count,

                InstalledPatches = patches.Count(p =>
                    p.Status.Equals("Installed",
                    StringComparison.OrdinalIgnoreCase)),

                PendingPatches = patches.Count(p =>
                    p.Status.Equals("Pending",
                    StringComparison.OrdinalIgnoreCase)),

                FailedPatches = patches.Count(p =>
                    p.Status.Equals("Failed",
                    StringComparison.OrdinalIgnoreCase)),

                CriticalPatches = patches.Count(p =>
                    p.Severity.Equals("Critical",
                    StringComparison.OrdinalIgnoreCase))
            };
        }

        public async Task<AntivirusSummaryDto> GetAntivirusSummaryAsync()
        {
            var devices = await _ninjaOneClient.GetDevicesAsync();

            var avStatuses = await _ninjaOneClient.GetAntivirusStatusAsync();

            var deviceRecords =
                from av in avStatuses
                join d in devices
                    on av.DeviceId equals d.Id
                select new DeviceAvRecordDto
                {
                    DeviceName = d.SystemName,
                    ProductName = av.ProductName,
                    ProductState = av.ProductState,
                    DefinitionStatus = av.DefinitionStatus,
                    Version = av.Version
                };

            return new AntivirusSummaryDto
            {
                TotalManagedDevices = devices.Count,

                DevicesWithAvInstalled = avStatuses.Count,

                ActiveProtectionDevices = avStatuses.Count(x =>
                    x.ProductState.Equals(
                        "ACTIVE",
                        StringComparison.OrdinalIgnoreCase)),

                OutOfDateDefinitions = avStatuses.Count(x =>
                    x.DefinitionStatus.Equals(
                        "OUT_OF_DATE",
                        StringComparison.OrdinalIgnoreCase)),

                DeviceAvRecords = deviceRecords.ToList()
            };
        }

        public async Task<TicketsSummaryDto>
    GetTicketsSummaryAsync()
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

            var openTickets =
                result.Data.Where(x =>
                    x.Status.Equals(
                        "Open",
                        StringComparison.OrdinalIgnoreCase));

            return new TicketsSummaryDto
            {
                TotalOpenTickets =
                    openTickets.Count(),

                CriticalTickets =
                    openTickets.Count(x =>
                        x.Priority == "Critical"),

                HighTickets =
                    openTickets.Count(x =>
                        x.Priority == "High"),

                MediumTickets =
                    openTickets.Count(x =>
                        x.Priority == "Medium"),

                LowTickets =
                    openTickets.Count(x =>
                        x.Priority == "Low")
            };
        }
    }
}