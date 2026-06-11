using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dashboard.API.Models;

namespace Dashboard.API.DTOs
{
    public class TicketsSummaryDto
    {
        public KpiMetricDto TotalOpenTickets { get; set; } = new();

        public KpiMetricDto CriticalTickets { get; set; } = new();

        public KpiMetricDto HighTickets { get; set; } = new();

        public KpiMetricDto MediumTickets { get; set; } = new();

        public KpiMetricDto LowTickets { get; set; } = new();

        public List<TicketPriorityDto> PriorityDistribution { get; set; } = [];

        public List<TicketStatusDto> StatusDistribution { get; set; } = [];

        public List<TicketRecord> RecentTickets { get; set; } = [];
    }

}