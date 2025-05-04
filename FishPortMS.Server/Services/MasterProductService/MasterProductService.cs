using FishPortMS.Server.Data;
using FishPortMS.Shared.DTOs.MasterProductDTO;
using FishPortMS.Shared.Enums;
using FishPortMS.Shared.Models.Products;
using FishPortMS.Shared.Response;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace FishPortMS.Server.Services.MasterProductService
{
    public class MasterProductService : IMasterProductService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly DataContext _context;

        public MasterProductService(IHttpContextAccessor httpContextAccessor, DataContext context)
        {
            _httpContextAccessor = httpContextAccessor;
            _context = context;
        }

        private string? GetUserRole() => _httpContextAccessor.HttpContext?.User
           .FindFirstValue(ClaimTypes.Role);

        private GetMasterProduct ConvertDTO(MasterProduct masterProduct)
        {
            List<ProductCarouselDTO> paymentCarouselDTO = masterProduct.ProductCarousels?
           .Select(carousel => new ProductCarouselDTO
           {
               Id = carousel.Id,
               MasterProductId = carousel.MasterProductId,
               ProductImageURL = carousel.ProductImageURL,
           }).ToList() ?? new List<ProductCarouselDTO>();

            GetMasterInventory getMasterInventory = new GetMasterInventory
            {
                Id = masterProduct.MasterInventory.Id,
                Qty = masterProduct.MasterInventory.Qty,
                SRP = masterProduct.MasterInventory.SRP,
                CurrentPrice = masterProduct.MasterInventory.CurrentPrice,
                PreviousPrice = masterProduct.MasterInventory.PreviousPrice
            };

            return new GetMasterProduct
            {
                Id = masterProduct.Id,
                Name = masterProduct.Name,
                Description = masterProduct.Description,
                IsActive = masterProduct.IsActive,
                MasterInventory = getMasterInventory,
                ProductImageURLs = paymentCarouselDTO
            };
        }

        public async Task<List<GetMasterProduct>?> GetAllMasterProducts()
        {
            if (!Enum.TryParse(GetUserRole(), out Roles acc_role)) return null;

            List<GetMasterProduct> getMasterProducts = new List<GetMasterProduct>();

            List<MasterProduct>? mproducts = await _context.MasterProducts
                .Include(p => p.MasterInventory)
                .OrderBy(p => p.Name)
                .ToListAsync();

            foreach (MasterProduct? mproduct in mproducts)
            {
                getMasterProducts.Add(ConvertDTO(mproduct));
            }

            return getMasterProducts;
        }

        public async Task<List<ProductCategory>> GetAllCategories()
        {
            if (!Enum.TryParse(GetUserRole(), out Roles acc_role)) return null;

            List<ProductCategory> productCategory = await _context.ProductCategories
                .OrderByDescending(category => category.Id)
                .ToListAsync();

            return productCategory;
        }

        public async Task<int> CreateMasterProduct(CreateMasterProduct request)
        {
            if (!Enum.TryParse(GetUserRole(), out Roles acc_role)) return -1;

            MasterInventory? masterInventory = new MasterInventory();

            MasterProduct? masterProduct = new MasterProduct
            {
                Name = request.Name,
                Description = request.Description,
                MasterInventory = masterInventory
            };

            foreach (string img in request.ProductImageURLs)
            {
                ProductCarousel new_carousel = new ProductCarousel
                {
                    MasterProduct = masterProduct,
                    ProductImageURL = img
                };
                _context.ProductCarousels.Add(new_carousel);
            }

            _context.MasterInventories.Add(masterInventory);
            _context.MasterProducts.Add(masterProduct);

            return await _context.SaveChangesAsync() == 0 ? 0 : 1;
        }

        public async Task<List<GetMasterProduct>?> GetAllLowStocks()
        {
            if (!Enum.TryParse(GetUserRole(), out Roles acc_role)) return null;

            List<GetMasterProduct> getMasterProducts = new List<GetMasterProduct>();

            List<MasterProduct>? mproducts = await _context.MasterProducts
                .Include(product => product.MasterInventory)
                .Where(product => product.MasterInventory.Qty < 10)
                .OrderBy(product => product.Name)
                .ToListAsync();

            foreach (MasterProduct? mproduct in mproducts)
            {
                getMasterProducts.Add(ConvertDTO(mproduct));
            }

            return getMasterProducts;
        }

        public async Task<GetMasterProduct?> GetSingleMasterProduct(int id)
        {
            if (!Enum.TryParse(GetUserRole(), out Roles acc_role)) return null;

            GetMasterProduct getMasterProduct = new GetMasterProduct();

            MasterProduct? mproduct = await _context.MasterProducts
                .Include(product => product.MasterInventory)
                .Include(product => product.ProductCarousels)
                .FirstOrDefaultAsync(product => product.Id == id);

            if (mproduct == null) return null;

            return getMasterProduct = ConvertDTO(mproduct);
        }

        public async Task<int> UpdateMasterInventory(int id, UpdateMasterInventory request)
        {
            if (!Enum.TryParse(GetUserRole(), out Roles acc_role)) return -1;

            MasterInventory? masterInventory = await _context.MasterInventories
                 .FirstOrDefaultAsync(product => product.MasterProductId == id);

            if (masterInventory == null) return 0;

            masterInventory.Qty = request.Qty;
            masterInventory.SRP = request.SRP;
            masterInventory.CurrentPrice = request.CurrentPrice;
            masterInventory.PreviousPrice = request.PreviousPrice;

            return await _context.SaveChangesAsync() == 0 ? 0 : 1;
        }

        public async Task<int> UpdateMasterProduct(int id, CreateMasterProduct request)
        {
            if (!Enum.TryParse(GetUserRole(), out Roles acc_role)) return -1;

            MasterProduct? masterProduct = await _context.MasterProducts
                 .FirstOrDefaultAsync(product => product.Id == id);

            if (masterProduct == null) return 0;

            masterProduct.Name = request.Name;
            masterProduct.Description = request.Description;
            masterProduct.LastUpdate = DateTime.Now;

            if (request.ProductImageURLs != null && request.ProductImageURLs.Any())
            {
                var existingImages = _context.ProductCarousels
                    .Where(pc => pc.MasterProduct.Id == masterProduct.Id);

                _context.ProductCarousels.RemoveRange(existingImages);

                foreach (string img in request.ProductImageURLs)
                {
                    ProductCarousel new_carousel = new ProductCarousel
                    {
                        MasterProduct = masterProduct,
                        ProductImageURL = img
                    };
                    _context.ProductCarousels.Add(new_carousel);
                }
            }
            return await _context.SaveChangesAsync() == 0 ? 0 : 1;
        }


        public async Task<PaginatedTableResponse<GetMasterProduct>?> GetAllProductPaginated(GetPaginatedDTO request)
        {
            if (!Enum.TryParse(GetUserRole(), out Roles acc_role)) return null;

            PaginatedTableResponse<GetMasterProduct> response = new PaginatedTableResponse<GetMasterProduct>();
            List<GetMasterProduct> db_results = new List<GetMasterProduct>();

            int count = 0;

            List<MasterProduct> result = await _context.MasterProducts
                 .Include(product => product.MasterInventory)
                 .Include(product => product.ProductCarousels)
                 .OrderBy(product => product.Name)
                 .Skip(request.Skip)
                 .Take(request.Take)
                 .ToListAsync();

            count = await _context.MasterProducts
                 .CountAsync();

            foreach (MasterProduct? products in result)
            {
                db_results.Add(ConvertDTO(products));
                response.ResponseData = db_results;
            };

            response.ResponseData = db_results;
            response.Count = count;

            return response;
        }

        public async Task<PaginatedTableResponse<GetMasterProduct>?> SearchProduct(GetPaginatedDTO request)
        {
            if (!Enum.TryParse(GetUserRole(), out Roles acc_role)) return null;

            PaginatedTableResponse<GetMasterProduct> response = new PaginatedTableResponse<GetMasterProduct>();
            List<GetMasterProduct> db_results = new List<GetMasterProduct>();

            List<MasterProduct> masterProducts = await _context.MasterProducts
             .Include(product => product.MasterInventory)
             .Include(product => product.ProductCarousels)
             .Where(product => product.Id.ToString().Contains(request.SearchValue)
                 || product.Name.Contains(request.SearchValue)
                 || product.MasterInventory.Qty.ToString().Contains(request.SearchValue)
                 || product.MasterInventory.SRP.ToString().Contains(request.SearchValue))
             .ToListAsync();

            foreach (MasterProduct? product in masterProducts)
            {
                db_results.Add(ConvertDTO(product));
                response.ResponseData = db_results;
            };

            response.ResponseData = db_results;
            response.Count = db_results.Count;

            return response;
        }

        public async Task<int> EnableProduct(int id)
        {
            if (!Enum.TryParse(GetUserRole(), out Roles acc_role)) return -1;

            MasterProduct? masterProduct = await _context.MasterProducts
               .FirstOrDefaultAsync(product => product.Id == id);

            if (masterProduct == null) return 0;

            masterProduct.IsActive = true;
            return await _context.SaveChangesAsync() == 0 ? 0 : 1;
        }

        public async Task<int> DisableProduct(int id)
        {
            if (!Enum.TryParse(GetUserRole(), out Roles acc_role)) return -1;

            MasterProduct? masterProduct = await _context.MasterProducts
               .FirstOrDefaultAsync(product => product.Id == id);

            if (masterProduct == null) return 0;

            masterProduct.IsActive = false;
            return await _context.SaveChangesAsync() == 0 ? 0 : 1;
        }

    }
}
