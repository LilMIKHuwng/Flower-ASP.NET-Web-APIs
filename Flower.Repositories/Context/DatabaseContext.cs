using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Flower.Contract.Repositories.Entity;
using Flower.Repositories.Entity;
using Microsoft.AspNetCore.Identity;

namespace Flower.Repositories.Context
{
    public class DatabaseContext : IdentityDbContext<ApplicationUser, ApplicationRole, int,
        IdentityUserClaim<int>, ApplicationUserRole, IdentityUserLogin<int>,
        IdentityRoleClaim<int>, IdentityUserToken<int>>
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) { }

        // DbSet
        public virtual DbSet<ApplicationUser> ApplicationUsers => Set<ApplicationUser>();
        public virtual DbSet<ApplicationRole> ApplicationRoles => Set<ApplicationRole>();
        public virtual DbSet<ApplicationUserRole> ApplicationUserRoles => Set<ApplicationUserRole>();


        public virtual DbSet<Category> Categories => Set<Category>();
        public virtual DbSet<FlowerType> Flowers => Set<FlowerType>();
        public virtual DbSet<CartItem> CartItems => Set<CartItem>();
        public virtual DbSet<Order> Orders => Set<Order>();
        public virtual DbSet<OrderDetail> OrderDetails => Set<OrderDetail>();
        public virtual DbSet<Payment> Payments => Set<Payment>();
        public virtual DbSet<Store> Stores => Set<Store>();
        public virtual DbSet<UserMessage> UserMessages => Set<UserMessage>();


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<ApplicationUserRole>(userRole =>
            {
                userRole.HasKey(ur => new { ur.UserId, ur.RoleId });

                userRole.HasOne(ur => ur.User)
                    .WithMany(u => u.UserRoles)
                    .HasForeignKey(ur => ur.UserId)
                    .OnDelete(DeleteBehavior.Restrict);

                userRole.HasOne(ur => ur.Role)
                    .WithMany(r => r.UserRoles)
                    .HasForeignKey(ur => ur.RoleId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            builder.Entity<UserMessage>(builder =>
            {
                builder.HasOne(x => x.Sender)
                    .WithMany(x => x.SentMessages)
                    .HasForeignKey(x => x.SenderId)
                    .OnDelete(DeleteBehavior.Restrict);

                builder.HasOne(x => x.Recipient)
                    .WithMany(x => x.ReceivedMessages)
                    .HasForeignKey(x => x.RecipientId)
                    .OnDelete(DeleteBehavior.Restrict);
            });


            // Seed dữ liệu cho Store mặc định
            builder.Entity<Store>().HasData(new Store
            {
                Id = 1,
                StoreName = "FPT University HCM Store",
                Address = "Lô E2a-7, Đường D1, Khu Công nghệ cao, P. Long Thạnh Mỹ, TP. Thủ Đức, TP.HCM",
                Latitude = 10.8411,       // Vĩ độ
                Longitude = 106.8095,     // Kinh độ
                CreatedTime = DateTimeOffset.Now,
                LastUpdatedTime = DateTimeOffset.Now
            });

            // Seed dữ liệu Role
            builder.Entity<ApplicationRole>().HasData(
                new ApplicationRole { Id = 1, Name = "Admin", NormalizedName = "ADMIN" },
                new ApplicationRole { Id = 2, Name = "User", NormalizedName = "USER" },
                new ApplicationRole { Id = 3, Name = "Staff", NormalizedName = "STAFF" }
            );

            // Seed dữ liệu User
            var hasher = new PasswordHasher<ApplicationUser>();

            var admin = new ApplicationUser
            {
                Id = 1,
                UserName = "admin",
                NormalizedUserName = "ADMIN",
                Email = "admin@fpt.edu.vn",
                NormalizedEmail = "ADMIN@FPT.EDU.VN",
                EmailConfirmed = true,
                SecurityStamp = Guid.NewGuid().ToString("D")
            };
            admin.PasswordHash = hasher.HashPassword(admin, "Admin@123");

            var user = new ApplicationUser
            {
                Id = 2,
                UserName = "user",
                NormalizedUserName = "USER",
                Email = "user@fpt.edu.vn",
                NormalizedEmail = "USER@FPT.EDU.VN",
                EmailConfirmed = true,
                SecurityStamp = Guid.NewGuid().ToString("D")
            };
            user.PasswordHash = hasher.HashPassword(user, "User@123");

            var staff = new ApplicationUser
            {
                Id = 3,
                UserName = "staff",
                NormalizedUserName = "STAFF",
                Email = "staff@fpt.edu.vn",
                NormalizedEmail = "STAFF@FPT.EDU.VN",
                EmailConfirmed = true,
                SecurityStamp = Guid.NewGuid().ToString("D")
            };
            staff.PasswordHash = hasher.HashPassword(staff, "Staff@123");

            builder.Entity<ApplicationUser>().HasData(admin, user, staff);

            // Gán Role cho từng User
            builder.Entity<ApplicationUserRole>().HasData(
                new ApplicationUserRole { UserId = 1, RoleId = 1 }, // Admin
                new ApplicationUserRole { UserId = 2, RoleId = 2 }, // User
                new ApplicationUserRole { UserId = 3, RoleId = 3 }  // Staff
            );

        }

    }
}
