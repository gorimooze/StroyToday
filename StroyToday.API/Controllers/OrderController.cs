using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using StroyToday.API.Models.Orders;
using StroyToday.Application.Interfaces.IServices;
using StroyToday.Common.Auth;
using StroyToday.Core.Dto;

namespace StroyToday.API.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/order")]
    public class OrderController : Controller
    {
        private readonly AuthenticationHelper _authHelper;
        private readonly IOrderService _orderService;

        public OrderController(AuthenticationHelper authHelper, IOrderService orderService)
        {
            _authHelper = authHelper;
            _orderService = orderService;
        }

        [HttpPost("add-order")]
        public async Task<IActionResult> AddOrder(OrderRequest orderRequest)
        {
            var userId = int.Parse(_authHelper.GetUserId(HttpContext));
            var orderDto = new OrderDto()
            {
                Name = orderRequest.Name,
                Description = orderRequest.Description,
                Price = orderRequest.Price,
                UserId = userId
            };

            await _orderService.Add(orderDto);

            return Ok();
        }

        [HttpGet("get-by-id")]
        public async Task<IActionResult> GetById(int orderId)
        {
            var timezone = HttpContext.Request.Cookies["time-zone"];

            if (timezone.IsNullOrEmpty())
            {
                return BadRequest(new { errorMessage = "Извините, вам нужно заново авторизоваться" });
            }

            var result = await _orderService.GetById(orderId, timezone);

            if (!result.IsSuccess)
            {
                return BadRequest(new { errorMessage = result.ErrorMessage });
            }

            return Ok(result.Result);
        }

        [HttpGet("get-list")]
        public async Task<IActionResult> GetList()
        {
            var timezone = HttpContext.Request.Cookies["time-zone"];

            if (timezone.IsNullOrEmpty())
            {
                return BadRequest(new { errorMessage = "Извините, вам нужно заново авторизоваться" });
            }

            var result = await _orderService.GetAll(timezone);

            if (!result.IsSuccess)
            {
                return BadRequest(new { errorMessage = result.ErrorMessage });
            }

            return Ok(result.Result);
        }
    }
}
