using FishPortMS.Shared.DTOs.DashboardDTO;
using System.Net.Http.Json;

namespace FishPortMS.Services.ClientDashboardService
{
    public class ClientDashboardService : IClientDashboardService
    {
        private readonly HttpClient _httpClient;
        public ClientDashboardService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<List<ChartDataDTO>> GetSalesChartAsync(string chartInterval)
            {
            var response = await _httpClient.GetFromJsonAsync<List<ChartDataDTO>>($"api/dashboard/get-sales?chartInterval={chartInterval}");

            if (response != null) return response;

            return new List<ChartDataDTO>();
        }   
    }
}
    