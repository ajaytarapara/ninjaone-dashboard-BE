using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dashboard.API.Models
{
    public class ActivityResponse
    {
        public int LastActivityId { get; set; }

        public List<Activity> Activities { get; set; } = new();
    }
}