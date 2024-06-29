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
        [AllowAnonymous] 
        [HttpPost]
        public ActionResult UploadImage([FromForm] IFormFile image)
        {
            _imageService.UploadImage(image);

            return Ok();

        }
        [HttpDelete]
        public ActionResult DeleteImage([FromBody] string imagePath)
        {
            _imageService.DeleteImage(imagePath);

            return NoContent();

        }
    }
}
