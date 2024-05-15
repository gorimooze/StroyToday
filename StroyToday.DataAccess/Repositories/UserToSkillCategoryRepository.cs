using StroyToday.Core.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StroyToday.Core.Dto;
using StroyToday.DataAccess.Models;

namespace StroyToday.DataAccess.Repositories
{
    public class UserToSkillCategoryRepository : IUserToSkillCategoryRepository
    {
        private readonly StroyTodayDbContext _context;

        public UserToSkillCategoryRepository(StroyTodayDbContext context)
        {
            _context = context;
        }

        public async Task Add(UserToSkillCategoryDto userToSkillCategoryDto)
        {
            var userToSkillCategoryEntity = new UserToSkillCategory()
            {
                SkillCategoryId = userToSkillCategoryDto.SkillCategoryId,
                UserId = userToSkillCategoryDto.UserId
            };

            await _context.UserToSkillCategories.AddRangeAsync(userToSkillCategoryEntity);
            await _context.SaveChangesAsync();
        }
    }
}
