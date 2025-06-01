using Flower.Core.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flower.Contract.Repositories.Entity
{
    public class Payment : BaseEntity
    {
        public int OrderID { get; set; }
        public DateTime PaymentDate { get; set; }
        public decimal Amount { get; set; }
        public string PaymentStatus { get; set; } 
        public string PaymentMethod { get; set; }

        public virtual Order Order { get; set; }
    }

}
