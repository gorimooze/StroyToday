using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StroyToday.API.Models.PortfolioForUser;
using StroyToday.Application.Interfaces.IServices;
using StroyToday.Common.Auth;

namespace StroyToday.API.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/portfolio")]
    public class PortfolioController : ControllerBase
    {
        private readonly AuthenticationHelper _authHelper;
        private readonly IPortfolioForUserService _portfolioForUserService;

        public PortfolioController(AuthenticationHelper authHelper, IPortfolioForUserService portfolioForUserService)
        {
            _authHelper = authHelper;
            _portfolioForUserService = portfolioForUserService;
        }

        [HttpPost("add-image")]
        public async Task<IActionResult> AddImage(PortfolioAddRequest request)
        {
            var userId = int.Parse(_authHelper.GetUserId(HttpContext));

            var result = await _portfolioForUserService.Add(request.Base64, request.ImageType, userId);

            if (!result.IsSuccess)
            {
                return BadRequest(new { errorMessage = result.Message });
            }

            return Ok();
        } 
    }
}
