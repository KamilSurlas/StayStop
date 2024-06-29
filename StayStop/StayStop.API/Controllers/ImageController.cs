using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StayStop.BLL.IService;
using StayStop.Model;

namespace StayStop.API.Controllers
{
    [Route("api/images")]
    [Authorize]
    [ApiController]
    public class ImageController : ControllerBase
    {
        private readonly IImageService _imageService;

        public ImageController(IImageService imageService)
        {
          _imageService = imageService;
        }
       
        [HttpPost]
        [AllowAnonymous]
        public ActionResult UploadImage([FromForm] IFormFile image)
        {
            var path = _imageService.UploadImage(image);

            return Ok(path);

        }
        [HttpPost("multiple")]
        [AllowAnonymous]
        public ActionResult UploadImages([FromForm] IEnumerable<IFormFile> images)
        {
            var paths = _imageService.UploadImages(images);

            return Ok(paths);

        }
        [HttpDelete]
        public ActionResult DeleteImage([FromBody] string imagePath)
        {
            _imageService.DeleteImage(imagePath);

            return NoContent();

        }
    }
}
