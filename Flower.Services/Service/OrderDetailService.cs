using AutoMapper;
using Flower.Contract.Repositories.Entity;
using Flower.Contract.Repositories.Interface;
using Flower.Contract.Services.Interface;
using Flower.Core.APIResponse;
using Flower.Core;
using Flower.ModelViews.FlowerTypeModelViews;
using Flower.ModelViews.OrderDetailModelViews;
using Flower.ModelViews.OrderModelViews;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Flower.Services.Service
{
    public class OrderDetailService : IOrderDetailService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public OrderDetailService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ApiResult<OrderDetailModelView>> GetOrderDetailByIdAsync(int id)
        {
            var orderDetail = await _unitOfWork.GetRepository<OrderDetail>().Entities
                .Include(od => od.Order)
                .Include(od => od.Flower)
                .FirstOrDefaultAsync(od => od.Id == id);

            if (orderDetail == null)
                return new ApiErrorResult<OrderDetailModelView>("Không tìm thấy chi tiết đơn hàng.");

            var result = _mapper.Map<OrderDetailModelView>(orderDetail);
            result.Order = _mapper.Map<OrderModelView>(orderDetail.Order);
            result.Flower = _mapper.Map<FlowerTypeModelView>(orderDetail.Flower);

            return new ApiSuccessResult<OrderDetailModelView>(result);
        }

        public async Task<ApiResult<List<OrderDetailModelView>>> GetAllOrderDetailsAsync(int? userId, int? orderId)
        {
            var query = _unitOfWork.GetRepository<OrderDetail>().Entities
                .Include(od => od.Order)
                .Include(od => od.Flower)
                .AsQueryable();

            if (userId.HasValue)
            {
                query = query.Where(od => od.Order.UserID == userId.Value);
            }

            if (orderId.HasValue)
            {
                query = query.Where(od => od.OrderID == orderId.Value);
            }

            var orderDetails = await query.ToListAsync();
            var result = _mapper.Map<List<OrderDetailModelView>>(orderDetails);

            for (int i = 0; i < result.Count; i++)
            {
                result[i].Order = _mapper.Map<OrderModelView>(orderDetails[i].Order);
                result[i].Flower = _mapper.Map<FlowerTypeModelView>(orderDetails[i].Flower);
            }

            return new ApiSuccessResult<List<OrderDetailModelView>>(result);
        }

        public async Task<ApiResult<BasePaginatedList<OrderDetailModelView>>> GetAllOrdersPaginatedAsync(
            int pageNumber, int pageSize, int? userId, int? orderId)
        {
            var query = _unitOfWork.GetRepository<OrderDetail>().Entities
                .Include(od => od.Order)
                .Include(od => od.Flower)
                .AsQueryable();

            if (userId.HasValue)
            {
                query = query.Where(od => od.Order.UserID == userId.Value);
            }

            if (orderId.HasValue)
            {
                query = query.Where(od => od.OrderID == orderId.Value);
            }

            var totalCount = await query.CountAsync();

            var pagedList = await query
                .OrderByDescending(od => od.CreatedTime)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var result = _mapper.Map<List<OrderDetailModelView>>(pagedList);

            for (int i = 0; i < result.Count; i++)
            {
                result[i].Order = _mapper.Map<OrderModelView>(pagedList[i].Order);
                result[i].Flower = _mapper.Map<FlowerTypeModelView>(pagedList[i].Flower);
            }

            return new ApiSuccessResult<BasePaginatedList<OrderDetailModelView>>(
                new BasePaginatedList<OrderDetailModelView>(result, totalCount, pageNumber, pageSize));
        }
    }

}
