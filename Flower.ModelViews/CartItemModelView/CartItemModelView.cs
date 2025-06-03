using Flower.ModelViews.FlowerTypeModelViews;
using Flower.ModelViews.UserModelViews;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flower.ModelViews.CartItemModelView
{
    public class CartItemModelView
    {
        public int Id { get; set; }
        public UserModelView User { get; set; }
        public FlowerTypeModelView Flower { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public DateTime AddedAt { get; set; } 
    }
}
