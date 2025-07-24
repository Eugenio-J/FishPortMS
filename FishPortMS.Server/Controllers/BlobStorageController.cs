using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using FishPortMS.Server.Services.BlobStorageService;

namespace FishPortMS.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlobStorageController : ControllerBase
    {
        private readonly IBlobStorageService _blobStorageService;

        public BlobStorageController(IBlobStorageService BlobStorageService)
        {
            _blobStorageService = BlobStorageService;
        }

        [HttpPost("upload-avatar")]
        public async Task<IActionResult> UploadAvatar(IFormFile files)
        {
            string result = await _blobStorageService.UploadAvatar(files);
            if (string.IsNullOrEmpty(result)) return BadRequest("Something went wrong. Pls try again.");
            return Ok(result);
        }

        [HttpPost("upload-product-carousel")]
        public async Task<ActionResult<List<string>>> UploadProductCarousel([FromForm] IEnumerable<IFormFile> files)
        {
            List<string> response = await _blobStorageService.UploadProductCarousel(files);
            return Ok(response);
        }
    }
}
