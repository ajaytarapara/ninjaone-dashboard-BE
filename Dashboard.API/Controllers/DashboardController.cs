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

        /// <summary>
        /// Returns high-level dashboard overview metrics.
        /// </summary>
        /// <remarks>
        /// Powers the following dashboard widgets:
        ///
        /// KPI Cards:
        /// - Total Devices
        /// - Online Devices
        /// - Offline Devices
        /// - Critical Alerts
        /// - Patch Compliance
        /// - Open Tickets
        /// - Security Score
        ///
        /// Charts:
        /// - Device Health Donut
        /// - OS Platform Distribution
        ///
        /// UI Features:
        /// - KPI Trend Indicators
        /// </remarks>
        /// <returns>Dashboard overview metrics.</returns>
        [HttpGet("overview-metrics")]
        public async Task<IActionResult> GetOverviewMetrics()
        {
            var result = await _dashboardService.GetOverviewMetricsAsync();

            return Ok(result);
        }

        /// <summary>
        /// Returns managed device inventory and asset information.
        /// </summary>
        /// <remarks>
        /// Powers:
        /// - Device Inventory Table
        /// - Device Status Column
        /// - Device Health Column
        /// - Organization Column
        /// - Location Column
        /// - Device Type Column
        /// - Last Contact Column
        /// - Device Tags
        /// - Search
        /// - Filtering
        /// - Pagination
        /// </remarks>
        [HttpGet("asset-list")]
        public async Task<ActionResult<AssetListResponseDto>> GetAssetList(
            string? search = null,
            string? status = null,
            int page = 1,
            int pageSize = 25,
            string? sortBy = "deviceName",
            string? sortOrder = "asc")
        {
            var result = await _dashboardService.GetAssetListAsync(
                search,
                status,
                page,
                pageSize,
                sortBy,
                sortOrder);

            return Ok(result);
        }
        
        /// <summary>
        /// Returns dashboard alert metrics and severity distribution.
        /// </summary>
        /// <remarks>
        /// Powers the following dashboard widgets:
        ///
        /// KPI Cards
        /// - Total Alerts
        /// - Critical Alerts
        /// - High Alerts
        /// - Medium Alerts
        /// - Low Alerts
        ///
        /// Charts
        /// - Alert Severity Distribution
        /// - Alert Severity Donut
        /// - Alert Severity Bar Chart
        ///
        /// UI Features
        /// - Alert Trend Indicators
        /// </remarks>
        /// <returns>Dashboard alerts summary.</returns>
        [HttpGet("alerts-summary")]
        public async Task<IActionResult> GetAlertsSummary()
        {
            var result = await _dashboardService.GetAlertsSummaryAsync();

            return Ok(result);
        }

        /// <summary>
        /// Returns patch management summary metrics.
        /// </summary>
        /// <remarks>
        /// Powers:
        ///
        /// KPI Cards
        /// - Total Patches
        /// - Installed Patches
        /// - Pending Patches
        /// - Failed Patches
        /// - Critical Patches
        /// - Compliance Score
        ///
        /// Charts
        /// - Patch Status Distribution
        /// - Patch Severity Distribution
        ///
        /// Dashboard Widgets
        /// - Compliance Donut
        /// - Patch Health Cards
        /// - Vulnerability Summary Widgets
        /// </remarks>

        [HttpGet("patch-summary")]
        public async Task<ActionResult<PatchSummaryDto>> GetPatchSummary()
        {
            var result = await _dashboardService.GetPatchSummaryAsync();

            return Ok(result);
        }

        /// <summary>
        /// Returns antivirus protection metrics and endpoint security data.
        /// </summary>
        /// <remarks>
        /// Powers:
        /// - Protected Devices KPI
        /// - Unprotected Devices KPI
        /// - Antivirus Coverage Percentage
        /// - Antivirus Health Score
        /// - Active Protection KPI
        /// - Out-of-Date Definitions KPI
        /// - Protection Status Distribution
        /// - Antivirus Vendor Distribution
        /// - Endpoint Protection Donut
        /// - Antivirus Device Inventory Table
        /// </remarks>
        [HttpGet("antivirus-summary")]
        public async Task<ActionResult<AntivirusSummaryDto>> GetAntivirusSummary()
        {
            var result =
                await _dashboardService.GetAntivirusSummaryAsync();

            return Ok(result);
        }

        /// <summary>
        /// Returns ticket metrics and service desk insights.
        /// </summary>
        /// <remarks>
        /// Powers:
        /// - Open Tickets KPI
        /// - Critical Tickets KPI
        /// - High Tickets KPI
        /// - Medium Tickets KPI
        /// - Low Tickets KPI
        /// - Ticket Priority Distribution
        /// - Ticket Status Distribution
        /// - Recent Tickets Table
        /// - Ticket Trend Indicators
        /// </remarks>
        [HttpGet("tickets-summary")]
        public async Task<ActionResult<TicketsSummaryDto>> GetTicketsSummary()
        {
            var result =
                await _dashboardService
                    .GetTicketsSummaryAsync();

            return Ok(result);
        }
    }
}