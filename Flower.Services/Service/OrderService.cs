using AutoMapper;
using Flower.Contract.Repositories.Entity;
using Flower.Contract.Repositories.Interface;
using Flower.Contract.Services.Interface;
using Flower.Core;
using Flower.Core.APIResponse;
using Flower.ModelViews.OrderModelViews;
using Flower.ModelViews.UserModelViews;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flower.Services.Service
{
    public class OrderService : IOrderService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _contextAccessor;

        public OrderService(IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor contextAccessor)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _contextAccessor = contextAccessor;
        }

        public async Task<ApiResult<object>> CreateOrderFromCartAsync(CreateOrderModelView model)
        {
            var cartRepo = _unitOfWork.GetRepository<CartItem>();
            var orderRepo = _unitOfWork.GetRepository<Order>();
            var orderDetailRepo = _unitOfWork.GetRepository<OrderDetail>();
            var flowerRepo = _unitOfWork.GetRepository<FlowerType>(); // Lấy repo FlowerType

            // Lấy các sản phẩm trong giỏ hàng của người dùng
            var cartItems = await cartRepo.Entities
                .Include(ci => ci.Flower)
                .Where(ci => ci.UserID == model.UserID)
                .ToListAsync();

            if (!cartItems.Any())
            {
                return new ApiErrorResult<object>("Giỏ hàng đang trống.");
            }

            var now = DateTime.Now;

            // Tạo đơn hàng mới
            var order = new Order
            {
                UserID = model.UserID,
                DeliveryAddress = model.DeliveryAddress,
                OrderDate = now,
                Status = "Pending",
                PaymentMethod = "VNPay",
                TotalAmount = 0,
                CreatedBy = model.UserID,
                CreatedTime = now
            };

            await orderRepo.InsertAsync(order);
            await _unitOfWork.SaveAsync(); // Đảm bảo có OrderID trước khi chèn OrderDetail

            foreach (var item in cartItems)
            {
                // Kiểm tra tồn kho đủ không
                if (item.Flower.Stock < item.Quantity)
                {
                    return new ApiErrorResult<object>($"Sản phẩm '{item.Flower.Name}' không đủ tồn kho.");
                }

                var detail = new OrderDetail
                {
                    OrderID = order.Id,
                    FlowerID = item.FlowerID,
                    Quantity = item.Quantity,
                    UnitPrice = item.UnitPrice,
                    TotalPrice = item.UnitPrice * item.Quantity,
                    CreatedBy = model.UserID,
                    CreatedTime = now
                };

                order.TotalAmount += detail.TotalPrice;
                await orderDetailRepo.InsertAsync(detail);
            }

            await orderRepo.UpdateAsync(order);

            // Xóa các mục trong giỏ hàng
            foreach (var item in cartItems)
            {
                await cartRepo.DeleteAsync(item.Id);
            }

            await _unitOfWork.SaveAsync();

            return new ApiSuccessResult<object>("Tạo đơn hàng thành công.");
        }



        public async Task<ApiResult<OrderModelView>> GetOrderByIdAsync(int id)
        {
            var order = await _unitOfWork.GetRepository<Order>().Entities
                .Include(o => o.User)
                .FirstOrDefaultAsync(o => o.Id == id);

            if (order == null)
                return new ApiErrorResult<OrderModelView>("Không tìm thấy đơn hàng.");

            var result = _mapper.Map<OrderModelView>(order);

            result.User = _mapper.Map<UserModelView>(order.User);

            return new ApiSuccessResult<OrderModelView>(result);
        }

        public async Task<ApiResult<List<OrderModelView>>> GetAllOrdersAsync(int? userId, string? status)
        {
            var query = _unitOfWork.GetRepository<Order>().Entities
                .Include(o => o.User)
                .AsQueryable();

            if (userId.HasValue)
                query = query.Where(o => o.UserID == userId.Value);

            if (!string.IsNullOrWhiteSpace(status))
                query = query.Where(o => o.Status.Contains(status));

            var orders = await query.OrderByDescending(o => o.CreatedTime).ToListAsync();
            var result = _mapper.Map<List<OrderModelView>>(orders);

            for (int i = 0; i < result.Count; i++)
            {
                result[i].User = _mapper.Map<UserModelView>(orders[i].User);
            }

            return new ApiSuccessResult<List<OrderModelView>>(result);
        }

        public async Task<ApiResult<BasePaginatedList<OrderModelView>>> GetAllOrdersPaginatedAsync(int pageNumber, int pageSize, int? userId, string? status)
        {
            var query = _unitOfWork.GetRepository<Order>().Entities
                .Include(o => o.User)
                .AsQueryable();

            if (userId.HasValue)
                query = query.Where(o => o.UserID == userId.Value);

            if (!string.IsNullOrWhiteSpace(status))
                query = query.Where(o => o.Status.Contains(status));

            int totalCount = await query.CountAsync();

            var pagedOrders = await query
                .OrderByDescending(o => o.CreatedTime)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var result = _mapper.Map<List<OrderModelView>>(pagedOrders);

            for (int i = 0; i < result.Count; i++)
            {
                result[i].User = _mapper.Map<UserModelView>(pagedOrders[i].User);
            }

            return new ApiSuccessResult<BasePaginatedList<OrderModelView>>(
                new BasePaginatedList<OrderModelView>(result, totalCount, pageNumber, pageSize));
        }


        public async Task<ApiResult<int>> GetLatestOrderIdByUserAsync(int userId)
        {
            var latestOrder = await _unitOfWork.GetRepository<Order>().Entities
                .Where(o => o.UserID == userId)
                .OrderByDescending(o => o.CreatedTime)
                .FirstOrDefaultAsync();

            if (latestOrder == null)
                return new ApiErrorResult<int>("Người dùng chưa có đơn hàng nào.");

            return new ApiSuccessResult<int>(latestOrder.Id);
        }

    }


}
