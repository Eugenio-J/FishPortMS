using DocumentFormat.OpenXml.InkML;
using FishPortMS.Server.Data;
using FishPortMS.Shared.DTOs.DashboardDTO;
using FishPortMS.Shared.Enums;
using FishPortMS.Shared.Enums.Status;
using FishPortMS.Shared.Models.Expenses;
using FishPortMS.Shared.Models.Receipts;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Security.Claims;

namespace FishPortMS.Server.Services.DashboardService
{
    public class DashboardService : IDashboardService
    {
        private readonly DataContext _dbContext;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public DashboardService(DataContext dbContext, IHttpContextAccessor httpContextAccessor)
        {
            _dbContext = dbContext;
            _httpContextAccessor = httpContextAccessor;
        }

        private DateTime PHTime()
        {
            TimeZoneInfo tz = TimeZoneInfo.FindSystemTimeZoneById("Asia/Manila");
            DateTime philippineTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, tz);
            return philippineTime;
        }

		private string? GetUserId() => _httpContextAccessor.HttpContext?.User
                .FindFirstValue(ClaimTypes.NameIdentifier);

		private string? GetUserRole() => _httpContextAccessor.HttpContext?.User
			.FindFirstValue(ClaimTypes.Role);

		public async Task<List<ChartDataDTO>> GetSalesChartAsync(string chartInterval)
		{
            string userRole = GetUserRole() ?? string.Empty;

			string userId = GetUserId() ?? string.Empty;

			DateTime currentDate = PHTime();

			List<ChartDataDTO> result = new List<ChartDataDTO>();

			switch (chartInterval) 
            {
                case "DAILY":
					result = await GetDailySales(userRole, userId, currentDate);
                    break;
				case "WEEKLY":
					result = await GetWeeklySales(userRole, userId, currentDate);
					break;
				case "MONTHLY":
					result = await GetMonthlySales(userRole, userId, currentDate);
					break;
				case "YEARLY":
					result = await GetYearlySales(userRole, userId, currentDate);
					break;
			}

            return result;
        }

        private async Task<List<ChartDataDTO>> GetDailySales(string Role, string userId, DateTime currentDate) 
        {
            IQueryable<Receipt> query = _dbContext.Receipts;

            if (Role == Roles.BUY_AND_SELL.ToString()) 
            {
                query = query.Where(x => x.BSId.ToString() == userId);
            }

			List<ChartDataDTO> result = new List<ChartDataDTO>();

			var todaySales = await query
				   .Where(r => r.DateCreated.Date == currentDate.Date)
				   .GroupBy(r => r.DateCreated.Hour) // group by hour (0–23)
				   .Select(g => new
				   {
					   Hour = g.Key,
					   GrossSales = g.Sum(r => r.GrossSales),
					   NetSales = g.Sum(x => x.NetSales),
					   Porsyento = g.Sum(x => x.DeductedAmount)
				   })
				   .ToListAsync();

			var hourlySales = Enumerable.Range(0, 24)
			.Select(hour => new
			{
				Hour = hour,
				GrossSales = todaySales.FirstOrDefault(s => s.Hour == hour)?.GrossSales ?? 0,
				NetSales = todaySales.FirstOrDefault(s => s.Hour == hour)?.NetSales ?? 0,
				Porsyento = todaySales.FirstOrDefault(s => s.Hour == hour)?.Porsyento ?? 0
			})
			.ToList();

			result = hourlySales
		   .Select(x => new ChartDataDTO
		   {
			   Label = $"{x.Hour:00}:00",   // Format as "00:00", "01:00", etc.
			   GrossSales = x.GrossSales,
			   NetSales = x.NetSales,
			   Porsyento = x.Porsyento
		   })
		   .ToList();

            return result;
		}

        private async Task<List<ChartDataDTO>> GetWeeklySales(string Role, string userId, DateTime currentDate) 
        {
			IQueryable<Receipt> query = _dbContext.Receipts.Include(x => x.VendorExpenses);

			if (Role == Roles.BUY_AND_SELL.ToString())
			{
				query = query.Where(x => x.BSId.ToString() == userId);
			}

			List<ChartDataDTO> result = new List<ChartDataDTO>();

			DateTime today = currentDate.Date;
			int daysSinceSunday = (int)today.DayOfWeek;
			DateTime startOfWeek = today.AddDays(-daysSinceSunday);  // Sunday 00:00
			DateTime endOfWeek = startOfWeek.AddDays(6).AddDays(1).AddTicks(-1);  // Saturday 23:59:59.9999999

			var weekSales = await query
				.Where(r => r.DateCreated >= startOfWeek && r.DateCreated <= endOfWeek)
				.ToListAsync();

			var groupWeekly = weekSales.GroupBy(r => r.DateCreated.DayOfWeek)
				.Select(g => new
				{
					Day = g.Key,
					GrossSales = g.Sum(r => r.GrossSales),
					NetSales = g.Sum(r => r.NetSales),
					Porsyento = g.Sum(r => r.DeductedAmount)
				})
				.ToList();

			var allDays = Enum.GetValues(typeof(DayOfWeek)).Cast<DayOfWeek>();

			var weeklySales = allDays
				.Select(day => new
				{
					Day = day,
					GrossSales = groupWeekly.FirstOrDefault(s => s.Day == day)?.GrossSales ?? 0,
					NetSales = groupWeekly.FirstOrDefault(s => s.Day == day)?.NetSales ?? 0,
					Porsyento = groupWeekly.FirstOrDefault(s => s.Day == day)?.Porsyento ?? 0,
				})
				.ToList();

			result = weeklySales
			   .Select(x => new ChartDataDTO
			   {
				   Label = x.Day.ToString(),  // e.g., "Monday"
				   GrossSales = x.GrossSales,
				   NetSales = x.NetSales,
				   Porsyento = x.Porsyento
			   })
			   .ToList();

			return result;
		}

