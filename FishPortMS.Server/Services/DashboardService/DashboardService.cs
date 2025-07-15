using DocumentFormat.OpenXml.InkML;
using FishPortMS.Server.Data;
using FishPortMS.Shared.DTOs.DashboardDTO;
using FishPortMS.Shared.Enums.Status;
using FishPortMS.Shared.Models.Receipts;
using Microsoft.EntityFrameworkCore;

namespace FishPortMS.Server.Services.DashboardService
{
    public class DashboardService : IDashboardService
    {
        private readonly DataContext _dbContext;
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public DashboardService(DataContext dbContext)
        {
            _dbContext = dbContext;
        }

		public async Task<SalesSummary> GetSalesByDate(DateTime From, DateTime To)
		{
            IQueryable<Receipt> query = _dbContext.Receipts.Where(x => x.DateCreated >= From && x.DateCreated <= To);

			var grossSale = await query.SumAsync(x => x.GrossSales);

			var NetSales = await query.SumAsync(x => x.NetSales);

			var TotalPercentage = await _dbContext.Receipts
			  .Where(x => x.DateCreated >= From && x.DateCreated <= To)
			  .SumAsync(x => x.GrossSales);

			var TotalExpense = await _dbContext.VendorExpenses
                .Include(x => x.Receipt)
                .Where(x => x.Receipt.DateCreated >= From && x.Receipt.DateCreated <= To)
			    .SumAsync(x => x.Amount);

            return new SalesSummary
            {
                GrossSales = grossSale,
                NetSales = NetSales,
            };
		}
	}
}
