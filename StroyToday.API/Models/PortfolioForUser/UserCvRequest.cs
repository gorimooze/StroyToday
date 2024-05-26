using StroyToday.Core.Dto;

namespace StroyToday.API.Models.PortfolioForUser
{
    public class UserCvRequest
    {
        public string Description { get; set; }
        public IList<int> SkillCategoryIds { get; set; }
        public IList<ImageDto> Images { get; set; }
    }
}
