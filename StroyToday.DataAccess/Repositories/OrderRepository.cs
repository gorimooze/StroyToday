using StroyToday.Core.IRepositories;
using Microsoft.EntityFrameworkCore;
using StroyToday.Core.Dto;
using StroyToday.DataAccess.Models;

namespace StroyToday.DataAccess.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly StroyTodayDbContext _context;

        public OrderRepository(StroyTodayDbContext context)
        {
            _context = context;
        }

        public async Task Add(OrderDto orderDto)
        {
            await _context.Orders.AddAsync(new Order()
            {
                Name = orderDto.Name,
                Description = orderDto.Description,
                Price = orderDto.Price,
                CreatedOn = orderDto.CreatedOn,
                UserId = orderDto.UserId
            });

            await _context.SaveChangesAsync();
        }

        public async Task<OrderDto> FindById(int orderId)
        {
            var orderDto = await _context.Orders.Select(x => new OrderDto()
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description,
                Price = x.Price,
                UserId = x.UserId,
                CreatedOn = x.CreatedOn,
                UserName = x.User.UserName
            }).FirstOrDefaultAsync(x => x.Id == orderId);

            if (orderDto == null)
            {
                return null;
            }

            return orderDto;
        }

        public async Task<IList<OrderDto>> GetAll()
        {
            var list = await _context.Orders.Select(x => new OrderDto()
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description,
                Price = x.Price,
                UserId = x.UserId,
                CreatedOn = x.CreatedOn,
                UserName = x.User.UserName
            }).OrderByDescending(x => x.Id).ToListAsync();

            return list;
        }
    }
}
