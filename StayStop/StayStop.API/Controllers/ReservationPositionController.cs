using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StayStop.BLL.Dtos.ReservationPosition;
using StayStop.BLL.Dtos.Room;
using StayStop.BLL.IService;
using StayStop.Model;

namespace StayStop.API.Controllers
{
    [Route("api/reservation/{reservationId}/reservationPosition")]
    [ApiController]
    [Authorize]
    public class ReservationPositionController : ControllerBase
    {
        private readonly IReservationPositionService _reservationPositionService;

        public ReservationPositionController(IReservationPositionService reservationPositionService)
        {
            _reservationPositionService = reservationPositionService;
        }

        [HttpGet("{reservationPositionId}")]
        public ActionResult<ReservationPositionResponseDto> GetById([FromRoute] int reservationId, [FromRoute] int reservationPositionId)
        {
            var resPosition = _reservationPositionService.GetById(reservationId, reservationPositionId);

            return Ok(resPosition);
        }
        [HttpGet]
        public ActionResult<IEnumerable<ReservationPositionResponseDto>> GetReservationPositions([FromRoute] int reservationId)
        {
            var resPositions = _reservationPositionService.GetReservationPositions(reservationId);

            return Ok(resPositions);
        }
        [HttpPost]
        public ActionResult Create([FromRoute] int reservationId, [FromBody] ReservationPositionRequestDto resPositionDto)
        {
            var newResPositionId = _reservationPositionService.Create(reservationId, resPositionDto);

            return Created($"/api/reservation/{reservationId}/reservationPosition/{newResPositionId}", null);
        }
        [HttpDelete("{reservationPositionId}")]
        public ActionResult DeleteReservationPosition([FromRoute] int reservationId, [FromRoute] int reservationPositionId)
        {
            _reservationPositionService.DeleteReservationPosition(reservationId, reservationPositionId);
            return NoContent();
        }
    }
}
