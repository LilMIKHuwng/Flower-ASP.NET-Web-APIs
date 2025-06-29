﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flower.ModelViews.FlowerTypeModelViews
{
    public class FlowerTypeCreateModelView
    {
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; } = 0;
        public List<IFormFile> ImageURLs { get; set; }
        public int? CategoryID { get; set; }
    }

}
