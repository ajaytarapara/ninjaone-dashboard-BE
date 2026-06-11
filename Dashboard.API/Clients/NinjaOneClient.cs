using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dashboard.API.Interfaces;
using Dashboard.API.Models;

namespace Dashboard.API.Clients
{
    public class NinjaOneClient : INinjaOneClient
    {
        private readonly HttpClient _httpClient;

        public NinjaOneClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<Device>> GetDevicesAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<Device>>
            (
                "/v2/devices"
            ) ?? [];
        }

        public async Task<string> GetAlertsAsync()
        {
            return await _httpClient.GetStringAsync("/v2/alerts");
        }

        public async Task<string> GetOrganizationsAsync()
        {
            return await _httpClient.GetStringAsync("/v2/organizations");
        }

        public async Task<string> GetLocationsAsync()
        {
            return await _httpClient.GetStringAsync("/v2/locations");
        }

        public async Task<string> GetUsersAsync()
        {
            return await _httpClient.GetStringAsync("/v2/users");
        }

        public async Task<string> GetActivitiesAsync()
        {
            return await _httpClient.GetStringAsync("/v2/activities");
        }

        public async Task<List<OsPatchInstall>> GetOsPatchInstallsAsync()
        {
            var response =
                await _httpClient.GetFromJsonAsync<OsPatchInstallResponse>(
                    "/v2/queries/os-patch-installs");

            return response?.Results ?? [];
        }

        public async Task<List<AntivirusStatus>> GetAntivirusStatusAsync()
        {
            var response =
                await _httpClient.GetFromJsonAsync<AntivirusStatusResponse>
                (
                    "/v2/queries/antivirus-status"
                );

            return response?.Results ?? [];
        }

        public async Task<List<TicketBoard>> GetBoardsAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<TicketBoard>>
            (
                "/v2/ticketing/trigger/boards"
            ) ?? [];
        }

        public async Task<TicketBoardRunResponse> RunBoardAsync(int boardId)
        {
            var response =
                await _httpClient.PostAsJsonAsync(
                    $"/v2/ticketing/trigger/board/{boardId}/run",
                    new { });

            response.EnsureSuccessStatusCode();

            return await response.Content
                .ReadFromJsonAsync<TicketBoardRunResponse>()
                    ?? new TicketBoardRunResponse();
        }
    }
}