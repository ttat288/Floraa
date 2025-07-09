using Microsoft.EntityFrameworkCore;
using Domain.Entities;
using Domain.Common;
using System.Linq.Expressions;

namespace Infrastructure.Data;

public class FloraDbContext : DbContext
{
    public FloraDbContext(DbContextOptions<FloraDbContext> options) : base(options)
    {
    }

    // DbSets
    public DbSet<User> Users { get; set; }
    public DbSet<Shop> Shops { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderDetail> OrderDetails { get; set; }
    public DbSet<Theme> Themes { get; set; }
    public DbSet<Color> Colors { get; set; }
    public DbSet<Occasion> Occasions { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<SubProduct> SubProducts { get; set; }
    public DbSet<Cart> Carts { get; set; }
    public DbSet<Storage> Storages { get; set; }
    public DbSet<StorageLog> StorageLogs { get; set; }
    public DbSet<Discount> Discounts { get; set; }
    public DbSet<Voucher> Vouchers { get; set; }
    public DbSet<UserVoucher> UserVouchers { get; set; }
    public DbSet<Payment> Payments { get; set; }
    public DbSet<Notification> Notifications { get; set; }
    public DbSet<RefreshToken> RefreshTokens { get; set; }
    public DbSet<ActivityLog> ActivityLogs { get; set; }
    public DbSet<RecipientType> RecipientTypes { get; set; }
    public DbSet<TargetAudience> TargetAudiences { get; set; }
    public DbSet<ProductTarget> ProductTargets { get; set; }
    public DbSet<Employee> Employees { get; set; }
    public DbSet<AccountActivation> AccountActivations { get; set; }

    // Junction tables
    public DbSet<ProductCategory> ProductCategories { get; set; }
    public DbSet<ProductOccasion> ProductOccasions { get; set; }
    public DbSet<ShopCategory> ShopCategories { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configure unique constraints
        modelBuilder.Entity<User>()
            .HasIndex(u => u.Email)
            .IsUnique();

        modelBuilder.Entity<Order>()
            .HasIndex(o => o.Code)
            .IsUnique();

        modelBuilder.Entity<Voucher>()
            .HasIndex(v => v.Code)
            .IsUnique();

        modelBuilder.Entity<RefreshToken>()
            .HasIndex(rt => rt.RefreshTokenCode)
            .IsUnique();

        modelBuilder.Entity<AccountActivation>()
            .HasIndex(aa => aa.ActivationToken)
            .IsUnique();

        // Configure composite unique constraints
        modelBuilder.Entity<Storage>()
            .HasIndex(s => new { s.ProductId, s.ShopId })
            .IsUnique();

        modelBuilder.Entity<UserVoucher>()
            .HasIndex(uv => new { uv.UserId, uv.VoucherId })
            .IsUnique();

        modelBuilder.Entity<ProductTarget>()
            .HasIndex(pt => new { pt.ProductId, pt.TargetAudienceId })
            .IsUnique();

        modelBuilder.Entity<Employee>()
            .HasIndex(e => new { e.UserId, e.ShopId })
            .IsUnique();

        modelBuilder.Entity<TargetAudience>()
            .HasIndex(ta => new { ta.OccasionId, ta.RecipientTypeId })
            .IsUnique();

        // Configure composite unique constraints for junction tables
        modelBuilder.Entity<ProductCategory>()
            .HasIndex(pc => new { pc.ProductId, pc.CategoryId })
            .IsUnique();

        modelBuilder.Entity<ProductOccasion>()
            .HasIndex(po => new { po.ProductId, po.OccasionId })
            .IsUnique();

        modelBuilder.Entity<ShopCategory>()
            .HasIndex(sc => new { sc.ShopId, sc.CategoryId })
            .IsUnique();

        // Configure relationships with cascade delete restrictions
        modelBuilder.Entity<Order>()
            .HasOne(o => o.User)
            .WithMany(u => u.Orders)
            .HasForeignKey(o => o.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Order>()
            .HasOne(o => o.Shop)
            .WithMany(s => s.Orders)
            .HasForeignKey(o => o.ShopId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Product>()
            .HasOne(p => p.Shop)
            .WithMany(s => s.Products)
            .HasForeignKey(p => p.ShopId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Product>()
            .HasOne(p => p.SubProduct)
            .WithMany(sp => sp.Products)
            .HasForeignKey(p => p.SubProductId)
            .OnDelete(DeleteBehavior.SetNull);

        modelBuilder.Entity<StorageLog>()
            .HasOne(sl => sl.CreatedByUser)
            .WithMany()
            .HasForeignKey(sl => sl.CreatedByUserId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<StorageLog>()
            .HasOne(sl => sl.Product)
            .WithMany(p => p.StorageLogs)
            .HasForeignKey(sl => sl.ProductId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<StorageLog>()
            .HasOne(sl => sl.Shop)
            .WithMany(s => s.StorageLogs)
            .HasForeignKey(sl => sl.ShopId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Employee>()
            .HasOne(e => e.User)
            .WithMany(u => u.Employees)
            .HasForeignKey(e => e.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Employee>()
            .HasOne(e => e.Shop)
            .WithMany(s => s.Employees)
            .HasForeignKey(e => e.ShopId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Shop>()
            .HasOne(s => s.User)
            .WithMany(u => u.Shops)
            .HasForeignKey(s => s.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<TargetAudience>()
            .HasOne(ta => ta.Occasion)
            .WithMany(o => o.TargetAudiences)
            .HasForeignKey(ta => ta.OccasionId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<TargetAudience>()
            .HasOne(ta => ta.RecipientType)
            .WithMany(rt => rt.TargetAudiences)
            .HasForeignKey(ta => ta.RecipientTypeId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<AccountActivation>()
            .HasOne(aa => aa.User)
            .WithMany()
            .HasForeignKey(aa => aa.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        // Configure decimal precision
        modelBuilder.Entity<Product>()
            .Property(p => p.Price)
            .HasPrecision(18, 2);

        modelBuilder.Entity<Order>()
            .Property(o => o.TotalPrice)
            .HasPrecision(18, 2);

        modelBuilder.Entity<OrderDetail>()
            .Property(od => od.Price)
            .HasPrecision(18, 2);

        modelBuilder.Entity<OrderDetail>()
            .Property(od => od.PriceAfterDiscount)
            .HasPrecision(18, 2);

        modelBuilder.Entity<OrderDetail>()
            .Property(od => od.Percent)
            .HasPrecision(5, 2);

        modelBuilder.Entity<Discount>()
            .Property(d => d.Percent)
            .HasPrecision(5, 2);

        modelBuilder.Entity<Voucher>()
            .Property(v => v.Percent)
            .HasPrecision(5, 2);

        modelBuilder.Entity<Voucher>()
            .Property(v => v.MinOrderValue)
            .HasPrecision(18, 2);

        modelBuilder.Entity<Voucher>()
            .Property(v => v.MaxDiscountAmount)
            .HasPrecision(18, 2);

        // Global query filter CHỈ cho BaseSoftDeleteEntity
        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            if (typeof(BaseSoftDeleteEntity).IsAssignableFrom(entityType.ClrType))
            {
                var parameter = Expression.Parameter(entityType.ClrType, "e");
                var property = Expression.Property(parameter, nameof(BaseSoftDeleteEntity.IsDeleted));
                var condition = Expression.Equal(property, Expression.Constant(false));
                var lambda = Expression.Lambda(condition, parameter);

                modelBuilder.Entity(entityType.ClrType).HasQueryFilter(lambda);
            }
        }

        // Seed data
        SeedData(modelBuilder);
    }

    private void SeedData(ModelBuilder modelBuilder)
    {
        // Seed Themes
        var themes = new[]
        {
            new Theme { Id = Guid.NewGuid(), Name = "Romantic", Description = "Romantic flower arrangements", IsActive = true, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
            new Theme { Id = Guid.NewGuid(), Name = "Classic", Description = "Classic flower arrangements", IsActive = true, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
            new Theme { Id = Guid.NewGuid(), Name = "Modern", Description = "Modern flower arrangements", IsActive = true, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow }
        };
        modelBuilder.Entity<Theme>().HasData(themes);

        // Seed Colors
        var colors = new[]
        {
            new Color { Id = Guid.NewGuid(), Name = "Red", Description = "Red flowers", IsActive = true, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
            new Color { Id = Guid.NewGuid(), Name = "White", Description = "White flowers", IsActive = true, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
            new Color { Id = Guid.NewGuid(), Name = "Pink", Description = "Pink flowers", IsActive = true, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
            new Color { Id = Guid.NewGuid(), Name = "Yellow", Description = "Yellow flowers", IsActive = true, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow }
        };
        modelBuilder.Entity<Color>().HasData(colors);

        // Seed Occasions
        var occasions = new[]
        {
            new Occasion { Id = Guid.NewGuid(), Name = "Birthday", IsActive = true, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
            new Occasion { Id = Guid.NewGuid(), Name = "Anniversary", IsActive = true, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
            new Occasion { Id = Guid.NewGuid(), Name = "Valentine's Day", IsActive = true, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
            new Occasion { Id = Guid.NewGuid(), Name = "Mother's Day", IsActive = true, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
            new Occasion { Id = Guid.NewGuid(), Name = "Wedding", IsActive = true, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow }
        };
        modelBuilder.Entity<Occasion>().HasData(occasions);

        // Seed Categories
        var categories = new[]
        {
            new Category { Id = Guid.NewGuid(), Name = "Roses", Description = "Rose flowers", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
            new Category { Id = Guid.NewGuid(), Name = "Lilies", Description = "Lily flowers", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
            new Category { Id = Guid.NewGuid(), Name = "Tulips", Description = "Tulip flowers", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
            new Category { Id = Guid.NewGuid(), Name = "Orchids", Description = "Orchid flowers", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow }
        };
        modelBuilder.Entity<Category>().HasData(categories);

        // Seed Recipient Types
        var recipientTypes = new[]
        {
            new RecipientType { Id = Guid.NewGuid(), Name = "Lover", IsActive = true, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
            new RecipientType { Id = Guid.NewGuid(), Name = "Family", IsActive = true, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
            new RecipientType { Id = Guid.NewGuid(), Name = "Friend", IsActive = true, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
            new RecipientType { Id = Guid.NewGuid(), Name = "Colleague", IsActive = true, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow }
        };
        modelBuilder.Entity<RecipientType>().HasData(recipientTypes);
    }
}
