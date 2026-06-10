using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dashboard.API.Models
{
    public class Activity
    {
        public int Id { get; set; }

        public string? Severity { get; set; }

        public string? Subject { get; set; }
    }
}