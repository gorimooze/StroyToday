using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StroyToday.Application.Helpers;
using StroyToday.Core.Dto;

namespace StroyToday.Application.Interfaces.IServices
{
    public interface IOrderService
    {
        Task Add(OrderDto orderDto);
        Task<GenericResult<OrderDto>> GetById(int orderId);
        Task<GenericResult<IList<OrderDto>>> GetAll();
    }
}
