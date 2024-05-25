using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StroyToday.Core.Dto;
using StroyToday.Core.IRepositories;
using StroyToday.Application.Interfaces.IServices;

namespace StroyToday.Application.Services
{
    public class SkillCategoryService : ISkillCategoryService
    {
        private readonly ISkillCategoryRepository _skillCategoryRepository;

        public SkillCategoryService(ISkillCategoryRepository skillCategoryRepository)
        {
            _skillCategoryRepository = skillCategoryRepository;
        }

        public async Task Add(string name)
        {
            var skillCategoryDto = new SkillCategoryDto()
            {
                Name = name
            };

            await _skillCategoryRepository.Add(skillCategoryDto);
        }
    }
}
