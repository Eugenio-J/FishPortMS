using FishPortMS.Components.Dashboard;
using FishPortMS.Shared.DTOs.DashboardDTO;
using FishPortMS.Shared.DTOs.NotificationDTO;
using FishPortMS.Shared.Enums;
using FishPortMS.Shared.Response;

namespace FishPortMS.Server.Services.DashboardService
{
    public interface IDashboardService
    {
        Task<List<ChartDataDTO>> GetSalesChartAsync(string chartInterval);
        Task<List<VendorExpenseData>> GetVendorExpense();
    }
}
