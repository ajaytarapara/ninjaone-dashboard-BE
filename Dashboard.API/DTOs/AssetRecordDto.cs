using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dashboard.API.DTOs
{
    public class AssetRecordDto
    {
        public int DeviceId { get; set; }

        public string DeviceName { get; set; } = string.Empty;

        public string DeviceType { get; set; } = string.Empty;

        public string Status { get; set; } = string.Empty;

        public string HealthStatus { get; set; } = string.Empty;

        public string Organization { get; set; } = string.Empty;

        public string Location { get; set; } = string.Empty;

        public string LastContact { get; set; } = string.Empty;

        public List<string> Tags { get; set; } = [];
    }
}