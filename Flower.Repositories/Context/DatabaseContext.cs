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

            var firebaseImageUrls = new List<string>
            {
                "https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Ftest1.png?alt=media&token=0c05a2e7-869d-4e0c-98b2-41dd842fe90c",
                "https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Ftest2.png?alt=media&token=cc65bd49-e3df-4a51-b513-c7bb534b63d4",
                "https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Ftest3.png?alt=media&token=e239b164-1d55-437b-889d-19781c61a8b0",
                "https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Ftest4.png?alt=media&token=1a0da7ef-2eb3-48e9-a9de-1e2866fe8752",
                "https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Ftest5.png?alt=media&token=b2b2f296-f847-4c95-96d3-50ae7fc827a0"
            };

            builder.Entity<Category>().HasData(
                new Category { Id = 1, CategoryName = "Roses", CreatedTime = DateTimeOffset.Now, LastUpdatedTime = DateTimeOffset.Now },
                new Category { Id = 2, CategoryName = "Lilies", CreatedTime = DateTimeOffset.Now, LastUpdatedTime = DateTimeOffset.Now },
                new Category { Id = 3, CategoryName = "Tulips", CreatedTime = DateTimeOffset.Now, LastUpdatedTime = DateTimeOffset.Now },
                new Category { Id = 4, CategoryName = "Sunflowers", CreatedTime = DateTimeOffset.Now, LastUpdatedTime = DateTimeOffset.Now },
                new Category { Id = 5, CategoryName = "Orchids", CreatedTime = DateTimeOffset.Now, LastUpdatedTime = DateTimeOffset.Now }
            );


            builder.Entity<FlowerType>().HasData(
                new FlowerType
                {
                    Id = 1,
                    Name = "Red Rose",
                    Description = "Classic red rose symbolizing love",
                    Price = 100000,
                    Stock = 100,
                    CategoryID = 1,
                    CreatedTime = DateTimeOffset.Now,
                    LastUpdatedTime = DateTimeOffset.Now,
                    ImageURLs = firebaseImageUrls,
                },
                new FlowerType
                {
                    Id = 2,
                    Name = "White Lily",
                    Description = "Elegant white lily for sympathy and peace",
                    Price = 150000,
                    Stock = 80,
                    CategoryID = 2,
                    CreatedTime = DateTimeOffset.Now,
                    LastUpdatedTime = DateTimeOffset.Now,
                    ImageURLs = firebaseImageUrls,
                },
                new FlowerType
                {
                    Id = 3,
                    Name = "Pink Tulip",
                    Description = "Bright and colorful tulips for any occasion",
                    Price = 100000,
                    Stock = 150,
                    CategoryID = 3,
                    CreatedTime = DateTimeOffset.Now,
                    LastUpdatedTime = DateTimeOffset.Now,
                    ImageURLs = firebaseImageUrls,
                },
                new FlowerType
                {
                    Id = 4,
                    Name = "Sunflower",
                    Description = "Cheerful sunflower for happy vibes",
                    Price = 100000,
                    Stock = 120,
                    CategoryID = 4,
                    CreatedTime = DateTimeOffset.Now,
                    LastUpdatedTime = DateTimeOffset.Now,
                    ImageURLs = firebaseImageUrls,
                },
                new FlowerType
                {
                    Id = 5,
                    Name = "Purple Orchid",
                    Description = "Delicate and exotic orchid",
                    Price = 100000,
                    Stock = 70,
                    CategoryID = 5,
                    CreatedTime = DateTimeOffset.Now,
                    LastUpdatedTime = DateTimeOffset.Now,
                    ImageURLs = firebaseImageUrls,
                }
            );



        }

    }
}
