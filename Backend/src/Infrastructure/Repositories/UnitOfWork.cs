using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore.Storage;

namespace Infrastructure.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly FloraDbContext _context;
    private IDbContextTransaction? _transaction;

    public UnitOfWork(FloraDbContext context)
    {
        _context = context;
        Users = new Repository<User>(_context);
        Products = new Repository<Product>(_context);
        Categories = new Repository<Category>(_context);
        Shops = new Repository<Shop>(_context);
        Orders = new Repository<Order>(_context);
        OrderDetails = new Repository<OrderDetail>(_context);
        Themes = new Repository<Theme>(_context);
        Colors = new Repository<Color>(_context);
        Occasions = new Repository<Occasion>(_context);
        SubProducts = new Repository<SubProduct>(_context);
        Carts = new Repository<Cart>(_context);
        Storages = new Repository<Storage>(_context);
        StorageLogs = new Repository<StorageLog>(_context);
        Discounts = new Repository<Discount>(_context);
        Vouchers = new Repository<Voucher>(_context);
        UserVouchers = new Repository<UserVoucher>(_context);
        Payments = new Repository<Payment>(_context);
        Notifications = new Repository<Notification>(_context);
        RefreshTokens = new Repository<RefreshToken>(_context);
        ActivityLogs = new Repository<ActivityLog>(_context);
        RecipientTypes = new Repository<RecipientType>(_context);
        TargetAudiences = new Repository<TargetAudience>(_context);
        ProductTargets = new Repository<ProductTarget>(_context);
        Employees = new Repository<Employee>(_context);
        ProductCategories = new Repository<ProductCategory>(_context);
        ProductOccasions = new Repository<ProductOccasion>(_context);
        ShopCategories = new Repository<ShopCategory>(_context);
        AccountActivations = new Repository<AccountActivation>(_context);
    }

    public IRepository<User> Users { get; }
    public IRepository<Product> Products { get; }
    public IRepository<Category> Categories { get; }
    public IRepository<Shop> Shops { get; }
    public IRepository<Order> Orders { get; }
    public IRepository<OrderDetail> OrderDetails { get; }
    public IRepository<Theme> Themes { get; }
    public IRepository<Color> Colors { get; }
    public IRepository<Occasion> Occasions { get; }
    public IRepository<SubProduct> SubProducts { get; }
    public IRepository<Cart> Carts { get; }
    public IRepository<Storage> Storages { get; }
    public IRepository<StorageLog> StorageLogs { get; }
    public IRepository<Discount> Discounts { get; }
    public IRepository<Voucher> Vouchers { get; }
    public IRepository<UserVoucher> UserVouchers { get; }
    public IRepository<Payment> Payments { get; }
    public IRepository<Notification> Notifications { get; }
    public IRepository<RefreshToken> RefreshTokens { get; }
    public IRepository<ActivityLog> ActivityLogs { get; }
    public IRepository<RecipientType> RecipientTypes { get; }
    public IRepository<TargetAudience> TargetAudiences { get; }
    public IRepository<ProductTarget> ProductTargets { get; }
    public IRepository<Employee> Employees { get; }
    public IRepository<ProductCategory> ProductCategories { get; }
    public IRepository<ProductOccasion> ProductOccasions { get; }
    public IRepository<ShopCategory> ShopCategories { get; }
    public IRepository<AccountActivation> AccountActivations { get; }

    public async Task<int> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync();
    }

    public async Task BeginTransactionAsync()
    {
        _transaction = await _context.Database.BeginTransactionAsync();
    }

    public async Task CommitTransactionAsync()
    {
        if (_transaction != null)
        {
            await _transaction.CommitAsync();
            await _transaction.DisposeAsync();
            _transaction = null;
        }
    }

    public async Task RollbackTransactionAsync()
    {
        if (_transaction != null)
        {
            await _transaction.RollbackAsync();
            await _transaction.DisposeAsync();
            _transaction = null;
        }
    }

    public void Dispose()
    {
        _transaction?.Dispose();
        _context.Dispose();
    }
}
