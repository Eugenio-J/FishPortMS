using FishPortMS.Server.Data;
using FishPortMS.Shared.Models.Products;
using Microsoft.EntityFrameworkCore;
using MudBlazor;

namespace FishPortMS.Server.Repositories.MasterProductRepository
{
	public class MasterProductRepository : IMasterProductRepository
	{
		private readonly DataContext _context;

        public MasterProductRepository(DataContext context)
        {
            _context = context;
        }

		public async Task<List<MasterProduct>> GetAllMasterProduct()
		{
			return await _context.MasterProducts.Include(x => x.MasterInventory).ToListAsync();
		}
	}
}
