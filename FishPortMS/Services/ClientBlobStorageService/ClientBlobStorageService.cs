using Microsoft.AspNetCore.Components.Forms;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace FishPortMS.Services.ClientBlobStorageService
{
    public class ClientBlobStorageService : IClientBlobStorageService
    {
        private readonly HttpClient _http;

        public ClientBlobStorageService(HttpClient http)
        {
            _http = http;
        }
        public async Task<string> UploadAvatar(IBrowserFile payload)
        {
            long max_file_size = 1024 * 1024 * 5;
            using var content = new MultipartFormDataContent();

            var fileContent = new StreamContent(payload.OpenReadStream(max_file_size));
            fileContent.Headers.ContentType = new MediaTypeHeaderValue(payload.ContentType);

            content.Add(
                content: fileContent,
                name: "\"files\"",
                fileName: payload.Name);

            var response = await _http.PostAsync("api/BlobStorage/upload-avatar", content);
            if (response.IsSuccessStatusCode)
            {
                string data = await response.Content.ReadAsStringAsync();
                return data;
            }

            return string.Empty;
        }

        public async Task<List<string>> UploadProductCarousel(MultipartFormDataContent payload)
        {
            var response = await _http.PostAsync("api/BlobStorage/upload-product-carousel", payload);

            List<string> data = new List<string>();
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadFromJsonAsync<List<string>>();
                if (content != null) data = content;
            }

            return data;
        }
    }
}
