using FishPortMS.Shared.Models.Products;

namespace FishPortMS.Server.Repositories.MasterProductRepository
{
	public interface IMasterProductRepository
	{
		Task<List<MasterProduct>> GetAllMasterProduct();
	}
}
