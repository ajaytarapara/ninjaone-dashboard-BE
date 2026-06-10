using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dashboard.API.DTOs
{
    public class DeviceAvRecordDto
    {
        public string DeviceName { get; set; } = string.Empty;

        public string ProductName { get; set; } = string.Empty;

        public string ProductState { get; set; } = string.Empty;

        public string DefinitionStatus { get; set; } = string.Empty;

        public string Version { get; set; } = string.Empty;
    }
}