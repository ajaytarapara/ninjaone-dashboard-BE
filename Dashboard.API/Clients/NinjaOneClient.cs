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

        public async Task<string> GetDevicesAsync()
        {
            return await _httpClient.GetStringAsync("/v2/devices");
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
    }
}