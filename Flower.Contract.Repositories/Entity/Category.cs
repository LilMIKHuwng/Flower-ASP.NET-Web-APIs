using Flower.Core.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flower.Contract.Repositories.Entity
{
    public class Category : BaseEntity
    {
        public string CategoryName { get; set; } 

        public virtual ICollection<FlowerType> Flowers { get; set; } = new List<FlowerType>();
    }
}
