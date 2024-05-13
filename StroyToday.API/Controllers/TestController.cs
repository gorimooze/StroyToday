using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StroyToday.API.Models.Auth;
using StroyToday.Application.Interfaces;
using StroyToday.Application.Services;
using StroyToday.Common.Auth;

namespace StroyToday.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("Test")]
    public class TestController : Controller
    {
        private readonly AuthenticationHelper _authHelper;

        public TestController(AuthenticationHelper authHelper)
        {
            _authHelper = authHelper;
        }

        [HttpGet("tiktak")]
        public async Task<IResult> TikTak()
        {
            var res = "Her tebe";

            var userId = _authHelper.GetUserId(HttpContext);

            return Results.Ok(res);
        }
    }
}
