using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dashboard.API.Models
{
    public class TopAlertDeviceDto
    {
        public string DeviceName { get; set; } = string.Empty;

        public int AlertCount { get; set; }
    }
}