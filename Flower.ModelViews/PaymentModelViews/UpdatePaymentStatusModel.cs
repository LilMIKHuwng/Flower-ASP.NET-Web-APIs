using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flower.ModelViews.PaymentModelViews
{
    public class UpdatePaymentStatusModel
    {
        public int PaymentId { get; set; }
        public bool IsSuccess { get; set; }
    }

}
