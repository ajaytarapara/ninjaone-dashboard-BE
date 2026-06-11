using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dashboard.API.Models
{
    public class AntivirusStatus
    {
        public string ProductName { get; set; } = string.Empty;

        public string ProductState { get; set; } = string.Empty;

        public string DefinitionStatus { get; set; } = string.Empty;

        public string Version { get; set; } = string.Empty;

        public int DeviceId { get; set; }

        public long Timestamp { get; set; }
    }
}