using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dashboard.API.DTOs
{
    public class ComplianceTrendDto
    {

        public string Period { get; set; } = string.Empty;

        public decimal CompliancePercentage { get; set; }
    }
}