using FishPortMS.Server.Services.VendorExpenseService;
using FishPortMS.Shared.DTOs.ExpenseDTO;
using FishPortMS.Shared.DTOs.ReceiptDTO;
using FishPortMS.Shared.DTOs.VendorExpDTO;
using FishPortMS.Shared.Response;
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
            var result = await _vendorExpenseService.AddExpense(request);
            if (result == 0) return Unauthorized();
            return Ok(result);
        }

		[HttpPut("update-vendor-expense")]
		public async Task<ActionResult<int>> UpdateVendorExpense(UpdateVendorExp request)
		{
			var result = await _vendorExpenseService.UpdateExpense(request);
			if (result == 0) return Unauthorized();
			return Ok(result);
		}

        [HttpDelete("delete-vendor-expense/{expenseId}")]
        public async Task<ActionResult<int>> DeleteExpense(int expenseId)
        {
            var result = await _vendorExpenseService.DeleteExpense(expenseId);
            if (result == 0) return Unauthorized();
            return Ok(result);
        }

        [HttpPost("add-expense-category")]
        public async Task<ActionResult<int>> AddCategory(CreateExpenseCategory request) 
        {
            var result = await _vendorExpenseService.AddCategory(request);
            if(result == 0) return BadRequest(result);
            return Ok(result);
        }

        [HttpGet("get-category-paginated")]
        public async Task<ActionResult<PaginatedTableResponse<GetExpenseCategory>>> GetAllExpenseCategoryPaginated([FromQuery] GetPaginatedDTO request)
        {
            var result = await _vendorExpenseService.GetAllExpenseCategoryPaginated(request);
            if (result == null) return Unauthorized();
            return Ok(result);
        }

        [HttpGet("search-category")]
        public async Task<ActionResult<PaginatedTableResponse<GetExpenseCategory>>> SearchExpenseCategory([FromQuery] GetPaginatedDTO request)
        {
            var result = await _vendorExpenseService.SearchExpenseCategory(request);
            if (result == null) return Unauthorized();
            return Ok(result);
        }

        [HttpGet("get-category-list")]
        public async Task<ActionResult<List<GetExpenseCategory>>> GetCategoryList()
        {
            var result = await _vendorExpenseService.GetCategoryList();
            if (result == null) return Unauthorized();
            return Ok(result);
        }

        [HttpGet("get-single-category/{Id}")]
        public async Task<ActionResult<GetExpenseCategory>> GetSingleCategory(int Id)
        {
            var result = await _vendorExpenseService.GetSingleCategory(Id);
            if (result == null) return Unauthorized();
            return Ok(result);
        }
    }
}
