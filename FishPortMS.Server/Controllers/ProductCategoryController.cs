using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using FishPortMS.Server.Services.ProductCategoryService;
using FishPortMS.Shared.DTOs.MasterProductDTO;
using FishPortMS.Shared.Models.Products;
using FishPortMS.Shared.Response;

namespace FishPortMS.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductCategoryController : ControllerBase
    {
        private readonly IProductCategoryService _productCategoryService;

        public ProductCategoryController(IProductCategoryService ProductCategoryService) 
        {
            _productCategoryService = ProductCategoryService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GetMasterProduct?>> GetSingleMasterProduct(int id)
        {
            var status = await _productCategoryService.GetSingleProductCategory(id);
            if (status == null) return NotFound(status);
            return Ok(status);
        }

        [HttpPost("add-category")]
        public async Task<ActionResult<int>> CreateProductCategory(CreateProductCategoryDTO request)
        {
            int status = await _productCategoryService.CreateProductCategory(request);
            if (status == -1) return Unauthorized();
            if (status == 0) return BadRequest();
            return Ok(status);
        }

        [HttpPut("update-category/{Id}")]
        public async Task<ActionResult<int>> UpdateProductCategory(int id, CreateProductCategoryDTO request)
        {
            int status = await _productCategoryService.UpdateProductCategory(id, request);
            if (status == -1) return Unauthorized();
            if (status == 0) return BadRequest();
            return Ok(status);
        }

        [HttpGet("all-category-paginated")]
        public async Task<ActionResult<PaginatedTableResponse<ProductCategory>?>> GetAllProductCategoryPaginated([FromQuery] GetPaginatedDTO request)
        {
            var status = await _productCategoryService.GetAllProductCategoryPaginated(request);
            if (status == null) return Unauthorized();
            return Ok(status);
        }

        [HttpGet("search-category")]
        public async Task<ActionResult<PaginatedTableResponse<ProductCategory>?>> SearchProductCategory([FromQuery] GetPaginatedDTO request)
        {
            var status = await _productCategoryService.SearchProductCategory(request);
            if (status == null) return Unauthorized();
            return status;
        }
    }
}
