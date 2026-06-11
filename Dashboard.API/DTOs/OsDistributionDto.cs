using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dashboard.API.DTOs
{
    public class OsDistributionDto
    {
        public string Platform { get; set; } = string.Empty;

        public int Count { get; set; }

        public decimal Percentage { get; set; }
    }
}