﻿using Flower.Contract.Services.Interface;
using Flower.Core.APIResponse;
using Flower.ModelViews.PaymentModelViews;
using Microsoft.AspNetCore.Mvc;

namespace TeamUp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentService _paymentService;

        public PaymentController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        public static string GetIpAddress(HttpContext context)
        {
            var ip = context.Request.Headers["X-Forwarded-For"].FirstOrDefault();
            if (string.IsNullOrEmpty(ip))
            {
                ip = context.Connection.RemoteIpAddress?.ToString();
            }
            return ip ?? "0.0.0.0";
        }

        // ==== VNPay ====

        [HttpPost("create-vnpay-url")]
        public async Task<ActionResult<ApiResult<string>>> CreateVNPayUrl([FromBody] CreatePaymentModelView model)
        {
            try
            {
                var ipAddress = GetIpAddress(HttpContext);
                var result = await _paymentService.CreateVNPayPaymentUrlAsync(model, ipAddress);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiErrorResult<string>(ex.Message));
            }
        }

        [HttpGet("vnpay-return")]
        public async Task<ActionResult<ApiResult<object>>> VNPayReturn()
        {
            try
            {
                var result = await _paymentService.HandleVNPayReturnAsync(Request.Query);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiErrorResult<string>(ex.Message));
            }
        }

        // ==== Common ====

        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResult<PaymentModelView>>> GetById(int id)
        {
            try
            {
                var result = await _paymentService.GetPaymentByIdAsync(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiErrorResult<PaymentModelView>(ex.Message));
            }
        }

        [HttpGet("all")]
        public async Task<ActionResult<ApiResult<List<PaymentModelView>>>> GetAll(
            [FromQuery] int? userId,
            int pageNumber = 1,
            int pageSize = 5)
        {
            try
            {
                var result = await _paymentService.GetAllPaymentsAsync(pageNumber, pageSize, userId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiErrorResult<List<PaymentModelView>>(ex.Message));
            }
        }
        [HttpPost("update-status")]
        public async Task<ActionResult<ApiResult<string>>> UpdatePaymentStatus([FromBody] UpdatePaymentStatusModel model)
        {
            try
            {
                var result = await _paymentService.UpdatePaymentStatusAsync(model);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiErrorResult<string>(ex.Message));
            }
        }
    }
}
