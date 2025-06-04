using Flower.Core;
using Flower.Core.APIResponse;
using Flower.ModelViews.PaymentModelViews;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flower.Contract.Services.Interface
{
    public interface IPaymentService
    {
        Task<ApiResult<string>> CreateVNPayPaymentUrlAsync(CreatePaymentModelView model, string ipAddress);
        Task<ApiResult<string>> HandleVNPayReturnAsync(IQueryCollection vnpParams);
        Task<ApiResult<PaymentModelView>> GetPaymentByIdAsync(int id);
        Task<ApiResult<BasePaginatedList<PaymentModelView>>> GetAllPaymentsAsync(int pageNumber, int pageSize, int? userId);
    }
}
