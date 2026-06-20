using Microsoft.EntityFrameworkCore;
using ToCarAPI.Models;

namespace ToCarAPI.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Banner> Banners { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Item>()
                .HasOne(i => i.Category)
                .WithMany(c => c.Items)
                .HasForeignKey(i => i.CategoryId);

            modelBuilder.Entity<OrderItem>()
                .HasOne(oi => oi.Order)
                .WithMany(o => o.OrderItems)
                .HasForeignKey(oi => oi.OrderId);

            modelBuilder.Entity<OrderItem>()
                .HasOne(oi => oi.Item)
                .WithMany()
                .HasForeignKey(oi => oi.ItemId);

            modelBuilder.Entity<CartItem>()
                .HasOne(ci => ci.Item)
                .WithMany()
                .HasForeignKey(ci => ci.ItemId);

            // Seed Categories
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "تشكيلة السيارات", ImageUrl = "https://via.placeholder.com/150" },
                new Category { Id = 2, Name = "قطع الغيار", ImageUrl = "https://via.placeholder.com/150" },
                new Category { Id = 3, Name = "الإكسسوارات", ImageUrl = "https://via.placeholder.com/150" }
            );

            // Seed Items
            modelBuilder.Entity<Item>().HasData(
    new Item { Id = 1, Title = "تويوتا كامري", Price = 25000, Description = "سيارة مريحة واقتصادية", ImageUrl = "https://via.placeholder.com/150", CategoryId = 1 },
    new Item { Id = 2, Title = "هوندا سيفيك", Price = 20000, Description = "سيارة اقتصادية وموثوقة", ImageUrl = "https://via.placeholder.com/150", CategoryId = 1 },
    new Item { Id = 3, Title = "فلتر زيت", Price = 15, Description = "فلتر زيت عالي الجودة", ImageUrl = "https://via.placeholder.com/150", CategoryId = 2 },
    new Item { Id = 4, Title = "طفاية هواء", Price = 50, Description = "طفاية هواء أصلية", ImageUrl = "https://via.placeholder.com/150", CategoryId = 2 },
    new Item { Id = 5, Title = "غطاء مقود", Price = 30, Description = "غطاء مقود جلدي فاخر", ImageUrl = "https://via.placeholder.com/150", CategoryId = 3 }
);

            // Seed Banners
            modelBuilder.Entity<Banner>().HasData(
                new Banner { Id = 1, ImageUrl = "https://via.placeholder.com/800x200", Title = "عروض الصيف" },
                new Banner { Id = 2, ImageUrl = "https://via.placeholder.com/800x200", Title = "تخفيضات قطع الغيار" }
            );
        }
    }
}