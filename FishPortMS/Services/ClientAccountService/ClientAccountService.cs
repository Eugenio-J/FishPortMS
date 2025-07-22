using FishPortMS.Client.Response;
using FishPortMS.Shared.DTOs.AccountDTO;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using Blazored.LocalStorage;
using System.Net.Http.Json;
using System.Net;
using FishPortMS.Utilities;

namespace FishPortMS.Services.ClientAccountService
{
    public class ClientAccountService : IClientAccountService
    {
        private HttpClient _http;
        private readonly NavigationManager _navigationManager;
        private readonly ModifiedSnackBar _modifiedSnackbar;
        private readonly ILocalStorageService _localStorageService;

        public ClientAccountService(
            HttpClient http,
            NavigationManager navigationManager,
            ModifiedSnackBar modifiedSnackbar,
            ILocalStorageService localStorageService
        )
        {
            _http = http;
            _navigationManager = navigationManager;
            _modifiedSnackbar = modifiedSnackbar;
            _localStorageService = localStorageService;
        }

        public Token Token { get; set; } = new Token();

        public async Task<string> GetSingleUserAvatar()
        {
            var result = await _http.GetStringAsync("api/account/get-user-avatar");
            return result;
        }

        public async Task<UpdateProfileDTO?> GetSingleUser()
        {
            // if provided an Id that does not exist GetAsync returns null
            var result = await _http.GetAsync($"api/account/get-single-user");
            if (result.StatusCode == HttpStatusCode.OK)
            {
                return await result.Content.ReadFromJsonAsync<UpdateProfileDTO>();
            }
            return null;
        }

        public async Task<int> UpdateUser(UpdateProfileDTO request)
        {
            HttpResponseMessage? response = await _http.PutAsJsonAsync($"api/account/update-user", request);

            if (response.IsSuccessStatusCode)
            {
                int response_data = await response.Content.ReadFromJsonAsync<int>();
                if (response_data == 0) return 0;
                _modifiedSnackbar.ShowMessage("User has been update successfully", Severity.Success);
                return response_data;
            }
            else
            {
                _modifiedSnackbar.ShowMessage("Failed to update user", Severity.Error);
                return 0;
            }
        }

        private async Task<string> SetToken(HttpResponseMessage message)
        {
            if (message.IsSuccessStatusCode)
            {
                Token.value = await message.Content.ReadAsStringAsync();
                return "success";
            }
            else
            {
                return await message.Content.ReadAsStringAsync();
            }
        }

        public async Task<string> LoginAsync(LoginDTO request)
        {
            var result = await _http.PostAsJsonAsync("api/account/login", request);

            var data = await SetToken(result);

            return data;
        }

        public async Task<string> RefreshToken()
        {
            var result = await _http.PostAsync("api/account/refresh-token", null);

            string data = await SetToken(result);
            return data;
        }

        public async Task<int> Register(RegisterDTO request)
        {
            HttpResponseMessage? response = await _http.PostAsJsonAsync($"api/account/register", request);

            if (response.IsSuccessStatusCode)
            {
                int response_data = await response.Content.ReadFromJsonAsync<int>();
                if (response_data == 0) return 0;
                return response_data;
            }
            else
            {
                return 0;
            }
        }

        public async Task<string> Logout()
        {
            var result = await _http.PostAsync("api/account/logout", null);
            string data = await SetToken(result);
            return data;
        }

        public async Task<int> ChangePassword(ChangePassDTO payload)
        {
            HttpResponseMessage? response = await _http.PutAsJsonAsync("api/account/change-pass", payload);

            if (response.IsSuccessStatusCode)
            {
                int response_data = await response.Content.ReadFromJsonAsync<int>();
                if (response_data == 0) return 0;
                _modifiedSnackbar.ShowMessage("Password Updated Successfully", Severity.Success);
                return response_data;
            }
            else
            {
                _modifiedSnackbar.ShowMessage("Failed to Update Password", Severity.Error);
                return 0;
            }
        }

        public async Task<int> ForgotPass(ForgotPasswordDTO payload)
        {
            HttpResponseMessage? response = await _http.PostAsJsonAsync("api/account/forgot-pass", payload);

            if (response.IsSuccessStatusCode)
            {
                int response_data = await response.Content.ReadFromJsonAsync<int>();
                if (response_data == 0) return 0;
                _modifiedSnackbar.ShowMessage("Password Updated Successfully", Severity.Success);
                _navigationManager.NavigateTo("login");
                return response_data;
            }
            else
            {
                _modifiedSnackbar.ShowMessage("Failed to Update Password", Severity.Error);
                _navigationManager.NavigateTo("login");
                return 0;
            }
        }

        public async Task<int> VerifyCode(VerifyCodeDTO payload)
        {
            HttpResponseMessage? response = await _http.PostAsJsonAsync("api/account/verify-code", payload);

            if (response.IsSuccessStatusCode)
            {
                int response_data = await response.Content.ReadFromJsonAsync<int>();
                if (response_data == 0) return 0;
                _modifiedSnackbar.ShowMessage("Verification Successful", Severity.Success);
                return response_data;
            }
            else
            {
                _modifiedSnackbar.ShowMessage("Invalid Verification Code", Severity.Error);
                return 0;
            }
        }

        public async Task<int> SendVerification(EmailVerificationDTO payload, string userEmail)
        {
            HttpResponseMessage? response = await _http.PostAsJsonAsync($"api/account/send-verification/{userEmail}", payload);

            if (response.IsSuccessStatusCode)
            {
                int response_data = await response.Content.ReadFromJsonAsync<int>();
                if (response_data == 0) return 0;
                _modifiedSnackbar.ShowMessage("Verification Code has been sent.", Severity.Success);
                return response_data;
            }
            else
            {
                _modifiedSnackbar.ShowMessage("Failed to send Verification Code", Severity.Error);
                return 0;
            }
        }
    }
}
