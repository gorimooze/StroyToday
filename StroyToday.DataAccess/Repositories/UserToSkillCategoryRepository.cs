using StroyToday.Core.IRepositories;
using Microsoft.EntityFrameworkCore;
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

        public async Task<IList<SkillCategoryDto>> GetAllByUserId(int userId)
        {
            var list = await (from utsc in _context.UserToSkillCategories
                join sc in _context.SkillCategories on utsc.SkillCategoryId equals sc.Id
                where utsc.UserId == userId
                select new SkillCategoryDto
                {
                    Id = sc.Id,
                    Name = sc.Name
                }).ToListAsync();

            return list;
        }
    }
}
