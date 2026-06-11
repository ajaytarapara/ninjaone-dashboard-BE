using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dashboard.API.Models;

namespace Dashboard.API.Interfaces
{
    public interface INinjaOneClient
    {
        Task<List<Device>> GetDevicesAsync();
        Task<string> GetAlertsAsync();
        Task<string> GetOrganizationsAsync();
        Task<string> GetLocationsAsync();
        Task<string> GetUsersAsync();
        Task<string> GetActivitiesAsync();
        Task<List<OsPatchInstall>> GetOsPatchInstallsAsync();
        Task<List<AntivirusStatus>> GetAntivirusStatusAsync();
        Task<List<TicketBoard>> GetBoardsAsync();
        Task<TicketBoardRunResponse> RunBoardAsync(int boardId);
    }
}