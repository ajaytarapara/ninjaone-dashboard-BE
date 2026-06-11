using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dashboard.API.Models;

namespace Dashboard.API.DTOs
{
    public class AntivirusStatusResponse
    {
        public Cursor? Cursor { get; set; }

        public List<AntivirusStatus> Results { get; set; } = [];
    }
}