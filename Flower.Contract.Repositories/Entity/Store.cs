using Flower.Core.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flower.Contract.Repositories.Entity
{
    public class Store : BaseEntity
    {
        public string StoreName { get; set; } = string.Empty;
        public string Address { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
}
