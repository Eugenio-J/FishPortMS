using Microsoft.AspNetCore.Components;
using MudBlazor;
using FishPortMS.Shared.DTOs.MasterProductDTO;
using FishPortMS.Shared.Models.Products;
using FishPortMS.Shared.Response;
using System.Net.Http.Json;
using FishPortMS.Utilities;

namespace FishPortMS.Services.ClientProductCategoryService
{
    public class ClientProductCategoryService : IClientProductCategoryService
    {
        private readonly HttpClient _http;
        private readonly NavigationManager _navigationManager;
        private readonly ModifiedSnackBar _modifiedSnackBar;

        public ClientProductCategoryService(HttpClient http, NavigationManager navigationManager, ModifiedSnackBar modifiedSnackBar)
        {
            _http = http;
            _navigationManager = navigationManager;
            _modifiedSnackBar = modifiedSnackBar;
        }

        public async Task<int> CreateProductCategory(CreateProductCategoryDTO payload)
        {
            HttpResponseMessage? response = await _http.PostAsJsonAsync("api/productcategory/add-category", payload);

            if (response.IsSuccessStatusCode)
            {
                int response_data = await response.Content.ReadFromJsonAsync<int>();
                if (response_data == 0) return 0;
                _modifiedSnackBar.ShowMessage("Category Created Successfully", Severity.Success);
                _navigationManager.NavigateTo("all-categories");
                return response_data;
            }
            else
            {
                _modifiedSnackBar.ShowMessage("Failed to Create Category", Severity.Error);
                _navigationManager.NavigateTo("all-categories");
                return 0;
            }
        }

        public async Task<ProductCategory?> GetSingleProductCategory(int id)
        {
            HttpResponseMessage? response = await _http.GetAsync($"api/productcategory/{id}");
            if (response.StatusCode == System.Net.HttpStatusCode.NotFound) return null;

            return await response.Content.ReadFromJsonAsync<ProductCategory?>();
        }

        public async Task<int> UpdateProductCategory(int id, CreateProductCategoryDTO payload)
        {
            HttpResponseMessage? response = await _http.PutAsJsonAsync($"api/productcategory/update-category/{id}", payload);

            if (response.IsSuccessStatusCode)
            {
                int response_data = await response.Content.ReadFromJsonAsync<int>();
                if (response_data == 0) return 0;
                _modifiedSnackBar.ShowMessage("Category Updated Successfully", Severity.Success);
                _navigationManager.NavigateTo("all-categories");
                return response_data;
            }
            else
            {
                _modifiedSnackBar.ShowMessage("Failed to Update Category", Severity.Error);
                _navigationManager.NavigateTo("all-categories");
                return 0;
            }
        }

        public async Task<PaginatedTableResponse<ProductCategory>> GetAllProductCategoryPaginated(GetPaginatedDTO payload)
        {
            PaginatedTableResponse<ProductCategory>? result = await _http.GetFromJsonAsync<PaginatedTableResponse<ProductCategory>>($"api/productcategory/all-category-paginated?Take={payload.Take}&Skip={payload.Skip}");

            if (result != null) return result;

            return new PaginatedTableResponse<ProductCategory>();
        }

        public async Task<PaginatedTableResponse<ProductCategory>> SearchProductCategory(GetPaginatedDTO payload)
        {
            PaginatedTableResponse<ProductCategory>? result = await _http.GetFromJsonAsync<PaginatedTableResponse<ProductCategory>>($"api/productcategory/search-category?SearchValue={payload.SearchValue}");

            if (result != null) return result;

            return new PaginatedTableResponse<ProductCategory>();
        }
    }
}
