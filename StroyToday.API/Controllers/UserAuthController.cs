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
        public async Task<IResult> Login(LoginUserRequest request)
        {
            var token = await _userService.Login(request.Email, request.Password);
            var context = HttpContext;

            context.Response.Cookies.Append("auth-token", token);

            return Results.Ok();
        }
    }
}
