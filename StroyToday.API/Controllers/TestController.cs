using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StroyToday.API.Models.Auth;
using StroyToday.Application.Interfaces;
using StroyToday.Application.Interfaces.IServices;
using StroyToday.Application.Services;
using StroyToday.Common.Auth;

namespace StroyToday.API.Controllers
{
    [ApiController]
    [Route("Test")]
    public class TestController : Controller
    {
        private readonly AuthenticationHelper _authHelper;
        private readonly ISkillCategoryService _skillCategoryService;

        public TestController(AuthenticationHelper authHelper, ISkillCategoryService skillCategoryService)
        {
            _authHelper = authHelper;
            _skillCategoryService = skillCategoryService;
        }

        [HttpGet("tiktak")]
        public async Task<IResult> TikTak()
        {
            var res = "Her tebe";

            var userId = _authHelper.GetUserId(HttpContext);

            return Results.Ok(res);
        }

        [HttpPost("add-skill")]
        public async Task<IActionResult> AddSkill(string name)
        {
            await _skillCategoryService.Add(name);
            return Ok();
        }
    }
}
