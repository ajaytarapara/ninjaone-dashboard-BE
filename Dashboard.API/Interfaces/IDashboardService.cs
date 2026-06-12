using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Dashboard.API.DTOs;

namespace Dashboard.API.Interfaces
{
    public interface IDashboardService
    {
        Task<OverviewMetricsDto> GetOverviewMetricsAsync();
        Task<AssetListResponseDto> GetAssetListAsync(
            string? search,
            string? status,
            int page,
            int pageSize,
            string? sortBy,
            string? sortOrder);
        Task<AlertsSummaryDto> GetAlertsSummaryAsync();
        Task<PatchSummaryDto> GetPatchSummaryAsync();
        Task<AntivirusSummaryDto> GetAntivirusSummaryAsync();
        Task<TicketsSummaryDto> GetTicketsSummaryAsync();
    }
}