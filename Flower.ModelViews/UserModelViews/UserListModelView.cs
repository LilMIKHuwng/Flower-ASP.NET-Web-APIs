using Flower.ModelViews.RoleModelViews;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flower.ModelViews.UserModelViews
{
    public class UserListModelView
    {
        public int Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public string? FullName { get; set; }
        public int? Age { get; set; }
        public string? AvatarUrl { get; set; }
        public string Email { get; set; } = string.Empty;
        public int? Status { get; set; }
        public DateTimeOffset CreatedTime { get; set; }
        public DateTimeOffset LastUpdatedTime { get; set; }
        public RoleModelView Role { get; set; }
    }
}
