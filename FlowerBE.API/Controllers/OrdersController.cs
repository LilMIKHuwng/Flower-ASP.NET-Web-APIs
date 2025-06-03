using Flower.Contract.Services.Interface;
using Flower.Core.APIResponse;
using Microsoft.AspNetCore.Mvc;
using Flower.Core;
using Flower.ModelViews.OrderModelViews;

namespace FlowerBE.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrdersController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        /// <summary>
        /// Create a new order
        /// </summary>
        [HttpPost("create")]
        public async Task<ActionResult<ApiResult<object>>> Create([FromBody] CreateOrderModelView model)
        {
            try
            {
                var result = await _orderService.CreateOrderFromCartAsync(model);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiErrorResult<object>(ex.Message));
            }
        }

        /// <summary>
        /// Get all orders (optionally filter by userId and status)
        /// </summary>
        [HttpGet("get-all")]
        public async Task<ActionResult<ApiResult<List<OrderModelView>>>> GetAll([FromQuery] int? userId, [FromQuery] string? status)
        {
            try
            {
                var result = await _orderService.GetAllOrdersAsync(userId, status);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiErrorResult<object>(ex.Message));
            }
        }

        /// <summary>
        /// Get paginated orders (optionally filter by userId and status)
        /// </summary>
        [HttpGet("paging")]
        public async Task<ActionResult<ApiResult<BasePaginatedList<OrderModelView>>>> GetPaging(
            [FromQuery] int pageNumber,
            [FromQuery] int pageSize,
            [FromQuery] int? userId,
            [FromQuery] string? status)
        {
            try
            {
                var result = await _orderService.GetAllOrdersPaginatedAsync(pageNumber, pageSize, userId, status);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiErrorResult<object>(ex.Message));
            }
        }

        /// <summary>
        /// Get order by ID
        /// </summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResult<OrderModelView>>> GetById(int id)
        {
            try
            {
                var result = await _orderService.GetOrderByIdAsync(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiErrorResult<object>(ex.Message));
            }
        }
    }
}
