using FishPortMS.Shared.DTOs.MasterProductDTO;
using FishPortMS.Shared.Models.Products;
using FishPortMS.Shared.Response;

namespace FishPortMS.Services.ClientMasterProductService
{
    public interface IClientMasterProductService
    {
        List<GetMasterProduct> getMasterProducts { get; set; }
        List<ProductCategory> getCategories { get; set; }
        Task GetAllMasterProducts();
        Task GetAllCategories();
        Task<List<GetMasterProduct>?> GetAllLowStocks();
        Task<GetMasterProduct?> GetSingleMasterProduct(int id);
        Task<int> CreateMasterProduct(CreateMasterProduct payload);
        Task<int> UpdateMasterProduct(int id, CreateMasterProduct payload);
        Task<int> UpdateMasterInventory(int id, UpdateMasterInventory payload);
        Task<PaginatedTableResponse<GetMasterProduct>> GetAllProductPaginated(GetPaginatedDTO request);
        Task<PaginatedTableResponse<GetMasterProduct>> SearchProduct(GetPaginatedDTO request);
        Task<int> EnableProduct(int id);
        Task<int> DisableProduct(int id);
    }
}
