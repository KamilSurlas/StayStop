using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StayStop.BLL.Dtos.Room;
using StayStop.BLL.IService;
using StayStop.BLL_EF.Service;
using StayStop.Model.Constants;

namespace StayStop.API.Controllers
{
    [Route("api/hotel/{hotelId}/room")]
    [ApiController]
    [Authorize(Roles = UserRole.Admin + "," + UserRole.Manager+ "," + UserRole.HotelOwner)]
    public class RoomController : ControllerBase
    {
        private readonly IRoomService _roomService;

        public RoomController(IRoomService roomService)
        {
            _roomService = roomService;
        }
        [HttpPost]
        public ActionResult Create([FromRoute] int hotelId, [FromBody] RoomRequestDto roomDto)
        {
            var newRoomId = _roomService.Create(hotelId, roomDto);

            return Created($"/api/hotel/{hotelId}/room/{newRoomId}", null);
        }
        [HttpGet]
        [AllowAnonymous]
        public ActionResult<List<RoomResponseDto>> GetAll([FromRoute] int hotelId)
        {
            var rooms = _roomService.GetAll(hotelId);
            return Ok(rooms);
        }
        [HttpGet("{roomId}")]
        [AllowAnonymous]
        public ActionResult<RoomResponseDto> GetById([FromRoute] int hotelId, [FromRoute] int roomId)
        {
            RoomResponseDto room = _roomService.GetById(hotelId, roomId);
            return Ok(room);
        }
        [HttpDelete]
        public ActionResult DeleteAll([FromRoute] int hotelId)
        {
            _roomService.DeleteAll(hotelId);

            return NoContent();
        }
        [HttpDelete("{roomId}")]
        public ActionResult DeleteById([FromRoute] int hotelId, [FromRoute] int roomId)
        {
            _roomService.DeleteById(hotelId,roomId);
            return NoContent();
        }
        [HttpPut("{roomId}")]
        public ActionResult Update([FromRoute] int hotelId, [FromRoute] int roomId, [FromBody] RoomUpdateRequestDto roomDto)
        {
            _roomService.Update(hotelId, roomId, roomDto);

            return Ok();
        }
        [HttpPatch("{roomId}")]
        public ActionResult SetRoomActivity([FromRoute] int hotelId, [FromRoute] int roomId)
        {
            _roomService.SetRoomActivity(hotelId, roomId);

            return Ok();
        }
        [AllowAnonymous]
        [HttpPost("{roomId}/images/cover")]
        public ActionResult UploadCoverImage([FromRoute]int roomId,[FromRoute] int hotelId, [FromForm] IFormFile coverImage)
        {
            _roomService.UploadCoverImage(hotelId,roomId, coverImage);

            return Ok();

        }
        [AllowAnonymous]
        [HttpPost("{roomId}/images/all")]
        public ActionResult UploadImage([FromRoute]int roomId, [FromRoute] int hotelId, [FromForm] IEnumerable<IFormFile> images)
        {
            _roomService.UploadImages(hotelId,roomId, images);

            return Ok();
        }
        [HttpDelete("{roomId}/images/all")]
        public ActionResult DeleteImage([FromRoute] int roomId, [FromRoute] int hotelId, [FromBody] string imagePath)
        {
            _roomService.DeleteImage(hotelId, roomId, imagePath);

            return Ok();
        }

    }
}
