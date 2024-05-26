using StroyToday.Application.Helpers;
using StroyToday.Core.Dto;

namespace StroyToday.Application.Interfaces.IServices
{
    public interface ISkillCategoryService
    {
        Task Add(string name);
        Task<IList<SkillCategoryDto>> GetAll();
        Task<IList<SkillCategoryDto>> GetAllByUserId(int userId);
        Task AddUserToSkillCategory(IList<int> skillCategoryIds, int userId);
    }
}
