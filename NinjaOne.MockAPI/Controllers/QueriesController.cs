using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NinjaOne.MockAPI.Services;

namespace NinjaOne.MockAPI.Controllers
{
    [ApiController]
    [Route("v2/queries")]
    public class QueriesController : ControllerBase
    {
        private readonly MockDataService _mockDataService;

        public QueriesController(MockDataService mockDataService)
        {
            _mockDataService = mockDataService;
        }

        [HttpGet("antivirus-status")]
        public IActionResult AntivirusStatus()
        {
            return Content(
             _mockDataService.GetJson("antivirus-status.json"),
             "application/json");
        }

        [HttpGet("software")]
        public IActionResult Software()
        {
            return Content(
             _mockDataService.GetJson("software.json"),
             "application/json");
        }

        [HttpGet("os-patch-installs")]
        public IActionResult PatchInstalls()
        {
            return Content(
            _mockDataService.GetJson("os-patch-installs.json"),
            "application/json");
        }
    }
}