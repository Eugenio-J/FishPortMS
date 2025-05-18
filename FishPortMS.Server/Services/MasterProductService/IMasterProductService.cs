using FishPortMS.Shared.DTOs.MasterProductDTO;
using FishPortMS.Shared.Models;
using FishPortMS.Shared.Models.Products;
using FishPortMS.Shared.Response;

namespace FishPortMS.Server.Services.MasterProductService
{
    public interface IMasterProductService
    {
        Task<List<GetMasterProduct>?> GetAllMasterProducts();
        Task<List<ProductCategory>?> GetAllCategories();
        Task<List<GetMasterProduct>?> GetAllLowStocks();
        Task<GetMasterProduct?> GetSingleMasterProduct(int id);
        Task<int> CreateMasterProduct(CreateMasterProduct request);
        Task<int> UpdateMasterProduct(int id, CreateMasterProduct request);
        Task<int> UpdateMasterInventory(int id, UpdateMasterInventory request);
        Task<PaginatedTableResponse<GetMasterProduct>?> GetAllProductPaginated(GetPaginatedDTO request);
        Task<PaginatedTableResponse<GetMasterProduct>?> SearchProduct(GetPaginatedDTO request);
        Task<int> EnableProduct(int id);
        Task<int> DisableProduct(int id);
    }
}
