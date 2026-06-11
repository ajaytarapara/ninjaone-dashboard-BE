using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dashboard.API.DTOs
{
    public class RecentActivityDto
    {
        public string Id { get; set; } = string.Empty;

        public string DeviceName { get; set; } = string.Empty;

        public string Action { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; }
    }
}