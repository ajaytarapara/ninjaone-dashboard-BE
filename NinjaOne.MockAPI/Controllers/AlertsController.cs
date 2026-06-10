using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NinjaOne.MockAPI.Services;

namespace NinjaOne.MockAPI.Controllers
{
    [ApiController]
    [Route("v2/alerts")]
    public class AlertsController : ControllerBase
    {
        private readonly MockDataService _mockDataService;
        public AlertsController(MockDataService mockDataService)
        {
            _mockDataService = mockDataService;
        }
        [HttpGet]
        public IActionResult Get()
        {
            var json = _mockDataService.GetJson("alerts.json");
            return Content(json, "application/json");
        }
    }
}