using MudBlazor;
using FishPortMS.Shared.Response;
using System.Net.Http.Json;
using FishPortMS.Utilities;
using FishPortMS.Shared.DTOs.ConsignacionDTO;
using Blazored.LocalStorage;

namespace FishPortMS.Services.ClientConsignacionService
{
    public class ClientConsignacionService : IClientConsignacionService
    {
        private readonly HttpClient _http;
        private readonly ModifiedSnackBar _modifiedSnackBar;
        private readonly ILocalStorageService _localStorageService;


        public ClientConsignacionService(HttpClient http, ModifiedSnackBar modifiedSnackBar, ILocalStorageService localStorageService)
        {
            _http = http;
            _modifiedSnackBar = modifiedSnackBar;
            _localStorageService = localStorageService;
        }

        public List<GetRenewalDetailsDTO> getRenewalDetails { get; set; } = new List<GetRenewalDetailsDTO>();
        public List<GetConsignacionPin> getConsignacionPin { get; set; } = new List<GetConsignacionPin>();
        public List<GetConsignacionDTO> getConsignacion { get; set; } = new List<GetConsignacionDTO>();

        public async Task<int> AddMoreFranchiseConsignacion(Guid userId, CreateConsignacionDTO payload)
        {
            HttpResponseMessage? response = await _http.PostAsJsonAsync($"api/consignacion/add-more-consignacion/{userId}", payload);

            if (response.IsSuccessStatusCode)
            {
                int response_data = await response.Content.ReadFromJsonAsync<int>();
                if (response_data == 0) return 0;
                _modifiedSnackBar.ShowMessage("Consignacion Created Successfully", Severity.Success);
                return response_data;
            }
            else
            {
                _modifiedSnackBar.ShowMessage("Consignacion Name is already in use.", Severity.Error);
                return 0;
            }      
        }
    
        public async Task<PaginatedTableResponse<GetConsignacionDTO>> GetAllConsignacionesPaginated(GetPaginatedDTO payload)
        {
            PaginatedTableResponse<GetConsignacionDTO>? result = await _http.GetFromJsonAsync<PaginatedTableResponse<GetConsignacionDTO>>($"api/consignacion/all-consignacion-paginated?Take={payload.Take}&Skip={payload.Skip}");

            if (result != null) return result;

            return new PaginatedTableResponse<GetConsignacionDTO>();
        }

        public async Task<PaginatedTableResponse<GetConsignacionDTO>> SearchConsignacion(GetPaginatedDTO payload)
        {
            PaginatedTableResponse<GetConsignacionDTO>? result = await _http.GetFromJsonAsync<PaginatedTableResponse<GetConsignacionDTO>>($"api/consignacion/search-consignacion?SearchValue={payload.SearchValue}");

            if (result != null) return result;

            return new PaginatedTableResponse<GetConsignacionDTO>();
        }

        public async Task<List<GetConsignacionDTO>> GetAllConsignacions()
        {
            var result = await _http.GetFromJsonAsync<List<GetConsignacionDTO>>("api/consignacion/all-consignacions");

            if (result == null) return new List<GetConsignacionDTO>();

            return result;
        }

        public async Task<int> EnableConsignacion(Guid id)
        {
            HttpResponseMessage? response = await _http.PutAsync($"api/consignacion/enable-consignacion/{id}", null);
            if (response.IsSuccessStatusCode)
            {
                int response_data = await response.Content.ReadFromJsonAsync<int>();
                if (response_data == 0) return 0;
                _modifiedSnackBar.ShowMessage("Consignacion Successfully Enabled", Severity.Success);
                return response_data;
            }
            else
            {
                _modifiedSnackBar.ShowMessage("Failed to Enable Consignacion", Severity.Error);
                return 0;
            }
        }

        public async Task<int> DisableConsignacion(Guid id)
        {
            HttpResponseMessage? response = await _http.PutAsync($"api/consignacion/disable-consignacion/{id}", null);
            if (response.IsSuccessStatusCode)
            {
                int response_data = await response.Content.ReadFromJsonAsync<int>();
                if (response_data == 0) return 0;
                _modifiedSnackBar.ShowMessage("Consignacion Successfully Disabled", Severity.Success);
                return response_data;
            }
            else
            {
                _modifiedSnackBar.ShowMessage("Failed to Disable Consignacion", Severity.Error);
                return 0;
            }
        }

        public async Task<int> EditConsignacionGeolocation(Guid id, UpdateConsignacionGeoDTO payload)
        {
            HttpResponseMessage? response = await _http.PutAsJsonAsync($"api/consignacion/update-geolocation/{id}", payload);
            if (response.IsSuccessStatusCode)
            {
                int response_data = await response.Content.ReadFromJsonAsync<int>();
                if (response_data == 0) return 0;
                _modifiedSnackBar.ShowMessage("Consignacion Updated Successfully", Severity.Success);
                return response_data;
            }
            else
            {
                _modifiedSnackBar.ShowMessage("Failed to Update Consignacion", Severity.Error);
                return 0;
            }
        }
        public async Task<List<GetConsignacionDTO>> GetRenewalDetails(DateTime month)
        {
            var result = await _http.GetFromJsonAsync<List<GetConsignacionDTO>>($"api/consignacion/all-renewal-details?month={month}");

            if (result != null)
            {
                getConsignacion = result;
            }
            return getConsignacion;
        }

        public async Task<List<GetConsignacionPin>> GetAllConsignacionPin()
        {
            var result = await _http.GetFromJsonAsync<List<GetConsignacionPin>>($"api/consignacion/all-consignacion-pin");

            if (result != null)
            {
                getConsignacionPin = result;
            }
            return getConsignacionPin;
        }

        public async Task<List<GetConsignacionDTO>?> GetAllConsignacionDetails()
        {
            HttpResponseMessage? response = await _http.GetAsync("api/consignacion/all-consignacion-details");
            if (response.StatusCode == System.Net.HttpStatusCode.NotFound) return null;

            return await response.Content.ReadFromJsonAsync<List<GetConsignacionDTO>?>();
        }

    }
}
