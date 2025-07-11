using FishPortMS.Shared.DTOs.VendorExpDTO;

namespace FishPortMS.Services.ClientVendorExpenseService
{
	public interface IClientVendorExpenseService
	{
		Task<int> AddVendorExpense(AddVendorExp payload);
		Task<int> UpdateVendorExpense(UpdateVendorExp payload);
	}
}
