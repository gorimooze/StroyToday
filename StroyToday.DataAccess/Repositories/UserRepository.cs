using StroyToday.Core.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using StroyToday.DataAccess.Models;
using StroyToday.Core.IRepositories;

namespace StroyToday.DataAccess.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly StroyTodayDbContext _context;

        public UserRepository(StroyTodayDbContext context)
        {
            _context = context;
        }

        public async Task Add(UserDto user)
        {
            var userEntity = new User()
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                PasswordHash = user.PasswordHash
            };

            await _context.Users.AddAsync(userEntity);
            await _context.SaveChangesAsync();
        }

        public async Task<UserDto> GetByEmail(string email)
        {
            var userEntity = await _context.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.Email == email) ?? throw new Exception();

            var userDto = new UserDto()
            {
                Id = userEntity.Id,
                UserName = userEntity.UserName,
                Email = userEntity.Email,
                PasswordHash = userEntity.PasswordHash
            };

            return userDto;
        }
    }
}
