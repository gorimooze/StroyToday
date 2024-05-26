﻿using StroyToday.Application.Interfaces.IServices;
using Microsoft.Extensions.Caching.Distributed;
using StroyToday.Application.Helpers;
using StroyToday.Core.Dto;
using StroyToday.Core.IRepositories;
using System.Text.Json;

namespace StroyToday.Application.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IDistributedCache _cache;
        private const string CacheKey = "orderList";

        public OrderService(IOrderRepository orderRepository, IDistributedCache cache)
        {
            _orderRepository = orderRepository;
            _cache = cache;
        }

        public async Task Add(OrderDto orderDto)
        {
            orderDto.CreatedOn = DateTime.UtcNow;

            await _orderRepository.Add(orderDto);

            // Очистка кэша, так как данные изменились
            await _cache.RemoveAsync(CacheKey);
        }

        public async Task<GenericResult<OrderDto>> GetById(int orderId)
        {
            var orderDto = await _orderRepository.FindById(orderId);

            if (orderDto == null)
            {
                return new GenericResult<OrderDto>()
                {
                    IsSuccess = false,
                    ErrorMessage = "Not found Order"
                };
            }

            return new GenericResult<OrderDto>()
            {
                Result = orderDto,
                IsSuccess = true
            };

        }

        public async Task<GenericResult<IList<OrderDto>>> GetAll()
        {
            try
            {
                //Попробуем получить данные из кэша
                var cachedOrderList = await _cache.GetStringAsync(CacheKey);
                if (!string.IsNullOrEmpty(cachedOrderList))
                {
                    var cachedOrders = JsonSerializer.Deserialize<IList<OrderDto>>(cachedOrderList);
                    return new GenericResult<IList<OrderDto>>()
                    {
                        Result = cachedOrders,
                        IsSuccess = true
                    };
                }

                // Если данных в кэше нет, получаем из базы данных
                var list = await _orderRepository.GetAll();

                // Кэшируем данные
                var listJson = JsonSerializer.Serialize(list);
                await _cache.SetStringAsync(CacheKey, listJson);

                return new GenericResult<IList<OrderDto>>()
                {
                    Result = list,
                    IsSuccess = true
                };
            }
            catch (Exception e)
            {
                return new GenericResult<IList<OrderDto>>()
                {
                    IsSuccess = true,
                    ErrorMessage = e.Message
                };
            }
        }
    }
}