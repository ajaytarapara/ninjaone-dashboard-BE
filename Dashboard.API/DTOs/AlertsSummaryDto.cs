using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dashboard.API.DTOs
{
    public class AlertsSummaryDto
    {
        public KpiMetricDto TotalAlerts { get; set; } = new();

        public KpiMetricDto CriticalAlerts { get; set; } = new();

        public KpiMetricDto HighAlerts { get; set; } = new();

        public KpiMetricDto MediumAlerts { get; set; } = new();

        public KpiMetricDto LowAlerts { get; set; } = new();

        public List<AlertSeverityDto> SeverityDistribution { get; set; } = [];
    }
}