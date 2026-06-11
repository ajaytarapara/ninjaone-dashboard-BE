using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dashboard.API.DTOs
{
    public class KpiMetricDto
    {
        public int Value { get; set; }

        public decimal TrendPercentage { get; set; }
    }
}