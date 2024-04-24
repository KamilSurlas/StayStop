using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StayStop.BLL.Dtos.Hotel;
using StayStop.BLL.IService;
using StayStop.BLL.Pagination;
using StayStop.Model;
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
        [HttpPost]
        [Authorize(Roles = "HotelOwner")]
        public ActionResult Create([FromBody] HotelRequestDto hotelDto)
        {
            var id = _hotelService.Create(hotelDto);
            return Created($"/api/hotel/{id}", null);
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
        public ActionResult RemoveManager([FromRoute] int hotelId, [FromBody] string managerEmail)
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
    }
}
