using FishPortMS.Shared.DTOs.ExpenseDTO;
using FishPortMS.Shared.DTOs.ReceiptDTO;
using FishPortMS.Shared.DTOs.VendorExpDTO;
using FishPortMS.Shared.Response;

namespace FishPortMS.Server.Services.VendorExpenseService
{
	public interface IVendorExpenseService
	{
		Task<int> AddCategory(CreateExpenseCategory request);
		Task<int> AddExpense(AddVendorExp request);
		Task<int> UpdateExpense(UpdateVendorExp request);
        Task<PaginatedTableResponse<GetExpenseCategory>> GetAllExpenseCategoryPaginated(GetPaginatedDTO request);
        Task<PaginatedTableResponse<GetExpenseCategory>> SearchExpenseCategory(GetPaginatedDTO request);
		Task<List<GetExpenseCategory>> GetCategoryList();
		Task<GetExpenseCategory?> GetSingleCategory(int Id);
    }
}
