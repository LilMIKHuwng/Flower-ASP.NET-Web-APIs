using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flower.ModelViews.UserModelViews
{
    public class UserUpdateProfileModelView
    {
        public int Id { get; set; }
        public string? FullName { get; set; }
        public int? Age { get; set; }
        public string? Email { get; set; }
        public IFormFile? AvatarUrl { get; set; }
    }
}
