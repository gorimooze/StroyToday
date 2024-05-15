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
    public class PortfolioForUserCVRepository : IPortfolioForUserCVRepository
    {
        private readonly StroyTodayDbContext _context;

        public PortfolioForUserCVRepository(StroyTodayDbContext context)
        {
            _context = context;
        }

        public async Task Add(PortfolioForUserCVDto portfolioForUserCvDto)
        {
            var portfolioForUserCVEntity = new PortfolioForUserCV()
            {
                ImageName = portfolioForUserCvDto.ImageName,
                UserCVId = portfolioForUserCvDto.UserCVId
            };

            await _context.Portfolios.AddRangeAsync(portfolioForUserCVEntity);
            await _context.SaveChangesAsync();
        }
    }
}
