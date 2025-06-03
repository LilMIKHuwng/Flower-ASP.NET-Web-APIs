using Flower.Core.APIResponse;
using Flower.Core;
using Flower.ModelViews.CartItemModelView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flower.Contract.Services.Interface
{
    public interface ICartItemService
    {
        Task<ApiResult<BasePaginatedList<CartItemModelView>>> GetAllCartItemsAsync(int pageNumber, int pageSize, int? userId, int? flowerId);
        Task<ApiResult<object>> AddCartItemAsync(CreateCartItemModelView model);
        Task<ApiResult<object>> UpdateCartItemAsync(int id, UpdateCartItemModelView model);
        Task<ApiResult<object>> DeleteCartItemAsync(int id);
        Task<ApiResult<CartItemModelView>> GetCartItemByIdAsync(int id);
        Task<ApiResult<List<CartItemModelView>>> GetAllCartItems();
        Task<ApiResult<List<CartItemModelView>>> GetCartItemsByUserIdAsync(int userId);
        Task<ApiResult<object>> DeleteCartItemsByUserIdAsync(int userId);
    }

}
