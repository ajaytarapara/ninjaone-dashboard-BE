using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dashboard.API.DTOs
{
    public class AlertsSummaryDto
    {
        public int TotalAlerts { get; set; }

        public int CriticalAlerts { get; set; }

        public int HighAlerts { get; set; }

        public int MediumAlerts { get; set; }

        public int LowAlerts { get; set; }

        public int RecentActivities { get; set; }
    }
}