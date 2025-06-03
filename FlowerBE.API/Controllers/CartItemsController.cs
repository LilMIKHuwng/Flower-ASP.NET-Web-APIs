using Flower.Contract.Services.Interface;
using Flower.ModelViews.CartItemModelView;
using Flower.Core.APIResponse;
using Microsoft.AspNetCore.Mvc;
using Flower.Core;

namespace FlowerBE.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartItemsController : ControllerBase
    {
        private readonly ICartItemService _cartItemService;

        public CartItemsController(ICartItemService cartItemService)
        {
            _cartItemService = cartItemService;
        }

        /// <summary>
        /// Create a new cart item
        /// </summary>
        [HttpPost("create")]
        public async Task<ActionResult<ApiResult<object>>> Create([FromBody] CreateCartItemModelView model)
        {
            try
            {
                var result = await _cartItemService.AddCartItemAsync(model);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiErrorResult<object>(ex.Message));
            }
        }

        /// <summary>
        /// Get all cart items
        /// </summary>
        [HttpGet("get-all")]
        public async Task<ActionResult<ApiResult<List<CartItemModelView>>>> GetAll()
        {
            try
            {
                var result = await _cartItemService.GetAllCartItems();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiErrorResult<object>(ex.Message));
            }
        }

        /// <summary>
        /// Get paginated cart items
        /// </summary>
        [HttpGet("paging")]
        public async Task<ActionResult<ApiResult<BasePaginatedList<CartItemModelView>>>> GetPaging(
            [FromQuery] int pageNumber,
            [FromQuery] int pageSize,
            [FromQuery] int? userId,
            [FromQuery] int? flowerId)
        {
            try
            {
                var result = await _cartItemService.GetAllCartItemsAsync(pageNumber, pageSize, userId, flowerId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiErrorResult<object>(ex.Message));
            }
        }

        /// <summary>
        /// Get a cart item by ID
        /// </summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResult<CartItemModelView>>> GetById(int id)
        {
            try
            {
                var result = await _cartItemService.GetCartItemByIdAsync(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiErrorResult<object>(ex.Message));
            }
        }

        /// <summary>
        /// Update a cart item
        /// </summary>
        [HttpPut("update/{id}")]
        public async Task<ActionResult<ApiResult<object>>> Update(int id, [FromBody] UpdateCartItemModelView model)
        {
            try
            {
                var result = await _cartItemService.UpdateCartItemAsync(id, model);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiErrorResult<object>(ex.Message));
            }
        }

        /// <summary>
        /// Delete a cart item by ID
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<ActionResult<ApiResult<object>>> Delete(int id)
        {
            try
            {
                var result = await _cartItemService.DeleteCartItemAsync(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiErrorResult<object>(ex.Message));
            }
        }

        /// <summary>
        /// Get all cart items by user ID
        /// </summary>
        [HttpGet("user/{userId}")]
        public async Task<ActionResult<ApiResult<List<CartItemModelView>>>> GetByUserId(int userId)
        {
            try
            {
                var result = await _cartItemService.GetCartItemsByUserIdAsync(userId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiErrorResult<object>(ex.Message));
            }
        }

        /// <summary>
        /// Delete all cart items by user ID
        /// </summary>
        [HttpDelete("user/{userId}")]
        public async Task<ActionResult<ApiResult<object>>> DeleteByUserId(int userId)
        {
            try
            {
                var result = await _cartItemService.DeleteCartItemsByUserIdAsync(userId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiErrorResult<object>(ex.Message));
            }
        }
    }
}
