using FishPortMS.Server.Data;
using FishPortMS.Shared.DTOs.VendorExpDTO;
using FishPortMS.Shared.Enums;
using FishPortMS.Shared.Models.Expenses;
using FishPortMS.Shared.Models.Receipts;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace FishPortMS.Server.Services.VendorExpenseService
{
	public class VendorExpenseService : IVendorExpenseService
	{
		private readonly DataContext _dbContext;
		private readonly IHttpContextAccessor _httpContextAccessor;

		public VendorExpenseService(DataContext dbContext, IHttpContextAccessor httpContextAccessor)
		{
			_dbContext = dbContext;
			_httpContextAccessor = httpContextAccessor;
		}

		private string? GetUserId() => _httpContextAccessor.HttpContext?.User
		 .FindFirstValue(ClaimTypes.NameIdentifier);

		private string? GetUserRole() => _httpContextAccessor.HttpContext?.User
			.FindFirstValue(ClaimTypes.Role);

		public async Task<int> AddVendorExpense(AddVendorExp request)
		{
			if (!Enum.TryParse(GetUserRole(), out Roles acc_role)) return 0;

			if (acc_role != Roles.BUY_AND_SELL) return 0;

			Receipt? receipt = await _dbContext.Receipts
				.Include(x => x.ReceiptItems)
				.Where(x => x.Id == request.ReceiptId)
				.FirstOrDefaultAsync();

			if (receipt == null) return 0;

			foreach (var expense in request.VendorExpenses) 
			{
				VendorExpense newExpense = new VendorExpense
				{
					ReceiptId = request.ReceiptId,
					Amount = expense.Amount,
					ExpenseCategoryId = expense.ExpenseCategoryId,
					Receipt = receipt
				};

				 _dbContext.VendorExpenses.Add(newExpense);
				receipt.VendorExpenses?.Add(newExpense);
			}

			return await _dbContext.SaveChangesAsync() == 0 ? 0 : 1;
		}

		public async Task<int> UpdateVendorExpense(UpdateVendorExp request)
		{
			if (!Enum.TryParse(GetUserRole(), out Roles acc_role)) return 0;

			if (acc_role != Roles.BUY_AND_SELL) return 0;

			Receipt? receipt = await _dbContext.Receipts
				.Include(x => x.ReceiptItems)
				.Include(x => x.VendorExpenses)
				.Where(x => x.Id == request.ReceiptId)
				.FirstOrDefaultAsync();

			if (receipt == null) return 0;

			foreach (var expense in request.VendorExpenses)
			{
				var exp = receipt.VendorExpenses
					.FirstOrDefault(x => x.Id == expense.Id 
						&& x.ReceiptId == request.ReceiptId);

				if (exp == null) return 0;

				exp.ExpenseCategoryId = expense.ExpenseCategoryId;
				exp.Amount = expense.Amount;
			}

			return await _dbContext.SaveChangesAsync() == 0 ? 0 : 1;
		}
	}
}
