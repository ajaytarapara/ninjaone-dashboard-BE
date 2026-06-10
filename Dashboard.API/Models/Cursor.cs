using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dashboard.API.Models
{
    public class Cursor
    {
        public string Name { get; set; } = string.Empty;

        public int Offset { get; set; }

        public int Count { get; set; }

        public long Expires { get; set; }
    }
}