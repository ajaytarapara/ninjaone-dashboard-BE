using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dashboard.API.DTOs
{
    public class OverviewMetricsDto
    {
        public int TotalDevices { get; set; }

        public int OnlineDevices { get; set; }

        public int OfflineDevices { get; set; }

        public int CriticalAlerts { get; set; }
    }
}