using Microsoft.AspNetCore.Components;
using MudBlazor;
using FishPortMS.Shared.DTOs.MasterProductDTO;
using FishPortMS.Shared.Models.Products;
using FishPortMS.Shared.Response;
using System.Net.Http.Json;
using FishPortMS.Utilities;

namespace FishPortMS.Services.ClientMasterProductService
{
    public class ClientMasterProductService : IClientMasterProductService
    {
        private readonly HttpClient _http;
        private readonly NavigationManager _navigationManager;
        private readonly ModifiedSnackBar _modifiedSnackBar;

        public ClientMasterProductService(HttpClient http, NavigationManager navigationManager, ModifiedSnackBar modifiedSnackBar)
        {
            _http = http;
            _navigationManager = navigationManager;
            _modifiedSnackBar = modifiedSnackBar;
        }

        public List<GetMasterProduct> getMasterProducts { get; set; } = new List<GetMasterProduct>();
        public List<ProductCategory> getCategories { get; set; } = new List<ProductCategory>();

        public async Task GetAllMasterProducts()
        {
            HttpResponseMessage? response = await _http.GetAsync($"api/masterproduct/all-products");

            if (!response.IsSuccessStatusCode) getMasterProducts = new List<GetMasterProduct>();

            List<GetMasterProduct>? content = await response.Content.ReadFromJsonAsync<List<GetMasterProduct>>();
            if (content != null && content.Count > 0) getMasterProducts = content;
        }
        public async Task GetAllCategories()
        {
            HttpResponseMessage? response = await _http.GetAsync("api/masterproduct/all-categories");

            if (!response.IsSuccessStatusCode) getCategories = new List<ProductCategory>();

            List<ProductCategory>? content = await response.Content.ReadFromJsonAsync<List<ProductCategory>>();
            if (content != null && content.Count > 0) getCategories = content;
        }

        public async Task<int> CreateMasterProduct(CreateMasterProduct payload)
        {
            HttpResponseMessage? response = await _http.PostAsJsonAsync("api/masterproduct/add-product", payload);

            if (response.IsSuccessStatusCode)
            {
                int response_data = await response.Content.ReadFromJsonAsync<int>();
                if (response_data == 0) return 0;
                _modifiedSnackBar.ShowMessage("Product Created Successfully", Severity.Success);
                _navigationManager.NavigateTo("all-products");
                return response_data;
            }
            else
            {
                _modifiedSnackBar.ShowMessage("Failed to Create Product", Severity.Error);
                _navigationManager.NavigateTo("all-products");
                return 0;
            }
        }

        public async Task<List<GetMasterProduct>> GetAllLowStocks()
        {
            var result = await _http.GetFromJsonAsync<List<GetMasterProduct>>("api/masterproduct/all-low-stocks");

            if (result != null)
            {
                getMasterProducts = result;
            }
            return getMasterProducts;
        }

        public async Task<GetMasterProduct?> GetSingleMasterProduct(int id)
        {
            HttpResponseMessage? response = await _http.GetAsync($"api/masterproduct/{id}");
            if (response.StatusCode == System.Net.HttpStatusCode.NotFound) return null;

            return await response.Content.ReadFromJsonAsync<GetMasterProduct?>();
        }

        public async Task<int> UpdateMasterInventory(int Id, UpdateMasterInventory payload)
        {
            HttpResponseMessage? response = await _http.PutAsJsonAsync($"api/masterproduct/update-inventory/{Id}", payload);

            if (response.IsSuccessStatusCode)
            {
                int response_data = await response.Content.ReadFromJsonAsync<int>();
                if (response_data == 0) return 0;
                _modifiedSnackBar.ShowMessage("Inventory Updated Successfully", Severity.Success);
                _navigationManager.NavigateTo("all-inventories");
                return response_data;
            }
            else
            {
                _modifiedSnackBar.ShowMessage("Failed to Update Inventory", Severity.Error);
                _navigationManager.NavigateTo("all-inventories");
                return 0;
            }
        }

        public async Task<int> UpdateMasterProduct(int Id, CreateMasterProduct payload)
        {
            HttpResponseMessage? response = await _http.PutAsJsonAsync($"api/masterproduct/update-product/{Id}", payload);
            if (response.IsSuccessStatusCode)
            {
                int response_data = await response.Content.ReadFromJsonAsync<int>();
                if (response_data == 0) return 0;
                _modifiedSnackBar.ShowMessage("Product Updated Successfully", Severity.Success);
                _navigationManager.NavigateTo("all-products");
                return response_data;
            }
            else
            {
                _modifiedSnackBar.ShowMessage("Failed to Update Product", Severity.Error);
                _navigationManager.NavigateTo("all-products");
                return 0;
            }
        }

        public async Task<PaginatedTableResponse<GetMasterProduct>> GetAllProductPaginated(GetPaginatedDTO payload)
        {
            PaginatedTableResponse<GetMasterProduct>? result = await _http.GetFromJsonAsync<PaginatedTableResponse<GetMasterProduct>>($"api/masterproduct/all-product-paginated?Take={payload.Take}&Skip={payload.Skip}");

            if (result != null) return result;

            return new PaginatedTableResponse<GetMasterProduct>();
        }

        public async Task<PaginatedTableResponse<GetMasterProduct>> SearchProduct(GetPaginatedDTO payload)
        {
            PaginatedTableResponse<GetMasterProduct>? result = await _http.GetFromJsonAsync<PaginatedTableResponse<GetMasterProduct>>($"api/masterproduct/search-product?SearchValue={payload.SearchValue}");

            if (result != null) return result;

            return new PaginatedTableResponse<GetMasterProduct>();
        }

        public async Task<int> EnableProduct(int id)
        {
            HttpResponseMessage? response = await _http.PutAsync($"api/masterproduct/enable-product/{id}", null);
            if (response.IsSuccessStatusCode)
            {
                int response_data = await response.Content.ReadFromJsonAsync<int>();
                if (response_data == 0) return 0;
                _modifiedSnackBar.ShowMessage("Product Successfully Enabled", Severity.Success);
                return response_data;
            }
            else
            {
                _modifiedSnackBar.ShowMessage("Failed to Enable Product", Severity.Error);
                return 0;
            }
        }

        public async Task<int> DisableProduct(int id)
        {
            HttpResponseMessage? response = await _http.PutAsync($"api/masterproduct/disable-product/{id}", null);
            if (response.IsSuccessStatusCode)
            {
                int response_data = await response.Content.ReadFromJsonAsync<int>();
                if (response_data == 0) return 0;
                _modifiedSnackBar.ShowMessage("Product Successfully Disabled", Severity.Success);
                _navigationManager.NavigateTo("all-products");
                return response_data;
            }
            else
            {
                _modifiedSnackBar.ShowMessage("Failed to Disable Product", Severity.Error);
                _navigationManager.NavigateTo("all-products");
                return 0;
            }
        }




    }
}
