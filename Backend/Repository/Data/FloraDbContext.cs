using Microsoft.EntityFrameworkCore;
using Repository.Models;

namespace Repository.Data
{
    public class FloraDbContext : DbContext
    {
        public FloraDbContext(DbContextOptions<FloraDbContext> options) : base(options)
        {
        }

        // DbSets
        public DbSet<User> Users { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure relationships and constraints

            // User - Order relationship (One-to-Many)
            modelBuilder.Entity<Order>()
                .HasOne(o => o.User)
                .WithMany(u => u.Orders)
                .HasForeignKey(o => o.UserId)
                .OnDelete(DeleteBehavior.SetNull);

            // User - Cart relationship (One-to-Many)
            modelBuilder.Entity<Cart>()
                .HasOne(c => c.User)
                .WithMany(u => u.Carts)
                .HasForeignKey(c => c.UserId)
                .OnDelete(DeleteBehavior.SetNull);

            // User - RefreshToken relationship (One-to-Many)
            modelBuilder.Entity<RefreshToken>()
                .HasOne(rt => rt.User)
                .WithMany(u => u.RefreshTokens)
                .HasForeignKey(rt => rt.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // Category - Product relationship (One-to-Many)
            modelBuilder.Entity<Product>()
                .HasOne(p => p.Category)
                .WithMany(c => c.Products)
                .HasForeignKey(p => p.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            // Order - OrderDetail relationship (One-to-Many)
            modelBuilder.Entity<OrderDetail>()
                .HasOne(od => od.Order)
                .WithMany(o => o.OrderDetails)
                .HasForeignKey(od => od.OrderId)
                .OnDelete(DeleteBehavior.Cascade);

            // Product - OrderDetail relationship (One-to-Many)
            modelBuilder.Entity<OrderDetail>()
                .HasOne(od => od.Product)
                .WithMany(p => p.OrderDetails)
                .HasForeignKey(od => od.ProductId)
                .OnDelete(DeleteBehavior.Restrict);

            // Cart - CartItem relationship (One-to-Many)
            modelBuilder.Entity<CartItem>()
                .HasOne(ci => ci.Cart)
                .WithMany(c => c.CartItems)
                .HasForeignKey(ci => ci.CartId)
                .OnDelete(DeleteBehavior.Cascade);

            // Product - CartItem relationship (One-to-Many)
            modelBuilder.Entity<CartItem>()
                .HasOne(ci => ci.Product)
                .WithMany(p => p.CartItems)
                .HasForeignKey(ci => ci.ProductId)
                .OnDelete(DeleteBehavior.Restrict);

            // Indexes for better performance
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();

            modelBuilder.Entity<Order>()
                .HasIndex(o => o.OrderNumber)
                .IsUnique();

            modelBuilder.Entity<Product>()
                .HasIndex(p => p.ProductName);

            modelBuilder.Entity<Category>()
                .HasIndex(c => c.CategoryName);

            modelBuilder.Entity<RefreshToken>()
                .HasIndex(rt => rt.Token)
                .IsUnique();

            modelBuilder.Entity<RefreshToken>()
                .HasIndex(rt => rt.UserId);

            // Seed initial data
            SeedData(modelBuilder);
        }

        private void SeedData(ModelBuilder modelBuilder)
        {
            // Define specific GUIDs for consistent seeding
            var categoryId1 = Guid.Parse("11111111-1111-1111-1111-111111111111");
            var categoryId2 = Guid.Parse("22222222-2222-2222-2222-222222222222");
            var categoryId3 = Guid.Parse("33333333-3333-3333-3333-333333333333");
            var categoryId4 = Guid.Parse("44444444-4444-4444-4444-444444444444");
            var categoryId5 = Guid.Parse("55555555-5555-5555-5555-555555555555");
            var categoryId6 = Guid.Parse("66666666-6666-6666-6666-666666666666");

            var adminUserId = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa");
            var staffUserId = Guid.Parse("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb");

            // Seed Categories
            modelBuilder.Entity<Category>().HasData(
                new Category
                {
                    Id = categoryId1,
                    CategoryName = "Hoa Tươi",
                    Description = "Các loại hoa tươi đẹp cho mọi dịp",
                    IsActive = true,
                    CreatedAt = DateTime.UtcNow
                },
                new Category
                {
                    Id = categoryId2,
                    CategoryName = "Hoa Cưới",
                    Description = "Hoa cưới và trang trí tiệc cưới",
                    IsActive = true,
                    CreatedAt = DateTime.UtcNow
                },
                new Category
                {
                    Id = categoryId3,
                    CategoryName = "Hoa Sinh Nhật",
                    Description = "Hoa tặng sinh nhật ý nghĩa",
                    IsActive = true,
                    CreatedAt = DateTime.UtcNow
                },
                new Category
                {
                    Id = categoryId4,
                    CategoryName = "Hoa Khai Trương",
                    Description = "Hoa chúc mừng khai trương",
                    IsActive = true,
                    CreatedAt = DateTime.UtcNow
                },
                new Category
                {
                    Id = categoryId5,
                    CategoryName = "Hoa Chia Buồn",
                    Description = "Hoa chia buồn trang trọng",
                    IsActive = true,
                    CreatedAt = DateTime.UtcNow
                },
                new Category
                {
                    Id = categoryId6,
                    CategoryName = "Hoa Valentine",
                    Description = "Hoa tặng người yêu ngày Valentine",
                    IsActive = true,
                    CreatedAt = DateTime.UtcNow
                }
            );

            // Seed Users with different roles
            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = adminUserId,
                    FullName = "Administrator",
                    Email = "admin@flora.com",
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword("Admin123!"),
                    Role = "Admin",
                    IsActive = true,
                    EmailConfirmed = true,
                    CreatedAt = DateTime.UtcNow
                },
                new User
                {
                    Id = staffUserId,
                    FullName = "Staff User",
                    Email = "staff@flora.com",
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword("Staff123!"),
                    Role = "Staff",
                    IsActive = true,
                    EmailConfirmed = true,
                    CreatedAt = DateTime.UtcNow
                }
            );

            // Seed Sample Products
            modelBuilder.Entity<Product>().HasData(
                // Hoa Tươi
                new Product
                {
                    Id = Guid.Parse("10000000-0000-0000-0000-000000000001"),
                    ProductName = "Bó Hoa Hồng Đỏ",
                    Description = "Bó hoa hồng đỏ tươi đẹp, thể hiện tình yêu chân thành",
                    Price = 250000,
                    StockQuantity = 50,
                    CategoryId = categoryId1,
                    IsActive = true,
                    IsFeatured = true,
                    CreatedAt = DateTime.UtcNow
                },
                new Product
                {
                    Id = Guid.Parse("10000000-0000-0000-0000-000000000002"),
                    ProductName = "Bó Hoa Hồng Trắng",
                    Description = "Bó hoa hồng trắng tinh khôi, thanh tao",
                    Price = 230000,
                    StockQuantity = 45,
                    CategoryId = categoryId1,
                    IsActive = true,
                    IsFeatured = false,
                    CreatedAt = DateTime.UtcNow
                },
                new Product
                {
                    Id = Guid.Parse("10000000-0000-0000-0000-000000000003"),
                    ProductName = "Bó Hoa Tulip",
                    Description = "Bó hoa tulip đầy màu sắc, tươi mới",
                    Price = 180000,
                    DiscountPrice = 150000,
                    StockQuantity = 30,
                    CategoryId = categoryId1,
                    IsActive = true,
                    IsFeatured = true,
                    CreatedAt = DateTime.UtcNow
                },
                new Product
                {
                    Id = Guid.Parse("10000000-0000-0000-0000-000000000004"),
                    ProductName = "Bó Hoa Ly",
                    Description = "Bó hoa ly thơm ngát, sang trọng",
                    Price = 200000,
                    StockQuantity = 25,
                    CategoryId = categoryId1,
                    IsActive = true,
                    IsFeatured = false,
                    CreatedAt = DateTime.UtcNow
                },

                // Hoa Cưới
                new Product
                {
                    Id = Guid.Parse("20000000-0000-0000-0000-000000000001"),
                    ProductName = "Hoa Cưới Cầm Tay",
                    Description = "Hoa cưới cầm tay tinh tế cho cô dâu",
                    Price = 500000,
                    StockQuantity = 20,
                    CategoryId = categoryId2,
                    IsActive = true,
                    IsFeatured = true,
                    CreatedAt = DateTime.UtcNow
                },
                new Product
                {
                    Id = Guid.Parse("20000000-0000-0000-0000-000000000002"),
                    ProductName = "Hoa Cài Áo Chú Rể",
                    Description = "Hoa cài áo thanh lịch cho chú rể",
                    Price = 50000,
                    StockQuantity = 50,
                    CategoryId = categoryId2,
                    IsActive = true,
                    IsFeatured = false,
                    CreatedAt = DateTime.UtcNow
                },
                new Product
                {
                    Id = Guid.Parse("20000000-0000-0000-0000-000000000003"),
                    ProductName = "Trang Trí Bàn Tiệc Cưới",
                    Description = "Hoa trang trí bàn tiệc cưới lãng mạn",
                    Price = 300000,
                    StockQuantity = 15,
                    CategoryId = categoryId2,
                    IsActive = true,
                    IsFeatured = true,
                    CreatedAt = DateTime.UtcNow
                },

                // Hoa Sinh Nhật
                new Product
                {
                    Id = Guid.Parse("30000000-0000-0000-0000-000000000001"),
                    ProductName = "Bó Hoa Sinh Nhật Rực Rỡ",
                    Description = "Bó hoa sinh nhật đầy màu sắc và ý nghĩa",
                    Price = 180000,
                    StockQuantity = 30,
                    CategoryId = categoryId3,
                    IsActive = true,
                    IsFeatured = true,
                    CreatedAt = DateTime.UtcNow
                },
                new Product
                {
                    Id = Guid.Parse("30000000-0000-0000-0000-000000000002"),
                    ProductName = "Giỏ Hoa Sinh Nhật",
                    Description = "Giỏ hoa sinh nhật xinh xắn, đáng yêu",
                    Price = 220000,
                    DiscountPrice = 200000,
                    StockQuantity = 20,
                    CategoryId = categoryId3,
                    IsActive = true,
                    IsFeatured = false,
                    CreatedAt = DateTime.UtcNow
                },
                new Product
                {
                    Id = Guid.Parse("30000000-0000-0000-0000-000000000003"),
                    ProductName = "Hoa Hướng Dương Sinh Nhật",
                    Description = "Hoa hướng dương tươi vui cho sinh nhật",
                    Price = 160000,
                    StockQuantity = 35,
                    CategoryId = categoryId3,
                    IsActive = true,
                    IsFeatured = false,
                    CreatedAt = DateTime.UtcNow
                },

                // Hoa Khai Trương
                new Product
                {
                    Id = Guid.Parse("40000000-0000-0000-0000-000000000001"),
                    ProductName = "Lẵng Hoa Khai Trương Sang Trọng",
                    Description = "Lẵng hoa khai trương sang trọng, mang lại may mắn",
                    Price = 800000,
                    StockQuantity = 15,
                    CategoryId = categoryId4,
                    IsActive = true,
                    IsFeatured = true,
                    CreatedAt = DateTime.UtcNow
                },
                new Product
                {
                    Id = Guid.Parse("40000000-0000-0000-0000-000000000002"),
                    ProductName = "Chậu Hoa Khai Trương",
                    Description = "Chậu hoa khai trương tươi lâu",
                    Price = 350000,
                    StockQuantity = 25,
                    CategoryId = categoryId4,
                    IsActive = true,
                    IsFeatured = false,
                    CreatedAt = DateTime.UtcNow
                },

                // Hoa Chia Buồn
                new Product
                {
                    Id = Guid.Parse("50000000-0000-0000-0000-000000000001"),
                    ProductName = "Vòng Hoa Chia Buồn",
                    Description = "Vòng hoa chia buồn trang trọng",
                    Price = 600000,
                    StockQuantity = 10,
                    CategoryId = categoryId5,
                    IsActive = true,
                    IsFeatured = false,
                    CreatedAt = DateTime.UtcNow
                },
                new Product
                {
                    Id = Guid.Parse("50000000-0000-0000-0000-000000000002"),
                    ProductName = "Lẵng Hoa Chia Buồn",
                    Description = "Lẵng hoa chia buồn thanh tịnh",
                    Price = 450000,
                    StockQuantity = 12,
                    CategoryId = categoryId5,
                    IsActive = true,
                    IsFeatured = false,
                    CreatedAt = DateTime.UtcNow
                },

                // Hoa Valentine
                new Product
                {
                    Id = Guid.Parse("60000000-0000-0000-0000-000000000001"),
                    ProductName = "Bó Hoa Valentine Đặc Biệt",
                    Description = "Bó hoa Valentine đặc biệt cho người yêu",
                    Price = 350000,
                    DiscountPrice = 300000,
                    StockQuantity = 40,
                    CategoryId = categoryId6,
                    IsActive = true,
                    IsFeatured = true,
                    CreatedAt = DateTime.UtcNow
                },
                new Product
                {
                    Id = Guid.Parse("60000000-0000-0000-0000-000000000002"),
                    ProductName = "Hộp Hoa Valentine",
                    Description = "Hộp hoa Valentine sang trọng",
                    Price = 280000,
                    StockQuantity = 30,
                    CategoryId = categoryId6,
                    IsActive = true,
                    IsFeatured = true,
                    CreatedAt = DateTime.UtcNow
                }
            );
        }
    }
}
