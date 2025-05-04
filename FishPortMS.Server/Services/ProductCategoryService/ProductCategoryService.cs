using Microsoft.EntityFrameworkCore;
using FishPortMS.Server.Data;
using FishPortMS.Shared.DTOs.MasterProductDTO;
using FishPortMS.Shared.Enums;
using FishPortMS.Shared.Models.Products;
using FishPortMS.Shared.Response;
using System.Security.Claims;

namespace FishPortMS.Server.Services.ProductCategoryService
{
    public class ProductCategoryService : IProductCategoryService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly DataContext _context;

        public ProductCategoryService(IHttpContextAccessor httpContextAccessor, DataContext context)
        {
            _httpContextAccessor = httpContextAccessor;
            _context = context;
        }

        private string? GetUserRole() => _httpContextAccessor.HttpContext?.User
          .FindFirstValue(ClaimTypes.Role);

        public async Task<int> CreateProductCategory(CreateProductCategoryDTO request)
        {
            if (!Enum.TryParse(GetUserRole(), out Roles acc_role)) return -1;

            if (await _context.ProductCategories.AnyAsync(category => category.Title == request.Title)) return 0;

            ProductCategory productCategory = new ProductCategory
            {
                Title = request.Title
            };

            _context.ProductCategories.Add(productCategory);

            return await _context.SaveChangesAsync() == 0 ? 0 : 1;
        }

        public async Task<int> UpdateProductCategory(int id, CreateProductCategoryDTO request)
        {
            if (!Enum.TryParse(GetUserRole(), out Roles acc_role)) return -1;

            ProductCategory? productCategory = await _context.ProductCategories
                 .FirstOrDefaultAsync(category => category.Id == id);

            if (productCategory == null) return 0;

            productCategory.Title = request.Title;

            return await _context.SaveChangesAsync() == 0 ? 0 : 1;
        }

        public async Task<ProductCategory?> GetSingleProductCategory(int id)
        {
            if (!Enum.TryParse(GetUserRole(), out Roles acc_role)) return null;

            ProductCategory? productCategory = await _context.ProductCategories
                .FirstOrDefaultAsync(category => category.Id == id);

            if (productCategory == null) return null;

            return productCategory;
        }

        public async Task<PaginatedTableResponse<ProductCategory>?> GetAllProductCategoryPaginated(GetPaginatedDTO request)
        {
            if (!Enum.TryParse(GetUserRole(), out Roles acc_role)) return null;

            PaginatedTableResponse<ProductCategory> response = new PaginatedTableResponse<ProductCategory>();

            List<ProductCategory> productCategory = await _context.ProductCategories
                .OrderByDescending(category => category.Id)
                .ToListAsync();

            response.ResponseData = productCategory;
            response.Count = productCategory.Count;

            return response;
        }

        public async Task<PaginatedTableResponse<ProductCategory>?> SearchProductCategory(GetPaginatedDTO request)
        {
            if (!Enum.TryParse(GetUserRole(), out Roles acc_role)) return null;

            PaginatedTableResponse<ProductCategory> response = new PaginatedTableResponse<ProductCategory>();

            List<ProductCategory> productCategory = await _context.ProductCategories
                .Where(category => category.Title.Contains(request.SearchValue))
                .OrderByDescending(category => category.Id)
                .ToListAsync();

            response.ResponseData = productCategory;
            response.Count = productCategory.Count;

            return response;
        }
    }
}
