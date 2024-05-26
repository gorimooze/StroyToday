using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StroyToday.Core.Dto;

namespace StroyToday.Core.IRepositories
{
    public interface IOrderRepository
    {
        Task Add(OrderDto orderDto);
        Task<OrderDto> FindById(int orderId);
        Task<IList<OrderDto>> GetAll();
    }
}
