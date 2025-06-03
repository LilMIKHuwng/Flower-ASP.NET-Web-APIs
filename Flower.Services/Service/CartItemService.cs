using AutoMapper;
using Flower.Contract.Repositories.Entity;
using Flower.Contract.Repositories.Interface;
using Flower.Contract.Services.Interface;
using Flower.Core.APIResponse;
using Flower.Core;
using Flower.ModelViews.CartItemModelView;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Flower.ModelViews.UserModelViews;
using Flower.ModelViews.FlowerTypeModelViews;
using Flower.ModelViews.CategoryModelViews;

namespace Flower.Services.Service
{
    public class CartItemService : ICartItemService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _contextAccessor;

        public CartItemService(IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor contextAccessor)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _contextAccessor = contextAccessor;
        }

        public async Task<ApiResult<BasePaginatedList<CartItemModelView>>> GetAllCartItemsAsync(int pageNumber, int pageSize, int? userId, int? flowerId)
        {
            var query = _unitOfWork.GetRepository<CartItem>().Entities;

            if (userId.HasValue)
                query = query.Where(c => c.UserID == userId.Value);

            if (flowerId.HasValue)
                query = query.Where(c => c.FlowerID == flowerId.Value);

            int totalCount = await query.CountAsync();

            var pagedItems = await query
                .OrderByDescending(c => c.AddedAt)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var result = _mapper.Map<List<CartItemModelView>>(pagedItems);

            for (int i = 0; i < result.Count; i++)
            {
                result[i].User = _mapper.Map<UserModelView>(pagedItems[i].User);

                result[i].Flower = _mapper.Map<FlowerTypeModelView>(pagedItems[i].Flower);

                result[i].Flower.Category = _mapper.Map<CategoryModelView>(pagedItems[i].Flower.Category);
            }

            return new ApiSuccessResult<BasePaginatedList<CartItemModelView>>(
                new BasePaginatedList<CartItemModelView>(result, totalCount, pageNumber, pageSize));
        }

        public async Task<ApiResult<object>> AddCartItemAsync(CreateCartItemModelView model)
        {
            var repo = _unitOfWork.GetRepository<FlowerType>();
            var flower = await repo.Entities.FirstOrDefaultAsync(c => c.Id == model.FlowerID);

            if (flower == null)
                return new ApiErrorResult<object>("Không tìm thấy loại hoa tương ứng với giỏ hàng.");

            if (flower.Stock < model.Quantity)
            {
                return new ApiErrorResult<object>($"Số lượng tồn kho không đủ. Hiện còn lại {flower.Stock} sản phẩm.");
            }

            var newItem = _mapper.Map<CartItem>(model);
            newItem.AddedAt = DateTime.Now;
            newItem.UnitPrice = flower.Price;

            newItem.CreatedBy = model.UserID;
            newItem.CreatedTime = DateTime.Now;

            await _unitOfWork.GetRepository<CartItem>().InsertAsync(newItem);
            await _unitOfWork.SaveAsync();

            return new ApiSuccessResult<object>("Thêm mục vào giỏ hàng thành công.");
        }

        public async Task<ApiResult<object>> UpdateCartItemAsync(int id, UpdateCartItemModelView model)
        {
            var repo = _unitOfWork.GetRepository<CartItem>();
            var cartItem = await repo.Entities.FirstOrDefaultAsync(c => c.Id == id);

            if (cartItem == null)
                return new ApiErrorResult<object>("Không tìm thấy mục giỏ hàng.");

            if (model.UserID.HasValue)
                cartItem.UserID = model.UserID.Value;

            FlowerType? flower = null;
            if (model.FlowerID.HasValue)
            {
                cartItem.FlowerID = model.FlowerID.Value;

                var repoflower = _unitOfWork.GetRepository<FlowerType>();
                flower = await repoflower.Entities.FirstOrDefaultAsync(f => f.Id == model.FlowerID);

                if (flower == null)
                    return new ApiErrorResult<object>("Không tìm thấy loại hoa.");

                cartItem.UnitPrice = flower.Price;
            }
            else
            {
                // Nếu không đổi hoa, lấy thông tin hoa hiện tại từ cartItem
                var repoflower = _unitOfWork.GetRepository<FlowerType>();
                flower = await repoflower.Entities.FirstOrDefaultAsync(f => f.Id == cartItem.FlowerID);

                if (flower == null)
                    return new ApiErrorResult<object>("Không tìm thấy loại hoa tương ứng với giỏ hàng.");
            }

            if (model.Quantity.HasValue)
            {
                if (model.Quantity.Value > flower.Stock)
                {
                    return new ApiErrorResult<object>($"Số lượng tồn kho không đủ. Hiện còn lại {flower.Stock} sản phẩm.");
                }

                cartItem.Quantity = model.Quantity.Value;
            }

            cartItem.LastUpdatedBy = model.UserID;
            cartItem.LastUpdatedTime = DateTime.Now;

            await repo.UpdateAsync(cartItem);
            await _unitOfWork.SaveAsync();

            return new ApiSuccessResult<object>("Cập nhật mục giỏ hàng thành công.");
        }



        public async Task<ApiResult<object>> DeleteCartItemAsync(int id)
        {
            var repo = _unitOfWork.GetRepository<CartItem>();
            var cartItem = await repo.Entities.FirstOrDefaultAsync(c => c.Id == id);

            if (cartItem == null)
                return new ApiErrorResult<object>("Không tìm thấy mục giỏ hàng.");

            await repo.DeleteAsync(cartItem.Id);
            await _unitOfWork.SaveAsync();

            return new ApiSuccessResult<object>("Xóa mục giỏ hàng thành công.");
        }

        public async Task<ApiResult<CartItemModelView>> GetCartItemByIdAsync(int id)
        {
            var cartItem = await _unitOfWork.GetRepository<CartItem>().Entities.FirstOrDefaultAsync(c => c.Id == id);

            if (cartItem == null)
                return new ApiErrorResult<CartItemModelView>("Không tìm thấy mục giỏ hàng.");

            var result = _mapper.Map<CartItemModelView>(cartItem);

            result.User = _mapper.Map<UserModelView>(cartItem.User);

            result.Flower = _mapper.Map<FlowerTypeModelView>(cartItem.Flower);

            result.Flower.Category = _mapper.Map<CategoryModelView>(cartItem.Flower.Category);

            return new ApiSuccessResult<CartItemModelView>(result);
        }

        public async Task<ApiResult<List<CartItemModelView>>> GetAllCartItems()
        {
            var items = await _unitOfWork.GetRepository<CartItem>().Entities
                .OrderByDescending(c => c.AddedAt)
                .ToListAsync();

            var result = _mapper.Map<List<CartItemModelView>>(items);

            for (int i = 0; i < result.Count; i++)
            {
                result[i].User = _mapper.Map<UserModelView>(items[i].User);

                result[i].Flower = _mapper.Map<FlowerTypeModelView>(items[i].Flower);

                result[i].Flower.Category = _mapper.Map<CategoryModelView>(items[i].Flower.Category);
            }

            return new ApiSuccessResult<List<CartItemModelView>>(result);
        }

        public async Task<ApiResult<List<CartItemModelView>>> GetCartItemsByUserIdAsync(int userId)
        {
            var cartItems = await _unitOfWork.GetRepository<CartItem>().Entities
                .Where(c => c.UserID == userId)
                .Include(c => c.User)
                .Include(c => c.Flower)
                .OrderByDescending(c => c.AddedAt)
                .ToListAsync();

            var result = _mapper.Map<List<CartItemModelView>>(cartItems);

            for (int i = 0; i < result.Count; i++)
            {
                result[i].User = _mapper.Map<UserModelView>(cartItems[i].User);

                result[i].Flower = _mapper.Map<FlowerTypeModelView>(cartItems[i].Flower);

                result[i].Flower.Category = _mapper.Map<CategoryModelView>(cartItems[i].Flower.Category);
            }

            return new ApiSuccessResult<List<CartItemModelView>>(result);
        }

        public async Task<ApiResult<object>> DeleteCartItemsByUserIdAsync(int userId)
        {
            var repo = _unitOfWork.GetRepository<CartItem>();
            var cartItems = await repo.Entities
                .Where(c => c.UserID == userId)
                .ToListAsync();

            if (cartItems == null || !cartItems.Any())
                return new ApiErrorResult<object>("Không có mục giỏ hàng nào để xóa.");

            foreach (var item in cartItems)
            {
                await repo.DeleteAsync(item.Id); // Gửi đúng kiểu int
            }

            await _unitOfWork.SaveAsync();

            return new ApiSuccessResult<object>("Đã xóa toàn bộ mục giỏ hàng của người dùng.");
        }

    }

}
