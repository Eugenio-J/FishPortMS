using FishPortMS.Shared.DTOs.ExpenseDTO;
using FishPortMS.Shared.DTOs.VendorExpDTO;
using FishPortMS.Shared.Response;

namespace FishPortMS.Services.ClientVendorExpenseService
{
	public interface IClientVendorExpenseService
	{
		Task<int> AddExpense(AddVendorExp payload);
		Task<int> UpdateExpense(UpdateVendorExp payload);
		Task<int> DeleteExpense(int expenseId);
		Task<int> AddCategory(CreateExpenseCategory payload);
        Task<PaginatedTableResponse<GetExpenseCategory>> GetAllExpenseCategoryPaginated(GetPaginatedDTO payload);
        Task<PaginatedTableResponse<GetExpenseCategory>> SearchExpenseCategory(GetPaginatedDTO payload);
        Task<List<GetExpenseCategory>> GetCategoryList();
        Task<GetExpenseCategory?> GetSingleCategory(int Id);
    }
}
