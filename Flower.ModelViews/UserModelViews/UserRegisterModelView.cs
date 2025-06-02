using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flower.ModelViews.UserModelViews
{
    public class UserRegisterModelView
    {
        public string Username { get; set; } = string.Empty;
        public string? FullName { get; set; }
        public string Email { get; set; } = string.Empty;
        public int? Age { get; set; }
        public IFormFile? AvatarUrl { get; set; }
        public string Password { get; set; } = string.Empty;

    }
}
