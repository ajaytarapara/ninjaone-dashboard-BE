using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dashboard.API.Models
{
    public class TicketBoardRunResponse
    {
        public List<TicketRecord> Data { get; set; } = [];
    }
}