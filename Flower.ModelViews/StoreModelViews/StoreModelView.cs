using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flower.ModelViews.StoreModelViews
{
    public class StoreModelView
    {
        public int Id { get; set; }
        public string StoreName { get; set; }
        public string Address { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
}
