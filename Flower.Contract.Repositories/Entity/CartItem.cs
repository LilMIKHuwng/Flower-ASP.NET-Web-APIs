using Flower.Core.Base;
using Flower.Repositories.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flower.Contract.Repositories.Entity
{
    public class CartItem : BaseEntity
    {
        public int UserID { get; set; }
        public int FlowerID { get; set; }
        public int Quantity { get; set; }
        public DateTime AddedAt { get; set; } = DateTime.Now;

        public virtual ApplicationUser User { get; set; }
        public virtual FlowerType Flower { get; set; }
    }
}
