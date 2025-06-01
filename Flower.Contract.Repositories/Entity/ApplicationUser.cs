using Microsoft.AspNetCore.Identity;
using Flower.Contract.Repositories.Entity;
using Flower.Core.Utils;

namespace Flower.Repositories.Entity
{
    public class ApplicationUser : IdentityUser<int>
    {
        public string? FullName { get; set; }
        public int? Age { get; set; }
        public string? AvatarUrl { get; set; }

        public int? Status { get; set; }

        public int? CreatedBy { get; set; }
        public int? LastUpdatedBy { get; set; }
        public int? DeletedBy { get; set; }
        public DateTimeOffset CreatedTime { get; set; } = DateTime.Now;
        public DateTimeOffset LastUpdatedTime { get; set; } = DateTime.Now;
        public DateTimeOffset? DeletedTime { get; set; }

        public string? RefreshToken { get; set; }
        public DateTimeOffset RefreshTokenExpiryTime { get; set; }



        public virtual ICollection<ApplicationUserRole> UserRoles { get; set; }

        public virtual ICollection<CartItem> CartItems { get; set; }

        public virtual ICollection<Order> Orders { get; set; }

        public virtual ICollection<UserMessage> SentMessages { get; set; }
        public virtual ICollection<UserMessage> ReceivedMessages { get; set; }
    }
}
