using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StayStop.BLL.Dtos.Reservation;
using StayStop.BLL.IService;
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
        [HttpGet]
        public ActionResult Create() 
        {
            var newReservationId = _reservationService.Create();

            return Created($"/api/reservation/{newReservationId}", null);
        }
        [HttpGet("{reservationId}/user/{userId}")]
        public ActionResult<ReservationResponseDto> GetUserReservationById([FromRoute] int reservationId, [FromRoute] int userId)
        {
            var reservation = _reservationService.GetUserReservationById(userId, reservationId);

            return Ok(reservation);
        }
        [HttpGet("/user/{userId}")]
        public ActionResult<IEnumerable<ReservationResponseDto>> GetUserReservations([FromRoute] int userId)
        {
            var reservations = _reservationService.GetUserReservations(userId);

            return Ok(reservations);
        }
        [HttpGet]
        public ActionResult<IEnumerable<ReservationResponseDto>> GetAll()
        {
            var reservations = _reservationService.GetAll();

            return Ok(reservations);
        }
        [HttpDelete("{reservationId}/user/{userId}")]
        public ActionResult DeleteUserReservationById ([FromRoute] int reservationId, [FromRoute] int userId)
        {
            _reservationService.DeleteById(userId, reservationId);

            return NoContent();
        }
    }
}
