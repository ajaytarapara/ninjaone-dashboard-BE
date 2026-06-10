using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dashboard.API.DTOs;
using Dashboard.API.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Dashboard.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DashboardController : ControllerBase
    {
        private readonly IDashboardService _dashboardService;

        public DashboardController(IDashboardService dashboardService)
        {
            _dashboardService = dashboardService;
        }

        [HttpGet("overview-metrics")]
        public async Task<IActionResult> GetOverviewMetrics()
        {
            var result = await _dashboardService.GetOverviewMetricsAsync();

            return Ok(result);
        }

        [HttpGet("asset-list")]
        public async Task<IActionResult> GetAssetList()
        {
            var result = await _dashboardService.GetAssetListAsync();

            return Ok(result);
        }

        [HttpGet("alerts-summary")]
        public async Task<IActionResult> GetAlertsSummary()
        {
            var result = await _dashboardService.GetAlertsSummaryAsync();

            return Ok(result);
        }

        [HttpGet("patch-summary")]
        public async Task<ActionResult<PatchSummaryDto>> GetPatchSummary()
        {
            var result = await _dashboardService.GetPatchSummaryAsync();

            return Ok(result);
        }
    }
}