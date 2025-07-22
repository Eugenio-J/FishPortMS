using Microsoft.AspNetCore.Components.Forms;

namespace FishPortMS.Services.ClientBlobStorageService
{
    public interface IClientBlobStorageService
    {
        Task<string> UploadAvatar(IBrowserFile payload);
        Task<List<string>> UploadProductCarousel(MultipartFormDataContent payload);

    }
}
