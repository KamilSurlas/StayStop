using Microsoft.AspNetCore.Mvc;
using StayStop.BLL.Dtos.User;
using StayStop.BLL.IService;

namespace StayStop.API.Controllers
{
    [Route("api/account")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;
        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }
        [HttpPost("register")]
        public ActionResult RegisterUser([FromBody] UserRegisterDto dto)
        {
            _accountService.RegisterUser(dto);
            return Ok();
        }
        [HttpPost("login")]
        public ActionResult Login([FromBody] UserLoginDto dto)
        {
            string token = _accountService.GetJwtToken(dto);
            return Ok(token);
        }
    }
}
