using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dashboard.API.DTOs
{
    public class AssetListDto
    {
        public int DeviceId { get; set; }

        public string? DeviceName { get; set; }

        public string? Organization { get; set; }

        public string? Location { get; set; }

        public string Status { get; set; } = string.Empty;
    }
}