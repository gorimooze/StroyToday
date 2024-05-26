using StroyToday.Core.Dto;

namespace StroyToday.API.Models.PortfolioForUser
{
    public class ProfileResponse
    {
        public IList<string> ImageUrls { get; set; }
        public string Description { get; set; }
        public IList<SkillCategoryDto> SkillCategories { get; set; }
    }
}
