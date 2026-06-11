using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dashboard.API.DTOs
{
    public class AntivirusSummaryDto
    {
        // KPI Cards

        public KpiMetricDto ProtectedDevices { get; set; } = new();

        public KpiMetricDto UnprotectedDevices { get; set; } = new();

        public KpiMetricDto CoveragePercentage { get; set; } = new();

        public KpiMetricDto AntivirusHealthScore { get; set; } = new();

        public KpiMetricDto ActiveProtectionDevices { get; set; } = new();

        public KpiMetricDto OutOfDateDefinitions { get; set; } = new();

        // Charts

        public List<AntivirusStatusDto> StatusDistribution { get; set; } = [];

        public List<AntivirusVendorDto> VendorDistribution { get; set; } = [];

        // Table Widget

        public List<DeviceAvRecordDto> DeviceAvRecords { get; set; } = [];
    }
}