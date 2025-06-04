using Flower.Core;
using Flower.Core.APIResponse;
using Flower.ModelViews.OrderModelViews;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flower.Contract.Services.Interface
{
    public interface IOrderService
    {
        Task<ApiResult<object>> CreateOrderFromCartAsync(CreateOrderModelView model);
        Task<ApiResult<OrderModelView>> GetOrderByIdAsync(int Id);
        Task<ApiResult<List<OrderModelView>>> GetAllOrdersAsync(int? userId, string? status);
        Task<ApiResult<BasePaginatedList<OrderModelView>>> GetAllOrdersPaginatedAsync(
            int pageNumber, int pageSize, int? userId, string? status);
        Task<ApiResult<int>> GetLatestOrderIdByUserAsync(int userId);
    }
}
