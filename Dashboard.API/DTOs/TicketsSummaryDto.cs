using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dashboard.API.DTOs
{
    public class TicketsSummaryDto
    {
        public int TotalOpenTickets { get; set; }

        public int CriticalTickets { get; set; }

        public int HighTickets { get; set; }

        public int MediumTickets { get; set; }

        public int LowTickets { get; set; }
    }
}