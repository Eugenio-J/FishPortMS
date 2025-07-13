using DocumentFormat.OpenXml.InkML;
using FishPortMS.Pages.Receipt;
using FishPortMS.Server.Data;
using FishPortMS.Shared.DTOs.ExpenseDTO;
using FishPortMS.Shared.DTOs.ReceiptDTO;
using FishPortMS.Shared.DTOs.VendorExpDTO;
using FishPortMS.Shared.Enums;
using FishPortMS.Shared.Models.Expenses;
using FishPortMS.Shared.Models.Receipts;
using FishPortMS.Shared.Response;
using Microsoft.EntityFrameworkCore;
using System.Linq;
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

		public async Task<int> AddExpense(AddVendorExp request)
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
					VendorExpenseCategoryId = expense.VendorExpenseCategoryId,
					Receipt = receipt
				};

				 _dbContext.VendorExpenses.Add(newExpense);
				receipt.VendorExpenses?.Add(newExpense);
			}

			return await _dbContext.SaveChangesAsync() == 0 ? 0 : 1;
		}

		public async Task<int> UpdateExpense(UpdateVendorExp request)
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

				exp.VendorExpenseCategoryId = expense.VendorExpenseCategoryId;
				exp.Amount = expense.Amount;
			}

            return await _dbContext.SaveChangesAsync() == 0 ? 0 : 1;
		}

        public async Task<int> AddCategory(CreateExpenseCategory request)
        {
            if(await _dbContext.VendorExpenseCategories.AnyAsync(x => x.Title == request.Title)) return 0;

			VendorExpenseCategory newCategory = new VendorExpenseCategory 
			{
				 Title = request.Title,
			};

			_dbContext.VendorExpenseCategories.Add(newCategory);

			return await _dbContext.SaveChangesAsync() == 0 ? 0 : 1;
        }

        public async Task<PaginatedTableResponse<GetExpenseCategory>> GetAllExpenseCategoryPaginated(GetPaginatedDTO request)
        {
            PaginatedTableResponse<GetExpenseCategory> response = new PaginatedTableResponse<GetExpenseCategory>();

            List<VendorExpenseCategory> expenseCategories = new List<VendorExpenseCategory>();

            string userId = GetUserId() ?? string.Empty;
            string userRole = GetUserRole() ?? string.Empty;

			IQueryable<VendorExpenseCategory>? query = _dbContext.VendorExpenseCategories;

            expenseCategories = await query.OrderByDescending(p => p.Id)
                    .Skip(request.Skip)
                    .Take(request.Take)
                    .ToListAsync();

            response.ResponseData = expenseCategories.Select(ConvertCategoryDTO).ToList();
            response.Count = await query.CountAsync();

            return response;
        }

        public async Task<PaginatedTableResponse<GetExpenseCategory>> SearchExpenseCategory(GetPaginatedDTO request)
        {
            PaginatedTableResponse<GetExpenseCategory> response = new PaginatedTableResponse<GetExpenseCategory>();

            List<VendorExpenseCategory> expenseCategories = new List<VendorExpenseCategory>();

            string userId = GetUserId() ?? string.Empty;
            string userRole = GetUserRole() ?? string.Empty;

            IQueryable<VendorExpenseCategory>? query = _dbContext.VendorExpenseCategories.Where(x => x.Title.Contains(request.SearchValue));

            expenseCategories = await query.OrderByDescending(p => p.Id)
                    .Skip(request.Skip)
                    .Take(request.Take)
                    .ToListAsync();

            response.ResponseData = expenseCategories.Select(ConvertCategoryDTO).ToList();
            response.Count = await query.CountAsync();

            return response;
        }

		private GetExpenseCategory ConvertCategoryDTO(VendorExpenseCategory category) 
		{
			return new GetExpenseCategory 
			{
				Id = category.Id,
				Title = category.Title,
			};
		}

        public async Task<List<GetExpenseCategory>> GetCategoryList()
        {
			List<GetExpenseCategory> result = new List<GetExpenseCategory>();

            var expenseCategories = await _dbContext.VendorExpenseCategories.ToListAsync();

			if(expenseCategories == null) return result;

			result = expenseCategories.Select(ConvertCategoryDTO).ToList();

			return result;
        }

        public async Task<GetExpenseCategory?> GetSingleCategory(int Id)
        {
			var singleCategory = await _dbContext.VendorExpenseCategories.FirstOrDefaultAsync(x => x.Id == Id);

			if (singleCategory == null) return null;

			return ConvertCategoryDTO(singleCategory);
        }
    }
}
