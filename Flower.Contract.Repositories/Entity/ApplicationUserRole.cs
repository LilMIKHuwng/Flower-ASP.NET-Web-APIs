using Microsoft.AspNetCore.Identity;
using Flower.Core.Utils;
using Flower.Repositories.Entity;

namespace Flower.Contract.Repositories.Entity
{
    public class ApplicationUserRole : IdentityUserRole<int>
    {
        public virtual ApplicationUser User { get; set; }
        public virtual ApplicationRole Role { get; set; }
    }
}
