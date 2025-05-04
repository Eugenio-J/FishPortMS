using FishPortMS.Shared.DTOs.MasterProductDTO;
using FishPortMS.Shared.Models.Products;
using FishPortMS.Shared.Response;

namespace FishPortMS.Server.Services.ProductCategoryService
{
    public interface IProductCategoryService
    {
        Task<ProductCategory?> GetSingleProductCategory(int id);
        Task<int> CreateProductCategory(CreateProductCategoryDTO request);
        Task<int> UpdateProductCategory(int id, CreateProductCategoryDTO request);
        Task<PaginatedTableResponse<ProductCategory>?> GetAllProductCategoryPaginated(GetPaginatedDTO request);
        Task<PaginatedTableResponse<ProductCategory>?> SearchProductCategory(GetPaginatedDTO request);
    }
}
