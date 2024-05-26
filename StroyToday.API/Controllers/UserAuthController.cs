using Microsoft.AspNetCore.Mvc;
using StroyToday.API.Models.Auth;
using StroyToday.Application.Interfaces.IServices;

namespace StroyToday.API.Controllers
{
    [ApiController]
    [Route("api/user-auth")]
    public class UserAuthController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserAuthController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("register")]
        public async Task<IResult> Register(RegisterUserRequest request)
        {
            await _userService.Register(request.UserName, request.Email, request.Password);

            return Results.Ok();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginUserRequest request)
        {
            var result = await _userService.Login(request.Email, request.Password);

            if (!result.IsSuccess)
            {
                return BadRequest(new { errorMessage = result.ErrorMessage });
            }

            var token = result.Result.Token;

            HttpContext.Response.Cookies.Append("auth-token", token);
            HttpContext.Response.Cookies.Append("time-zone", request.TimeZone);

            return Ok(new { username = result.Result.UserName });
        }

        [HttpPost("logout")]
        public IActionResult Logout()
        {
            HttpContext.Response.Cookies.Delete("auth-token");

            return Ok(new { message = "Вы успешно вышли из системы" });
        }
    }
}
