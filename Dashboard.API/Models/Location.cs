using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dashboard.API.Models
{
    public class Location
    {
        public int Id { get; set; }

        public int OrganizationId { get; set; }

        public string? Name { get; set; }

        public string? Address { get; set; }
    }
}