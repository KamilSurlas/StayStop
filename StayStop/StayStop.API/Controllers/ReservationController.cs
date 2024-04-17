using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StayStop.BLL.Dtos.Reservation;
using StayStop.BLL.IService;
using StayStop.BLL.Pagination;
using StayStop.Model;

namespace StayStop.API.Controllers
{
    [Route("api/reservation")]
    [ApiController]
    [Authorize]
    public class ReservationController : ControllerBase
    {
        private readonly IReservationService _reservationService;

        public ReservationController(IReservationService reservationService)
        {
            _reservationService = reservationService;
        }
        [HttpPost]
        public ActionResult Create([FromBody] ReservationRequestDto reservationDto) 
        {
            var newReservationId = _reservationService.Create(reservationDto);

            return Created($"/api/reservation/{newReservationId}", null);
        }
        [HttpGet("/api/user/{userId}/reservation/{reservationId}")]
        public ActionResult<ReservationResponseDto> GetUserReservationById([FromRoute] int reservationId, [FromRoute] int userId)
        {
            var reservation = _reservationService.GetUserReservationById(userId, reservationId);

            return Ok(reservation);
        }
        [HttpGet("/api/user/{userId}/reservation")]
        public ActionResult<IEnumerable<ReservationResponseDto>> GetUserReservations([FromRoute] int userId)
        {
            var reservations = _reservationService.GetUserReservations(userId);

            return Ok(reservations);
        }
        [HttpGet]
        public ActionResult<IEnumerable<ReservationResponseDto>> GetAll([FromQuery] HotelPagination pagination)
        {
            var reservations = _reservationService.GetAll(pagination);

            return Ok(reservations);
        }
        [HttpDelete("{reservationId}")]
        public ActionResult DeleteUserReservationById ([FromRoute] int reservationId)
        {
            _reservationService.DeleteById( reservationId);

            return NoContent();
        }
    }
}
