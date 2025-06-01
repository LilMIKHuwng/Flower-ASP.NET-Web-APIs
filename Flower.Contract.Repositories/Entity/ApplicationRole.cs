using Microsoft.AspNetCore.Identity;
using Flower.Core.Utils;

namespace Flower.Contract.Repositories.Entity
{
    public class ApplicationRole : IdentityRole<int>
    {
        public string? Description { get; set; }

        public int? CreatedBy { get; set; }
        public int? LastUpdatedBy { get; set; }
        public int? DeletedBy { get; set; }
        public DateTimeOffset CreatedTime { get; set; } = DateTime.Now;
        public DateTimeOffset LastUpdatedTime { get; set; } = DateTime.Now;
        public DateTimeOffset? DeletedTime { get; set; }

        public virtual ICollection<ApplicationUserRole> UserRoles { get; set; }
    }
}