using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dashboard.API.DTOs
{
    public class AntivirusSummaryDto
    {
        public int TotalManagedDevices { get; set; }

        public int DevicesWithAvInstalled { get; set; }

        public int ActiveProtectionDevices { get; set; }

        public int OutOfDateDefinitions { get; set; }

        public List<DeviceAvRecordDto> DeviceAvRecords { get; set; } = [];
    }
}