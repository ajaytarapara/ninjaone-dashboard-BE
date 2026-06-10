using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NinjaOne.MockAPI.Services;

namespace NinjaOne.MockAPI.Controllers
{
    [ApiController]
    [Route("v2/users")]
    public class UsersController : ControllerBase
    {
        private readonly MockDataService _mockDataService;

        public UsersController(MockDataService mockDataService)
        {
            _mockDataService = mockDataService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Content(
            _mockDataService.GetJson("users.json"),
             "application/json");
        }
    }
}