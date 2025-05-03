using MudBlazor;
using FishPortMS.Shared.DTOs.UserManagementDTO;
using FishPortMS.Shared.Response;
using System.Data;
using System.Net.Http.Json;
using FishPortMS.Utilities;

namespace FishPortMS.Services.ClientUserManagementService
{
    public class ClientUserManagementService : IClientUserManagementService
    {
        private readonly HttpClient _http;
        private readonly ModifiedSnackBar _modifiedSnackBar
;

        public ClientUserManagementService(HttpClient http, ModifiedSnackBar modifiedSnackBar)
        {
            _http = http;
            _modifiedSnackBar = modifiedSnackBar;
        }
      
        public async Task<PaginatedTableResponse<GetUsersDTO>?> GetAllUsersPaginated(GetPaginatedDTO payload)
        {
            PaginatedTableResponse<GetUsersDTO>? result = await _http.GetFromJsonAsync<PaginatedTableResponse<GetUsersDTO>>($"api/usermanagement/all-users-paginated?Take={payload.Take}&Skip={payload.Skip}&UserRoles={payload.Roles}");

            if (result != null) return result;

            return new PaginatedTableResponse<GetUsersDTO>();
        }

        public async Task<PaginatedTableResponse<GetUsersDTO>?> SearchUser(GetPaginatedDTO payload)
        {
            PaginatedTableResponse<GetUsersDTO>? result = await _http.GetFromJsonAsync<PaginatedTableResponse<GetUsersDTO>>($"api/usermanagement/search-users?SearchValue={payload.SearchValue}&UserRoles={payload.Roles}");

            if (result != null) return result;

            return new PaginatedTableResponse<GetUsersDTO>();
        }

        public async Task<int> DisableAccount(Guid userId)
        {
            HttpResponseMessage? response = await _http.PutAsync($"api/usermanagement/disable-acc/{userId}", null);

            if (response.IsSuccessStatusCode)
            {
                int response_data = await response.Content.ReadFromJsonAsync<int>();
                if (response_data == 0) return 0;

                _modifiedSnackBar.ShowMessage("Account Successfully Disabled", Severity.Success);
                return response_data;
            }
            else
            {
                _modifiedSnackBar.ShowMessage("Failed to Disable Account", Severity.Error);
                return 0;
            }
        }

        public async Task<int> EnableAccount(Guid userId)
        {
            HttpResponseMessage? response = await _http.PutAsync($"api/usermanagement/enable-acc/{userId}", null);

            if (response.IsSuccessStatusCode)
            {
                int response_data = await response.Content.ReadFromJsonAsync<int>();
                if (response_data == 0) return 0;
                _modifiedSnackBar.ShowMessage("Account Successfully Enabled", Severity.Success);
                return response_data;
            }
            else
            {
                _modifiedSnackBar.ShowMessage("Failed to Enable Account", Severity.Error);
                return 0;
            }
        }

    }
}
