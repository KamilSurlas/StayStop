using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StayStop.BLL.Dtos.Hotel;
using StayStop.BLL.Dtos.Hotel.HotelOpinion;
using StayStop.BLL.IService;
using StayStop.BLL.Pagination;
using StayStop.Model;
using StayStop.Model.Constants;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace StayStop.API.Controllers
{
    [Route("api/hotel")]
    [ApiController]
    [Authorize]
    public class HotelController : ControllerBase
    {
        private readonly IHotelService _hotelService;

        public HotelController(IHotelService hotelService)
        {
            _hotelService = hotelService;
        }
        [HttpGet]
        [AllowAnonymous]
        public ActionResult<IEnumerable<HotelResponseDto>> GetAll([FromQuery] HotelPagination pagination)
        {
            var hotels = _hotelService.GetAll(pagination);

            return Ok(hotels);
        }
        [HttpGet("{hotelId}")]
        [AllowAnonymous]
        public ActionResult<HotelResponseDto> GetById([FromRoute] int hotelId)
        {
            var hotel = _hotelService.GetById(hotelId);

            return Ok(hotel);
        }
        [HttpGet("{hotelId}/opinion")]
        [AllowAnonymous]
        public ActionResult<HotelOpinionResponseDto> GetHotelOpinion([FromRoute] int hotelId)
        {
            var hotelOpinion = _hotelService.GetOpinion(hotelId);

            return Ok(hotelOpinion);
        }
        [HttpPost]
        [Authorize(Roles = UserRole.HotelOwner)]
        public ActionResult Create([FromBody] HotelRequestDto hotelDto)
        {
            var id = _hotelService.Create(hotelDto);
            return Created($"/api/hotel/{id}", new { newId = id });
        }
        [HttpDelete("{hotelId}")]
        public ActionResult DeleteById([FromRoute] int hotelId)
        {
            _hotelService.Delete(hotelId);

            return NoContent();
        }
        [HttpPut("{hotelId}")]
        public ActionResult Update([FromRoute] int hotelId, [FromBody] HotelUpdateRequestDto hotelDto)
        {
            _hotelService.Update(hotelId, hotelDto);

            return Ok();
        }
        [HttpPost("{hotelId}/managers")]
        public ActionResult AddManager([FromRoute] int hotelId, [FromBody] string managerEmail)
        {
            _hotelService.AddManager(hotelId,managerEmail);

            return Ok();
        }
        [HttpDelete("{hotelId}/managers/remove/{userId}")]
        public ActionResult RemoveManager([FromRoute] int hotelId, [FromRoute] int userId)
        {
            _hotelService.RemoveManager(hotelId, userId);

            return Ok();
        }
        [HttpGet("{hotelId}/managers")]
        public ActionResult GetManagers([FromRoute] int hotelId)
        {
            var managers = _hotelService.GetManagers(hotelId);

            return Ok(managers);
        }
        [HttpDelete("{hotelId}/images")]
        public ActionResult DeleteImage([FromRoute] int hotelId, [FromBody] string path)
        {
            _hotelService.DeleteImage(hotelId, path);

            return NoContent();
        }
       
    }
}
