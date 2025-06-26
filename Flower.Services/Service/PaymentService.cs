using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using VNPAY.NET;
using VNPAY.NET.Enums;
using VNPAY.NET.Models;
using Net.payOS.Types;
using Net.payOS;
using Flower.Contract.Repositories.Interface;
using Flower.Core.APIResponse;
using Flower.ModelViews.PaymentModelViews;
using Flower.Contract.Repositories.Entity;
using Flower.ModelViews.OrderModelViews;
using Flower.Core;
using Flower.Contract.Services.Interface;

namespace TeamUp.Services.Service
{
    public class PaymentService : IPaymentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IConfiguration _configuration;
        private readonly IVnpay _vnpay;

        private string _tmnCode;
        private string _hashSecret;
        private string _baseUrl;
        private string _callbackUrl;

        public PaymentService(IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor contextAccessor, IConfiguration configuration, IVnpay vnpay)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _contextAccessor = contextAccessor;
            _configuration = configuration;
            _vnpay = vnpay;

            _tmnCode = configuration["Vnpay:TmnCode"];
            _hashSecret = configuration["Vnpay:HashSecret"];
            _baseUrl = configuration["Vnpay:BaseUrl"];
            _callbackUrl = configuration["Vnpay:ReturnUrl"];

            _vnpay.Initialize(_tmnCode, _hashSecret, _baseUrl, _callbackUrl);
        }

        public async Task<ApiResult<CreatePaymentResponseModel>> CreateVNPayPaymentUrlAsync(CreatePaymentModelView model, string ipAddress)
        {
            // Kiểm tra dữ liệu
            if (model.OrderID == null)
                return new ApiErrorResult<CreatePaymentResponseModel>("Invalid payment information");

            decimal amount = 0;
            string description = null;

            if (model.OrderID != null)
            {
                var order = await _unitOfWork.GetRepository<Order>()
                    .Entities.FirstOrDefaultAsync(x => x.Id == model.OrderID && !x.DeletedTime.HasValue);

                if (order == null)
                    return new ApiErrorResult<CreatePaymentResponseModel>("Không tìm hóa đơn.");

                amount = order.TotalAmount;
                description = $"Thanh toán hóa đơn |{order.Id}|{order.UserID}";
            }
            var order_EX = await _unitOfWork.GetRepository<Order>()
       .Entities.FirstOrDefaultAsync(x => x.Id == model.OrderID && !x.DeletedTime.HasValue);

            if (order_EX == null)
                return new ApiErrorResult<CreatePaymentResponseModel>("Không tìm thấy hóa đơn.");

            var payment = new Payment
            {
                CreatedTime = DateTime.Now,
                CreatedBy = order_EX.UserID,
                PaymentMethod = "VNPay",
                OrderID = order_EX.Id,
                PaymentDate = DateTime.Now,
                PaymentStatus = "Pending",
                Amount = order_EX.TotalAmount
            };

            await _unitOfWork.GetRepository<Payment>().InsertAsync(payment);
            await _unitOfWork.SaveAsync();
            var request = new PaymentRequest
            {
                PaymentId = DateTime.Now.Ticks,
                Money = (double)amount,
                Description = description,
                IpAddress = ipAddress,
                BankCode = BankCode.ANY,
                CreatedDate = DateTime.Now,
                Currency = Currency.VND,
                Language = DisplayLanguage.Vietnamese
            };

            string paymentUrl = _vnpay.GetPaymentUrl(request);

            return new ApiSuccessResult<CreatePaymentResponseModel>(new CreatePaymentResponseModel
            {
                PaymentUrl = paymentUrl,
                PaymentId = payment.Id
            });

        }
        public async Task<ApiResult<string>> UpdatePaymentStatusAsync(UpdatePaymentStatusModel model)
        {
            _unitOfWork.BeginTransaction();

            try
            {
                var paymentRepo = _unitOfWork.GetRepository<Payment>();
                var orderRepo = _unitOfWork.GetRepository<Order>();

                var payment = await paymentRepo.GetByIdAsync(model.PaymentId);
                if (payment == null)
                    return new ApiErrorResult<string>("Không tìm thấy thanh toán.");

                payment.PaymentStatus = model.IsSuccess ? "Success" : "Failed";
                payment.PaymentDate = DateTime.Now;

                await paymentRepo.UpdateAsync(payment);

                var order = await orderRepo.GetByIdAsync(payment.OrderID);
                if (order != null)
                {
                    order.Status = model.IsSuccess ? "Paid" : "Failed";
                    await orderRepo.UpdateAsync(order);
                }

                await _unitOfWork.SaveAsync();
                _unitOfWork.CommitTransaction();

                return new ApiSuccessResult<string>("Cập nhật trạng thái thanh toán thành công.");
            }
            catch (Exception ex)
            {
                _unitOfWork.RollBack();
                return new ApiErrorResult<string>($"Lỗi khi cập nhật: {ex.Message}");
            }
        }


        public async Task<ApiResult<string>> HandleVNPayReturnAsync(IQueryCollection vnpParams)
        {
            var result = _vnpay.GetPaymentResult(vnpParams);
            if (result == null || string.IsNullOrEmpty(result.Description))
                return new ApiErrorResult<string>("Thanh toán thất bại.");

            var parts = result.Description.Split('|');
            if (parts.Length != 3)
                return new ApiErrorResult<string>("Thanh toán thất bại.");

            if (!int.TryParse(parts[1], out int orderId) || !int.TryParse(parts[2], out int userId))
                return new ApiErrorResult<string>("Thông tin đơn hàng không hợp lệ.");

            _unitOfWork.BeginTransaction();

            try
            {
                Payment payment = new Payment
                {
                    CreatedTime = DateTime.Now,
                    CreatedBy = userId,
                    PaymentMethod = "VNPay",
                    OrderID = orderId,
                    PaymentDate = DateTime.Now,
                    PaymentStatus = result.IsSuccess ? "Success" : "Failed"
                };

                var order = await _unitOfWork.GetRepository<Order>().GetByIdAsync(orderId);
                if (order == null)
                    return new ApiErrorResult<string>("Không tìm thấy đơn hàng.");

                order.Status = result.IsSuccess ? "Paid" : "Failed";

                await _unitOfWork.GetRepository<Order>().UpdateAsync(order);

                payment.Amount = order.TotalAmount;

                await _unitOfWork.GetRepository<Payment>().InsertAsync(payment);
                await _unitOfWork.SaveAsync();
                _unitOfWork.CommitTransaction();


                return new ApiSuccessResult<string>("Thanh toán thành công.");
            }
            catch (Exception ex)
            {
                _unitOfWork.RollBack();
                return new ApiErrorResult<string>($"Thanh toán thất bại. Lỗi: {ex.Message}");
            }
        }




        public async Task<ApiResult<PaymentModelView>> GetPaymentByIdAsync(int id)
        {
            var payment = await _unitOfWork.GetRepository<Payment>().Entities
                .Include(p => p.Order)
                .FirstOrDefaultAsync(p => p.Id == id && !p.DeletedTime.HasValue);

            if (payment == null)
                return new ApiErrorResult<PaymentModelView>("Không tìm thấy thanh toán.");

            var result = _mapper.Map<PaymentModelView>(payment);

            result.Order = _mapper.Map<OrderModelView>(payment.Order);

            return new ApiSuccessResult<PaymentModelView>(result);
        }

        public async Task<ApiResult<BasePaginatedList<PaymentModelView>>> GetAllPaymentsAsync(int pageNumber, int pageSize, int? userId)
        {
            var query = _unitOfWork.GetRepository<Payment>().Entities
                .Include(p => p.Order)
                .Where(p => !p.DeletedTime.HasValue);

            if (userId.HasValue)
            {
                query = query.Where(p => p.Order.UserID == userId.Value);
            }

            int totalCount = await query.CountAsync();

            var paginatedPayments = await query
                .OrderByDescending(p => p.PaymentDate)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var result = _mapper.Map<List<PaymentModelView>>(paginatedPayments);

            for (int i = 0; i < result.Count; i++)
            {
                var payment = paginatedPayments[i];
                result[i].Order = _mapper.Map<OrderModelView>(payment.Order);
            }

            return new ApiSuccessResult<BasePaginatedList<PaymentModelView>>(
                new BasePaginatedList<PaymentModelView>(result, totalCount, pageNumber, pageSize));
        }

    }
}
