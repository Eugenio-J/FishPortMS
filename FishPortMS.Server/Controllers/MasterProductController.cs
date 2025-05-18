using FishPortMS.Server.Services.MasterProductService;
using FishPortMS.Shared.DTOs.MasterProductDTO;
using FishPortMS.Shared.Models.Products;
using FishPortMS.Shared.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FishPortMS.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MasterProductController : ControllerBase
    {
        private readonly IMasterProductService _masterProductService;

        public MasterProductController(IMasterProductService MasterProductService)
        {
            _masterProductService = MasterProductService;
        }

        [HttpGet("all-products")]
        public async Task<ActionResult<List<MasterProduct>?>> GetAllMasterProducts()
        {
            var status = await _masterProductService.GetAllMasterProducts();
            if (status == null) return Unauthorized();
            return Ok(status);
        }

        [HttpGet("all-categories")]
        public async Task<ActionResult<List<ProductCategory>?>> GetAllCategories()
        {
            var status = await _masterProductService.GetAllCategories();
            if (status == null) return Unauthorized();
            return Ok(status);
        }

        [HttpGet("all-low-stocks")]
        public async Task<ActionResult<List<GetMasterProduct>?>> GetAllLowStocks()
        {
            var status = await _masterProductService.GetAllLowStocks();
            if (status == null) return Unauthorized();
            return Ok(status);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GetMasterProduct?>> GetSingleMasterProduct(int id)
        {
            var status = await _masterProductService.GetSingleMasterProduct(id);
            if (status == null) return NotFound(status);
            return Ok(status);
        }

        [HttpPost("add-product")]
        public async Task<ActionResult<int>> CreateMasterProduct(CreateMasterProduct request)
        {
            int status = await _masterProductService.CreateMasterProduct(request);
            if (status == -1) return Unauthorized();
            if (status == 0) return BadRequest();
            return Ok(status);
        }

        [HttpPut("update-product/{Id}")]
        public async Task<ActionResult<int>> UpdateMasterProduct(int id, CreateMasterProduct request)
        {
            int status = await _masterProductService.UpdateMasterProduct(id, request);
            if (status == -1) return Unauthorized();
            if (status == 0) return BadRequest();
            return Ok(status);
        }

        [HttpPut("update-inventory/{id}")]
        public async Task<ActionResult<MasterProduct>> UpdateMasterInventory(int id, UpdateMasterInventory request)
        {
            int status = await _masterProductService.UpdateMasterInventory(id, request);
            if (status == -1) return Unauthorized();
            if (status == 0) return BadRequest();
            return Ok(status);
        }

        [HttpGet("all-product-paginated")]
        public async Task<ActionResult<PaginatedTableResponse<GetMasterProduct>?>> GetAllProductPaginated([FromQuery] GetPaginatedDTO request)
        {
            var status = await _masterProductService.GetAllProductPaginated(request);
            if (status == null) return Unauthorized();
            return Ok(status);
        }

        [HttpGet("search-product")]
        public async Task<ActionResult<PaginatedTableResponse<GetMasterProduct>?>> SearchProduct([FromQuery] GetPaginatedDTO request)
        {
            var status = await _masterProductService.SearchProduct(request);
            if (status == null) return Unauthorized();
            return status;
        }

        [HttpPut("enable-product/{id}")]
        public async Task<ActionResult<int>> EnableProduct(int id)
        {
            var status = await _masterProductService.EnableProduct(id);
            if (status == -1) return Unauthorized();
            if (status == 0) return BadRequest(status); 
            return Ok(status);
        }

        [HttpPut("disable-product/{id}")]
        public async Task<ActionResult<int>> DisableProduct(int id)
        {
            var status = await _masterProductService.DisableProduct(id);
            if (status == -1) return Unauthorized();
            if (status == 0) return BadRequest(status);
            return Ok(status);
        }
    }
}
