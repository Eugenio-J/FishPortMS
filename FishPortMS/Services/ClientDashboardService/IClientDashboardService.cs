using FishPortMS.Shared.DTOs.DashboardDTO;

namespace FishPortMS.Services.ClientDashboardService
{
    public interface IClientDashboardService
    {
        Task<List<ChartDataDTO>> GetSalesChartAsync(string chartInterval);
    }
}
