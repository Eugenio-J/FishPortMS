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
            IQueryable<Receipt> query = _dbContext.Receipts
                .Where(x => x.DateCreated >= From && x.DateCreated <= To);

			var grossSale = await query.SumAsync(x => x.GrossSales);

			var NetSales = await query.SumAsync(x => x.NetSales);

			var TotalPorsyento = await query.SumAsync(x => x.DeductedAmount);

            var groupedData = await _dbContext.VendorExpenses
              .Include(x => x.Receipt)
              .Include(x => x.VendorExpenseCategory)
              .Where(x => x.Receipt.DateCreated >= From && x.Receipt.DateCreated <= To)
              .GroupBy(x => new
              {
                  Date = x.Receipt.DateCreated.Date,
                  x.VendorExpenseCategoryId,
                  x.VendorExpenseCategory.Title
              })
              .Select(g => new
              {
                  g.Key.Date,
                  g.Key.VendorExpenseCategoryId,
                  g.Key.Title,
                  TotalAmount = g.Sum(x => x.Amount)
              })
              .OrderBy(x => x.Date)
              .ToListAsync();



            return new SalesSummary
            {
                GrossSales = grossSale,
                NetSales = NetSales,
                TotalPorsyento = TotalPorsyento
            };
		}
	}
}
