using Flower.Contract.Services.Interface;
using Flower.Core;
using Flower.Core.APIResponse;
using Flower.ModelViews.OrderDetailModelViews;
using Flower.ModelViews.OrderModelViews;
using Microsoft.AspNetCore.Mvc;

namespace FlowerBE.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderDetailsController : ControllerBase
    {
        private readonly IOrderDetailService _orderDetailService;

        public OrderDetailsController(IOrderDetailService orderDetailService)
        {
            _orderDetailService = orderDetailService;
        }

        /// <summary>
        /// Get order detail by ID
        /// </summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResult<OrderDetailModelView>>> GetById(int id)
        {
            try
            {
                var result = await _orderDetailService.GetOrderDetailByIdAsync(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiErrorResult<object>(ex.Message));
            }
        }

        /// <summary>
        /// Get all order details (optionally filter by userId and orderId)
        /// </summary>
        [HttpGet("get-all")]
        public async Task<ActionResult<ApiResult<List<OrderDetailModelView>>>> GetAll([FromQuery] int? userId, [FromQuery] int? orderId)
        {
            try
            {
                var result = await _orderDetailService.GetAllOrderDetailsAsync(userId, orderId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiErrorResult<object>(ex.Message));
            }
        }

        /// <summary>
        /// Get paginated order details (optionally filter by userId and orderId)
        /// </summary>
        [HttpGet("paging")]
        public async Task<ActionResult<ApiResult<BasePaginatedList<OrderDetailModelView>>>> GetPaging(
            [FromQuery] int pageNumber,
            [FromQuery] int pageSize,
            [FromQuery] int? userId,
            [FromQuery] int? orderId)
        {
            try
            {
                var result = await _orderDetailService.GetAllOrdersPaginatedAsync(pageNumber, pageSize, userId, orderId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiErrorResult<object>(ex.Message));
            }
        }
    }
}
