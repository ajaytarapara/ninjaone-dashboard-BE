using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dashboard.API.Models
{
    public class OsPatchInstallResponse
    {

    public Cursor? Cursor { get; set; }

    public List<OsPatchInstall> Results { get; set; } = [];
}
    
}