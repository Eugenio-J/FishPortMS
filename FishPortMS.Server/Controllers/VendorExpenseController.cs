using FishPortMS.Server.Services.VendorExpenseService;
using FishPortMS.Shared.DTOs.VendorExpDTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FishPortMS.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VendorExpenseController : ControllerBase
    {
        private readonly IVendorExpenseService _vendorExpenseService;
        public VendorExpenseController(IVendorExpenseService vendorExpenseService)
        {
            _vendorExpenseService = vendorExpenseService;
        }

        [HttpPost("add-vendor-expense")]
        public async Task<ActionResult<int>> AddVendorExpense(AddVendorExp request) 
        {
            var result = await _vendorExpenseService.AddVendorExpense(request);
            if (result == 0) return Unauthorized();
            return Ok(result);
        }

		[HttpPut("update-vendor-expense")]
		public async Task<ActionResult<int>> UpdateVendorExpense(UpdateVendorExp request)
		{
			var result = await _vendorExpenseService.UpdateVendorExpense(request);
			if (result == 0) return Unauthorized();
			return Ok(result);
		}

	}
}
