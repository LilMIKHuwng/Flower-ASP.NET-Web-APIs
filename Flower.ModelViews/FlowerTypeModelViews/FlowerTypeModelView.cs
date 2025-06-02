using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flower.ModelViews.FlowerTypeModelViews
{
    public class FlowerTypeModelView
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; } = 0;
        public List<string> ImageURLs { get; set; }
        public CategoryModelViews.CategoryModelView Category { get; set; }
    }
}
