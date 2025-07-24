namespace FishPortMS.Server.Services.BlobStorageService
{
    public interface IBlobStorageService
    {
        Task<string> UploadAvatar(IFormFile request);
        public Task<List<string>> UploadProductCarousel(IEnumerable<IFormFile> files);
    }
}
