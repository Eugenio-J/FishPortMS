using FishPortMS.Server.Services.DashboardService;
using FishPortMS.Shared.DTOs.DashboardDTO;
using FishPortMS.Shared.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FishPortMS.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DashboardController : ControllerBase
    {
        private readonly IDashboardService _dashboardService;
        public DashboardController(IDashboardService dashboardService)
        {
            _dashboardService = dashboardService;
        }

        [HttpGet("get-sales")]
        public async Task<ActionResult<List<ChartDataDTO>>> GetSalesChartAsync([FromQuery] string chartInterval) 
        {
            var result = await _dashboardService.GetSalesChartAsync(chartInterval);
            if (result == null) return Unauthorized(result);
            return Ok(result);
        }

        [HttpGet("get-expense-data")]
        public async Task<ActionResult<List<VendorExpenseData>>> GetVendorExpense()
        {
            var result = await _dashboardService.GetVendorExpense();
            if (result == null) return Unauthorized(result);
            return Ok(result);
        }

        [HttpGet("get-product-data")]
        public async Task<ActionResult<List<VendorExpenseData>>> GetProductSales([FromQuery] int masterProductId, string chartInterval)
        {
            var result = await _dashboardService.GetProductSales(masterProductId, chartInterval);
            if (result == null) return Unauthorized(result);
            return Ok(result);
        }
    }
}
