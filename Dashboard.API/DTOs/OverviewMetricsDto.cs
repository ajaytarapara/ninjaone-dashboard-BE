using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dashboard.API.Models;

namespace Dashboard.API.DTOs
{
    public class OverviewMetricsDto
    {
        public KpiMetricDto TotalDevices { get; set; } = new();

        public KpiMetricDto OnlineDevices { get; set; } = new();

        public KpiMetricDto OfflineDevices { get; set; } = new();

        public KpiMetricDto CriticalAlerts { get; set; } = new();

        public KpiMetricDto PatchCompliance { get; set; } = new();

        public KpiMetricDto OpenTickets { get; set; } = new();

        public KpiMetricDto SecurityScore { get; set; } = new();

        public List<DeviceHealthDto> DeviceHealthDonut { get; set; } = [];

        public List<OsDistributionDto> OsPlatformDistribution { get; set; } = [];

        public List<TopAlertDeviceDto> TopAlertDevices { get; set; } = [];
    }
}