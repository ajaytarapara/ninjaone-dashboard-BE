using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dashboard.API.DTOs
{
    public class TicketStatusDto
    {
        public string Status { get; set; } = string.Empty;

        public int Count { get; set; }

        public decimal Percentage { get; set; }
    }
}