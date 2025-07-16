using FishPortMS.Shared.DTOs.ReceiptDTO;
using FishPortMS.Shared.Response;
using FishPortMS.Utilities;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using System.Net.Http.Json;

namespace FishPortMS.Services.ClientReceiptService
{
    public class ClientReceiptService : IClientReceiptService
    {
        private readonly HttpClient _http;
        private readonly NavigationManager _navigationManager;
        private readonly ModifiedSnackBar _modifiedSnackBar;
        public ClientReceiptService(HttpClient http, NavigationManager navigationManager, ModifiedSnackBar modifiedSnackBar)
        {
            _http = http;
            _navigationManager = navigationManager;
            _modifiedSnackBar = modifiedSnackBar;   
        }

        public async Task<int> CreateReceipt(CreateReceiptDTO payload)
        {
             HttpResponseMessage? response = await _http.PostAsJsonAsync($"api/receipt/create-receipt", payload);

            if (response.IsSuccessStatusCode)
            {   
                int response_data = await response.Content.ReadFromJsonAsync<int>();
                if (response_data == 0) return 0;
                _modifiedSnackBar.ShowMessage("Receipt Created Successfully", Severity.Success);
                return response_data;
            }
            else
            {
                _modifiedSnackBar.ShowMessage("Failed to Create Receipt", Severity.Error);
                return 0;
            }
        }

        public async Task<PaginatedTableResponse<GetReceiptDTO>> GetAllReceiptPaginated(GetPaginatedDTO payload)
        {
            var result = await _http.GetFromJsonAsync<PaginatedTableResponse<GetReceiptDTO>>($"api/receipt/get-receipt-paginated?Take={payload.Take}&Skip={payload.Skip}");

            if (result != null) return result;

            return new PaginatedTableResponse<GetReceiptDTO>();
        }

        public async Task<GetReceiptDTO> GetSingleReceipt(int receiptId)
        {
            HttpResponseMessage? response = await _http.GetAsync($"api/receipt/single-receipt?receiptId={receiptId}");
            if (response.StatusCode == System.Net.HttpStatusCode.NotFound) return null;

            return await response.Content.ReadFromJsonAsync<GetReceiptDTO?>();
        }

        public async Task<PaginatedTableResponse<GetReceiptDTO>> SearchReceipt(GetPaginatedDTO payload)
        {
            PaginatedTableResponse<GetReceiptDTO>? result = await _http.GetFromJsonAsync<PaginatedTableResponse<GetReceiptDTO>>($"api/receipt/search-receipt?SearchValue={payload.SearchValue}");

            if (result != null) return result;

            return new PaginatedTableResponse<GetReceiptDTO>();
        }

        public async Task<int> UpdateReceipt(int Id, CreateReceiptDTO payload)
        {
            HttpResponseMessage? response = await _http.PutAsJsonAsync($"api/receipt/update-receipt/{Id}", payload);

            if (response.IsSuccessStatusCode)
            {
                int response_data = await response.Content.ReadFromJsonAsync<int>();
                if (response_data == 0) return 0;
                _modifiedSnackBar.ShowMessage("Receipt Updated Successfully", Severity.Success);
                return response_data;
            }
            else
            {
                _modifiedSnackBar.ShowMessage("Failed to Update Receipt", Severity.Error);
                return 0;
            }
        }
    }
}
