using Flower.Core.APIResponse;
using Flower.Core;
using Flower.ModelViews.OrderModelViews;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Flower.ModelViews.OrderDetailModelViews;

namespace Flower.Contract.Services.Interface
{
    public interface IOrderDetailService
    {
        Task<ApiResult<OrderDetailModelView>> GetOrderDetailByIdAsync(int Id);
        Task<ApiResult<List<OrderDetailModelView>>> GetAllOrderDetailsAsync(int? userId, int? orderId);
        Task<ApiResult<BasePaginatedList<OrderDetailModelView>>> GetAllOrdersPaginatedAsync(
            int pageNumber, int pageSize, int? userId, int? orderId);
    }
}
