using FishPortMS.Shared.DTOs.VendorExpDTO;
using FishPortMS.Utilities;
using MudBlazor;
using System.Net.Http.Json;

namespace FishPortMS.Services.ClientVendorExpenseService
{
	public class ClientVendorExpenseService : IClientVendorExpenseService
	{
		private readonly HttpClient _httpClient;
		private readonly ModifiedSnackBar _modifiedSnackBar;
		public ClientVendorExpenseService(HttpClient httpClient, ModifiedSnackBar modifiedSnackBar)
		{
			_httpClient = httpClient;
			_modifiedSnackBar = modifiedSnackBar;
		}

		public async Task<int> AddVendorExpense(AddVendorExp payload)
		{
			HttpResponseMessage response = await _httpClient.PostAsJsonAsync($"api/vendorexpense/add-vendor-expense", payload);

			if (response.IsSuccessStatusCode)
			{
				int response_data = await response.Content.ReadFromJsonAsync<int>();
				if (response_data == 0) return 0;
				_modifiedSnackBar.ShowMessage("Added expense successfully", Severity.Success);
				return response_data;
			}
			else 
			{
				_modifiedSnackBar.ShowMessage("Failed to add expense", Severity.Error);
				return 0;
			}
		}

		public async Task<int> UpdateVendorExpense(UpdateVendorExp payload) 
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
	}
}
