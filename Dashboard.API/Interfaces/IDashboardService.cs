using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dashboard.API.DTOs;
using Dashboard.API.Models;

namespace Dashboard.API.Interfaces
{
    public interface IDashboardService
    {
        Task<OverviewMetricsDto> GetOverviewMetricsAsync();
        Task<AssetListResponseDto> GetAssetListAsync(string? search, string? status, int pageNumber, int pageSize);
        Task<AlertsSummaryDto> GetAlertsSummaryAsync();
        Task<PatchSummaryDto> GetPatchSummaryAsync();
        Task<AntivirusSummaryDto> GetAntivirusSummaryAsync();
        Task<TicketsSummaryDto> GetTicketsSummaryAsync();
    }
}