		private async Task<List<ChartDataDTO>> GetMonthlySales(string Role, string userId, DateTime currentDate)
		{
			IQueryable<Receipt> query = _dbContext.Receipts;

			if (Role == Roles.BUY_AND_SELL.ToString())
			{
				query = query.Where(x => x.BSId.ToString() == userId);
			}

			List<ChartDataDTO> result = new List<ChartDataDTO>();

			int currentYear = currentDate.Year;

			var monthSales = await query
				.Where(r => r.DateCreated.Year == currentYear)
				.GroupBy(r => r.DateCreated.Month) // 1 = January, ..., 12 = December
				.Select(g => new
				{
					Month = g.Key,
					GrossSales = g.Sum(r => r.GrossSales),
					NetSales = g.Sum(r => r.NetSales),
					Porsyento = g.Sum(r => r.DeductedAmount),
				})
				.ToListAsync();

			var monthlySales = Enumerable.Range(1, 12)
				.Select(month => new
				{
					Month = month,
					GrossSales = monthSales.FirstOrDefault(s => s.Month == month)?.GrossSales ?? 0,
					NetSales = monthSales.FirstOrDefault(s => s.Month == month)?.NetSales ?? 0,
					Porsyento = monthSales.FirstOrDefault(s => s.Month == month)?.Porsyento ?? 0,
				})
				.ToList();

			var chartData = monthlySales.Select(x => new
			{
				MonthLabel = new DateTime(currentYear, x.Month, 1).ToString("MMMM"), // "January", "February", ...
				x.GrossSales,
				x.NetSales,
				x.Porsyento
			}).ToList();

			result = chartData
			.Select(x => new ChartDataDTO
			{
				Label = $"{x.MonthLabel}",   // Format as "00:00", "01:00", etc.
				GrossSales = x.GrossSales,
				NetSales = x.NetSales,
				Porsyento = x.Porsyento
			})
			.ToList();

			return result;
		}

		private async Task<List<ChartDataDTO>> GetYearlySales(string Role, string userId, DateTime currentDate)
		{
			IQueryable<Receipt> query = _dbContext.Receipts;

			if (Role == Roles.BUY_AND_SELL.ToString())
			{
				query = query.Where(x => x.BSId.ToString() == userId);
			}

			List<ChartDataDTO> result = new List<ChartDataDTO>();

			int currentYear = currentDate.Year;
			int startYear = currentYear - 4; // last 5 years

			var yearSales = await query
				.Where(r => r.DateCreated.Year >= startYear && r.DateCreated.Year <= currentYear)
				.GroupBy(r => r.DateCreated.Year)
				.Select(g => new
				{
					Year = g.Key,
					GrossSales = g.Sum(r => r.GrossSales),
					NetSales = g.Sum(r => r.NetSales),
					Porsyento = g.Sum(r => r.DeductedAmount)
				})
				.ToListAsync();

			var yearlySales = Enumerable.Range(startYear, currentYear - startYear + 1)
				.Select(year => new
				{
					Year = year,
					GrossSales = yearSales.FirstOrDefault(s => s.Year == year)?.GrossSales ?? 0,
					NetSales = yearSales.FirstOrDefault(s => s.Year == year)?.NetSales ?? 0,
					Porsyento = yearSales.FirstOrDefault(s => s.Year == year)?.Porsyento ?? 0
				})
				.ToList();

			var chartData = yearlySales.Select(x => new
			{
				YearLabel = x.Year.ToString(),
				x.GrossSales,
				x.NetSales,
				x.Porsyento
			}).ToList();

			result = chartData
				 .Select(x => new ChartDataDTO
				 {
					 Label = $"{x.YearLabel}",   // Format as "00:00", "01:00", etc.
					 GrossSales = x.GrossSales,
					 NetSales = x.NetSales,
					 Porsyento = x.Porsyento
				 })
				 .ToList();

			return result;
		}


		public async Task<List<VendorExpenseData>> GetVendorExpense() 
		{
            string userRole = GetUserRole() ?? string.Empty;

            string userId = GetUserId() ?? string.Empty;

            IQueryable<VendorExpense> query = _dbContext.VendorExpenses
				.Include(x => x.VendorExpenseCategory)
				.Include(x => x.Receipt);

            if (userRole == Roles.BUY_AND_SELL.ToString())
            {
                query = query.Where(x => x.Receipt.BSId.ToString() == userId);
            }

            List<VendorExpenseData> result = new List<VendorExpenseData>();

            var expense = await query
					.GroupBy(x => new { x.VendorExpenseCategory.Id, x.VendorExpenseCategory.Title })
                   .Select(g => new
                   {
                       Id = g.Key.Id,
                       Category = g.Key.Title,
                       TotalAmount = g.Sum(r => r.Amount)
                   })
                   .ToListAsync();

            result = expense
           .Select(x => new VendorExpenseData
           {
               ExpenseCategoryName = x.Category, 
               ExpenseCategoryId = x.Id,
               Amount = x.TotalAmount
           })
           .ToList();

            return result;
        }

    }
}
