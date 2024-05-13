using Microsoft.AspNetCore.Mvc;
using StroyToday.API.Models.Auth;
using StroyToday.Application.Services;
using StroyToday.Common.Auth;

namespace StroyToday.API.Controllers
{
    [ApiController]
    [Route("UserAuth")]
    public class UserAuthController : ControllerBase
    {
        private readonly UserService _userService;

        public UserAuthController(UserService userService)
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
                return BadRequest(result.Result);
            }

            var token = result.Result;

            HttpContext.Response.Cookies.Append("auth-token", token);

            return Ok();
        }
    }
}
