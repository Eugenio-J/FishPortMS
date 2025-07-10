using FishPortMS.Server.Services.ReceiptService;
using FishPortMS.Shared.DTOs.ReceiptDTO;
using FishPortMS.Shared.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FishPortMS.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReceiptController : ControllerBase
    {
        private readonly IReceiptService _receiptService;
        public ReceiptController(IReceiptService receiptService)
        {
            _receiptService = receiptService;
        }

        [HttpPost("create-receipt")]
        public async Task<ActionResult<int>> CreateReceipt(CreateReceiptDTO request) 
        {
            var status = await _receiptService.CreateReceipt(request);
            if (status == 0) return BadRequest(status);
            return Ok(status);
        }

        [HttpPut("update-receipt/{Id}")]
        public async Task<ActionResult<int>> UpdateReceipt(int Id, CreateReceiptDTO request)
        {
            var status = await _receiptService.UpdateReceipt(Id, request);
            if (status == 0) return BadRequest(status);
            return Ok(status);
        }

        [HttpGet("get-receipt-paginated")]
        public async Task<ActionResult<PaginatedTableResponse<GetReceiptDTO>>> GetReceiptPaginated([FromQuery] GetPaginatedDTO request) 
        {
            var result = await _receiptService.GetReceiptPaginated(request);
            if (result == null) return Unauthorized();
            return Ok(result);  
        }

        [HttpGet("search-receipt")]
        public async Task<ActionResult<PaginatedTableResponse<GetReceiptDTO>>> SearchReceipt([FromQuery] GetPaginatedDTO request)
        {
            var result = await _receiptService.SearchReceipt(request);
            if (result == null) return Unauthorized();
            return Ok(result);
        }
        [HttpGet("single-receipt")]
        public async Task<ActionResult<GetReceiptDTO>> GetSingleReceipt(int receiptId) 
        { 
            var result = await _receiptService.GetSingleReceipt(receiptId);
            if (result == null) return Unauthorized();  
            return Ok(result);
        }

    }
}
