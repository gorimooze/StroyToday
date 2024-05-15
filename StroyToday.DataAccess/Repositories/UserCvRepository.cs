using StroyToday.Core.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using StroyToday.Core.Dto;
using StroyToday.DataAccess.Models;

namespace StroyToday.DataAccess.Repositories
{
    public class UserCvRepository : IUserCvRepository
    {
        private readonly StroyTodayDbContext _context;

        public UserCvRepository(StroyTodayDbContext context)
        {
            _context = context;
        }

        public async Task Add(UserCvDto userCvDto)
        {
            var userCvEntity = new UserCV()
            {
                Description = userCvDto.Description,
                UserId = userCvDto.UserId
            };

            await _context.UserCVs.AddAsync(userCvEntity);
            await _context.SaveChangesAsync();
        }

        public async Task<UserCvDto> GetById(int userCvId)
        {
            var userCvEntity = await _context.UserCVs
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.Id == userCvId);

            if (userCvEntity == null)
            {
                return null;
            }

            var userCvDto = new UserCvDto()
            {
                Id = userCvEntity.Id,
                Description = userCvEntity.Description,
                UserId = userCvEntity.UserId
            };

            return userCvDto;
        }
    }
}
