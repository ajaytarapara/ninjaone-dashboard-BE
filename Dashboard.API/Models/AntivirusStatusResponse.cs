using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dashboard.API.Models
{
    public class AntivirusStatusResponse
    {

        public Cursor? Cursor { get; set; }

        public List<AntivirusStatus> Results { get; set; } = [];
    }
}