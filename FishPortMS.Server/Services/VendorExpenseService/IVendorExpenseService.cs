using FishPortMS.Shared.DTOs.VendorExpDTO;

namespace FishPortMS.Server.Services.VendorExpenseService
{
	public interface IVendorExpenseService
	{
		Task<int> AddVendorExpense(AddVendorExp request);
		Task<int> UpdateVendorExpense(UpdateVendorExp request);
	}
}
