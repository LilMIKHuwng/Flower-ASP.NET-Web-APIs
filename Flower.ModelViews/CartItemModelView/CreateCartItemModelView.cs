using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flower.ModelViews.CartItemModelView
{
    public class CreateCartItemModelView
    {
        [Required(ErrorMessage = "UserID là bắt buộc.")]
        [Range(1, int.MaxValue, ErrorMessage = "UserID phải lớn hơn 0.")]
        public int UserID { get; set; }

        [Required(ErrorMessage = "FlowerID là bắt buộc.")]
        [Range(1, int.MaxValue, ErrorMessage = "FlowerID phải lớn hơn 0.")]
        public int FlowerID { get; set; }

        [Required(ErrorMessage = "Quantity là bắt buộc.")]
        [Range(1, 1000, ErrorMessage = "Quantity phải từ 1 đến 1000.")]
        public int Quantity { get; set; }
    }
}
