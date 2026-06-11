using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NinjaOne.MockAPI.Services;

namespace NinjaOne.MockAPI.Controllers
{
    [ApiController]
    [Route("v2/ticketing")]
    public class TicketingController : ControllerBase
    {
        private readonly MockDataService _mockDataService;

        public TicketingController(MockDataService mockDataService)
        {
            _mockDataService = mockDataService;
        }

        [HttpGet("trigger/boards")]
        public IActionResult GetBoards()
        {
            return Content(
                _mockDataService.GetJson("boards.json"),
                "application/json");
        }

        [HttpPost("trigger/board/{boardId}/run")]
        public IActionResult RunBoard(int boardId)
        {
            return Content(
                _mockDataService.GetJson("board-run-response.json"),
                "application/json");
        }
    }
}