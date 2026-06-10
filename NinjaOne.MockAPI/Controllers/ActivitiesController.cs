using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NinjaOne.MockAPI.Services;

namespace NinjaOne.MockAPI.Controllers
{
    [ApiController]
    [Route("v2/activities")]
    public class ActivitiesController : ControllerBase
    {
        private readonly MockDataService _mockDataService;
        public ActivitiesController(MockDataService mockDataService)
        {
            _mockDataService = mockDataService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Content(
              _mockDataService.GetJson("activities.json"),
              "application/json");
        }
    }
}