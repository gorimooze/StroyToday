using Microsoft.EntityFrameworkCore;
using StroyToday.Core.IRepositories;
using StroyToday.Core.Dto;
using StroyToday.DataAccess.Models;

namespace StroyToday.DataAccess.Repositories
{
    public class PortfolioForUserRepository : IPortfolioForUserRepository
    {
        private readonly StroyTodayDbContext _context;

        public PortfolioForUserRepository(StroyTodayDbContext context)
        {
            _context = context;
        }

        public async Task Add(PortfolioForUserDto portfolioForUserDto)
        {
            var portfolioForUserEntity = new PortfolioForUser()
            {
                ImageName = portfolioForUserDto.ImageName,
                UserId = portfolioForUserDto.UserId
            };

            await _context.Portfolios.AddRangeAsync(portfolioForUserEntity);
            await _context.SaveChangesAsync();
        }

        public async Task<IList<string>> GetImagesByUserId(int userId)
        {
            var list = await _context.Portfolios.Where(x => x.UserId == userId).Select(x => x.ImageName).ToListAsync();
            return list;
        }
    }
}
