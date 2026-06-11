using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dashboard.API.DTOs
{
    public class RecentAlertDto
    {
        public string Id { get; set; } = string.Empty;

        public string DeviceName { get; set; } = string.Empty;

        public string Severity { get; set; } = string.Empty;

        public string Message { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; }
    }
}