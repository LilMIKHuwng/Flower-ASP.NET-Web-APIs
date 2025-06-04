using Flower.ModelViews.FlowerTypeModelViews;
using Flower.ModelViews.OrderModelViews;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flower.ModelViews.OrderDetailModelViews
{
    public class OrderDetailModelView
    {
        public int Id { get; set; }
        public OrderModelView Order { get; set; }
        public FlowerTypeModelView Flower { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
