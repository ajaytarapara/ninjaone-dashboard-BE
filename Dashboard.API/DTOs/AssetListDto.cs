using System;
using System.Collections.Generic;

namespace Dashboard.API.DTOs
{
    public class AssetListDto
    {
        public int Id { get; set; }

        public int DeviceId { get; set; }

        public string DeviceName { get; set; } = string.Empty;

        public string DeviceType { get; set; } = string.Empty;

        public string OsPlatform { get; set; } = string.Empty;

        public string SerialNumber { get; set; } = string.Empty;

        public string Status { get; set; } = string.Empty;

        public string HealthStatus { get; set; } = string.Empty;

        public string? Organization { get; set; }

        public string? Location { get; set; }

        public string LastSeen { get; set; } = string.Empty;

        public string LastUser { get; set; } = string.Empty;

        public string LastLogon { get; set; } = string.Empty;

        public long LastContact { get; set; }

        public List<string> Tags { get; set; } = [];
    }
}