using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NinjaOne.MockAPI.Services;

namespace NinjaOne.MockAPI.Controllers
{
    [ApiController]
    [Route("v2/devices")]
    public class DevicesController : ControllerBase
    {
        private readonly MockDataService _mockDataService;

        public DevicesController(MockDataService mockDataService)
        {
            _mockDataService = mockDataService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var json = _mockDataService.GetJson("devices.json");
            return Content(json, "application/json");
        }
    }
}