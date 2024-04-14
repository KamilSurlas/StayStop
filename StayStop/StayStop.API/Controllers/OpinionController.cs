using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StayStop.BLL.Dtos.Opinion;
using StayStop.BLL.IService;
using StayStop.Model;

namespace StayStop.API.Controllers
{
    [Route("api/reservation/{reservationId}/opinion")]
    [ApiController]
    [Authorize]
    public class OpinionController : ControllerBase
    {
        private readonly IOpinionService _opinionService;

        public OpinionController(IOpinionService opinionService)
        {
            _opinionService = opinionService;
        }

        [HttpPost]
        public ActionResult Create([FromRoute] int reservationId, [FromBody] OpinionRequestDto opinionDto)
        {
            var newOpinionId = _opinionService.Create(reservationId, opinionDto);


            return Created($"api/reservation/{reservationId}/opinion/{newOpinionId}", null);
        }
        [HttpGet]
        [AllowAnonymous]
        public ActionResult<OpinionResponseDto> GetByReservationId([FromRoute] int reservationId)
        {
            var opinion = _opinionService.GetByReservationId(reservationId);

            return Ok(opinion);
        }
        [HttpDelete]
        public ActionResult Delete([FromRoute] int reservationId)
        {
            _opinionService.Delete(reservationId);
            return NoContent();
        }
        [HttpPut]
        public ActionResult Update([FromRoute] int reservationId, [FromBody] OpinionUpdateRequestDto opinionDto)
        {
            _opinionService.Update(reservationId, opinionDto);

            return Ok();
        }
    }
}
