using Flower.Core.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flower.Contract.Repositories.Entity
{
    public class OrderDetail : BaseEntity
    {
        public int OrderID { get; set; }
        public int FlowerID { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }

        public virtual Order Order { get; set; }
        public virtual FlowerType Flower { get; set; }
    }

}
