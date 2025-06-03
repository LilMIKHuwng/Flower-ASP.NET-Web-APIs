using Flower.ModelViews.UserModelViews;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flower.ModelViews.OrderModelViews
{
    public class OrderModelView
    {
        public int Id { get; set; }
        public UserModelView User { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }
        public string Status { get; set; }
        public string PaymentMethod { get; set; }
        public string DeliveryAddress { get; set; }
    }
}
