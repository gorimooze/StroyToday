using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StroyToday.Application.Helpers;
using StroyToday.Core.Dto;
using StroyToday.Core.IRepositories;
using StroyToday.Application.Interfaces.IServices;

namespace StroyToday.Application.Services
{
    public class SkillCategoryService : ISkillCategoryService
    {
        private readonly ISkillCategoryRepository _skillCategoryRepository;
        private readonly IUserToSkillCategoryRepository _userToSkillCategoryRepository;

        public SkillCategoryService(ISkillCategoryRepository skillCategoryRepository, IUserToSkillCategoryRepository userToSkillCategoryRepository)
        {
            _skillCategoryRepository = skillCategoryRepository;
            _userToSkillCategoryRepository = userToSkillCategoryRepository;
        }

        public async Task Add(string name)
        {
            var skillCategoryDto = new SkillCategoryDto()
            {
                Name = name
            };

            await _skillCategoryRepository.Add(skillCategoryDto);
        }

        public async Task<IList<SkillCategoryDto>> GetAll()
        {
            var list = await _skillCategoryRepository.GetAll();

            return list;
        }

        public async Task<IList<SkillCategoryDto>> GetAllByUserId(int userId)
        {
            var list = await _userToSkillCategoryRepository.GetAllByUserId(userId);
            return list;
        }

        public async Task AddUserToSkillCategory(IList<int> skillCategoryIds, int userId)
        {
            foreach (var skillId in skillCategoryIds)
            {
                var userToSkillDto = new UserToSkillCategoryDto()
                {
                    SkillCategoryId = skillId,
                    UserId = userId
                };
                await _userToSkillCategoryRepository.Add(userToSkillDto);
            }
        }
    }
}
