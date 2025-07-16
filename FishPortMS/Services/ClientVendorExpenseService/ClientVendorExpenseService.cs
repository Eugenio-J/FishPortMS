using FishPortMS.Shared.DTOs.ExpenseDTO;
using FishPortMS.Shared.DTOs.ReceiptDTO;
using FishPortMS.Shared.DTOs.VendorExpDTO;
using FishPortMS.Shared.Response;
using FishPortMS.Utilities;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using System.Net.Http.Json;
using static System.Net.WebRequestMethods;

namespace FishPortMS.Services.ClientVendorExpenseService
{
    public class ClientVendorExpenseService : IClientVendorExpenseService
	{
		private readonly HttpClient _httpClient;
		private readonly ModifiedSnackBar _modifiedSnackBar;
		private readonly NavigationManager _navigationManager;
		public ClientVendorExpenseService(HttpClient httpClient, ModifiedSnackBar modifiedSnackBar, NavigationManager navigationManager)
		{
			_httpClient = httpClient;
			_modifiedSnackBar = modifiedSnackBar;
			_navigationManager = navigationManager;
        }

        public async Task<int> AddCategory(CreateExpenseCategory payload)
        {
			HttpResponseMessage response = await _httpClient.PostAsJsonAsync($"api/vendorexpense/add-expense-category", payload);

			if (response.IsSuccessStatusCode)
			{
				int response_data = await response.Content.ReadFromJsonAsync<int>();
				if (response_data == 0)
				{
					_modifiedSnackBar.ShowMessage("Failed to add category", Severity.Error);
					return 0;
				}
				_modifiedSnackBar.ShowMessage("Added category successfully", Severity.Success);
				return response_data;
			}
			else 
			{
                _modifiedSnackBar.ShowMessage("Failed to add category", Severity.Error);
                return 0;
            }
        }

        public async Task<int> AddExpense(AddVendorExp payload)
		{
			HttpResponseMessage response = await _httpClient.PostAsJsonAsync($"api/vendorexpense/add-vendor-expense", payload);

			if (response.IsSuccessStatusCode)
			{
				int response_data = await response.Content.ReadFromJsonAsync<int>();
				if (response_data == 0) return 0;
				_modifiedSnackBar.ShowMessage("Added expense successfully", Severity.Success);
				_navigationManager.NavigateTo("all-receipt");
				return response_data;
			}
			else 
			{
				_modifiedSnackBar.ShowMessage("Failed to add expense", Severity.Error);
				return 0;
			}
		}

        public async Task<PaginatedTableResponse<GetExpenseCategory>> GetAllExpenseCategoryPaginated(GetPaginatedDTO payload)
        {
            var result = await _httpClient.GetFromJsonAsync<PaginatedTableResponse<GetExpenseCategory>>($"api/vendorexpense/get-category-paginated?Take={payload.Take}&Skip={payload.Skip}");

            if (result != null) return result;

            return new PaginatedTableResponse<GetExpenseCategory>();
        }

        public async Task<List<GetExpenseCategory>> GetCategoryList()
        {
            var result = await _httpClient.GetFromJsonAsync<List<GetExpenseCategory>>($"api/vendorexpense/get-category-list");

            if (result != null) return result;

            return new List<GetExpenseCategory>();
        }

        public async Task<GetExpenseCategory?> GetSingleCategory(int Id)
        {
            var result = await _httpClient.GetFromJsonAsync<GetExpenseCategory>($"api/vendorexpense/get-single-category");

            if (result != null) return result;

            return new GetExpenseCategory();
        }

        public async Task<PaginatedTableResponse<GetExpenseCategory>> SearchExpenseCategory(GetPaginatedDTO payload)
        {
            var result = await _httpClient.GetFromJsonAsync<PaginatedTableResponse<GetExpenseCategory>>($"api/vendorexpense/search-category?Take={payload.Take}&Skip={payload.Skip}&SearchValue={payload.SearchValue}");

            if (result != null) return result;

            return new PaginatedTableResponse<GetExpenseCategory>();
        }

        public async Task<int> UpdateExpense(UpdateVendorExp payload) 
		{
			HttpResponseMessage response = await _httpClient.PutAsJsonAsync($"api/vendorexpense/update-vendor-expense", payload);

			if (response.IsSuccessStatusCode) 
			{
				int response_data = await response.Content.ReadFromJsonAsync<int>();
				if (response_data == 0) return 0;
				_modifiedSnackBar.ShowMessage("Expense updated successfully", Severity.Success);
				return response_data;
			}
			else 
			{
				_modifiedSnackBar.ShowMessage("Failed to update expense", Severity.Error);
				return 0;
			}
		}

        public async Task<int> DeleteExpense(int expenseId)
        {
            HttpResponseMessage response = await _httpClient.DeleteAsync($"api/vendorexpense/delete-vendor-expense/{expenseId}");

            if (response.IsSuccessStatusCode)
            {
                int response_data = await response.Content.ReadFromJsonAsync<int>();
                if (response_data == 0) return 0;
                _modifiedSnackBar.ShowMessage("Expense deleted successfully", Severity.Success);
                return response_data;
            }
            else
            {
                _modifiedSnackBar.ShowMessage("Failed to delete expense", Severity.Error);
                return 0;
            }
        }
    }
}
