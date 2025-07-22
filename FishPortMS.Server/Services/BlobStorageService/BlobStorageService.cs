using Azure;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;

namespace FishPortMS.Server.Services.BlobStorageService
{
    public class BlobStorageService : IBlobStorageService
    {
        private readonly BlobServiceClient _blobServiceClient;
        private BlobContainerClient _client;
        public static readonly List<string> ImageExtensions = new List<string> { ".JPG", ".JPEG", ".PNG" };

        public BlobStorageService(BlobServiceClient blobServiceClient)
        {
            _blobServiceClient = blobServiceClient;
            _client = _blobServiceClient.GetBlobContainerClient("fishportmsimg");
        }

        public string BlobContainer()
        {
            return "fishportmsimg";
        }

        public async Task<string> UploadAvatar(IFormFile request)
        {
            DateTime date = DateTime.Now;
            string formattedDate = date.ToString("yyyy-MM-dd-HH-mm-ss");

            string extension = Path.GetExtension(request.FileName);
            string filename_for_storage = Guid.NewGuid().ToString() + "-" + formattedDate + extension;

            var response = await _client.UploadBlobAsync("/avatar/" + filename_for_storage, request.OpenReadStream());

            if (response.GetRawResponse().Status == 201)
            {
                return $"https://echoblazorstorage.blob.core.windows.net/{BlobContainer()}/avatar/{filename_for_storage}";
            }
            return string.Empty;
        }

        public async Task<List<string>> UploadProductCarousel(IEnumerable<IFormFile> files)
        {
            List<string> image_urls = new List<string>();

            foreach (var file in files)
            {
                string extension = Path.GetExtension(file.FileName);
                string filename_for_storage = "master-product-" + Guid.NewGuid().ToString() + extension;

                //upload to azure
                Response<BlobContentInfo>? azure_response = await _client.UploadBlobAsync($"/master-product/" + filename_for_storage, file.OpenReadStream());

                // if the upload fails go to next iteration
                if (azure_response.GetRawResponse().Status != 201) continue;

                string result = $"https://echoblazorstorage.blob.core.windows.net/{BlobContainer()}/master-product/{filename_for_storage}";

                image_urls.Add(result);
            }

            return image_urls;
        }
    }
}
