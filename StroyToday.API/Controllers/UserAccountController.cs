using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StroyToday.API.Models.PortfolioForUser;
using StroyToday.Application.Interfaces.IServices;
using StroyToday.Common.Auth;
using StroyToday.Core.IRepositories;

namespace StroyToday.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/user-account")]
    public class UserAccountController : Controller
    {
        private readonly AuthenticationHelper _authHelper;
        private readonly ISkillCategoryService _skillCategoryService;
        private readonly IPortfolioForUserService _portfolioForUserService;
        private readonly IUserCvService _userCvService;

        public UserAccountController(
            AuthenticationHelper authenticationHelper,
            ISkillCategoryService skillCategoryService, 
            IPortfolioForUserService portfolioForUserService, 
            IUserCvService userCvService)
        {
            _authHelper = authenticationHelper;
            _skillCategoryService = skillCategoryService;
            _portfolioForUserService = portfolioForUserService;
            _userCvService = userCvService;
        }

        [HttpGet("get-all-skill-categories")]
        public async Task<IActionResult> GetAllSkillCategories()
        {
            var list = await _skillCategoryService.GetAll();

            return Ok(list);
        }

        [HttpPost("add-user-cv")]
        public async Task<IActionResult> AddUserCv(UserCvRequest userCvRequest)
        {
            var userId = int.Parse(_authHelper.GetUserId(HttpContext));

            var resultPortfolio = await _portfolioForUserService.Add(userCvRequest.Images, userId);

            if (!resultPortfolio.IsSuccess)
            {
                return BadRequest(new { errorMessage = resultPortfolio.Message });
            }

            await _userCvService.Add(userCvRequest.Description, userId);

            await _skillCategoryService.AddUserToSkillCategory(userCvRequest.SkillCategoryIds, userId);

            return Ok();
        }

        [HttpGet("profile")]
        public async Task<IActionResult> Profile()
        {
            var userId = int.Parse(_authHelper.GetUserId(HttpContext));

            var profileResponse = new ProfileResponse();

            var listUserSkills = await _skillCategoryService.GetAllByUserId(userId);
            profileResponse.SkillCategories = listUserSkills;

            var listUrlsImageResponse = await _portfolioForUserService.GetImageUrlsByUserId(userId);

            if (!listUrlsImageResponse.IsSuccess)
            {
                return BadRequest(new { errorMessage = listUrlsImageResponse.ErrorMessage });
            }

            profileResponse.ImageUrls = listUrlsImageResponse.Result;

            var userCvResult = await _userCvService.GetByUserId(userId);

            if (!userCvResult.IsSuccess)
            {
                return BadRequest(new { errorMessage = userCvResult.ErrorMessage });
            }

            profileResponse.Description = userCvResult.Result.Description;

            return Ok(profileResponse);
        }
    }
}
