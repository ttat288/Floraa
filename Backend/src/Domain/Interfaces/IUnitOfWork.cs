using Domain.Entities;

namespace Domain.Interfaces;

public interface IUnitOfWork : IDisposable
{
    IRepository<User> Users { get; }
    IRepository<Product> Products { get; }
    IRepository<Category> Categories { get; }
    IRepository<Shop> Shops { get; }
    IRepository<Order> Orders { get; }
    IRepository<OrderDetail> OrderDetails { get; }
    IRepository<Theme> Themes { get; }
    IRepository<Color> Colors { get; }
    IRepository<Occasion> Occasions { get; }
    IRepository<SubProduct> SubProducts { get; }
    IRepository<Cart> Carts { get; }
    IRepository<Storage> Storages { get; }
    IRepository<StorageLog> StorageLogs { get; }
    IRepository<Discount> Discounts { get; }
    IRepository<Voucher> Vouchers { get; }
    IRepository<UserVoucher> UserVouchers { get; }
    IRepository<Payment> Payments { get; }
    IRepository<Notification> Notifications { get; }
    IRepository<RefreshToken> RefreshTokens { get; }
    IRepository<ActivityLog> ActivityLogs { get; }
    IRepository<RecipientType> RecipientTypes { get; }
    IRepository<TargetAudience> TargetAudiences { get; }
    IRepository<ProductTarget> ProductTargets { get; }
    IRepository<Employee> Employees { get; }
    IRepository<ProductCategory> ProductCategories { get; }
    IRepository<ProductOccasion> ProductOccasions { get; }
    IRepository<ShopCategory> ShopCategories { get; }
    IRepository<AccountActivation> AccountActivations { get; }

    Task<int> SaveChangesAsync();
    Task BeginTransactionAsync();
    Task CommitTransactionAsync();
    Task RollbackTransactionAsync();
}
