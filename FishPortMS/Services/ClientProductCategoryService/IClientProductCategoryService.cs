using FishPortMS.Shared.DTOs.MasterProductDTO;
using FishPortMS.Shared.Models.Products;
using FishPortMS.Shared.Response;

namespace FishPortMS.Services.ClientProductCategoryService
{
    public interface IClientProductCategoryService
    {
        Task<ProductCategory?> GetSingleProductCategory(int id);
        Task<int> CreateProductCategory(CreateProductCategoryDTO payload);
        Task<int> UpdateProductCategory(int id, CreateProductCategoryDTO payload);
        Task<PaginatedTableResponse<ProductCategory>> GetAllProductCategoryPaginated(GetPaginatedDTO payload);
        Task<PaginatedTableResponse<ProductCategory>> SearchProductCategory(GetPaginatedDTO payload);
    }
}
