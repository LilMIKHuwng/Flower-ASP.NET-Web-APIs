using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flower.ModelViews.OrderModelViews
{
    public class CreateOrderModelView
    {
        [Required(ErrorMessage = "UserID là bắt buộc.")]
        [Range(1, int.MaxValue, ErrorMessage = "UserID phải lớn hơn 0.")]
        public int UserID { get; set; }

        [Required(ErrorMessage = "Địa chỉ giao hàng là bắt buộc.")]
        [StringLength(200, MinimumLength = 5, ErrorMessage = "Địa chỉ phải từ 5 đến 200 ký tự.")]
        public string DeliveryAddress { get; set; }
    }
}
