namespace Application.Common.Constants;

public static class CacheKeys
{
    // User cache keys
    private const string USER_BY_ID = "user:id:{0}";
    private const string USER_BY_EMAIL = "user:email:{0}";
    
    // Product cache keys
    public const string PRODUCTS_LIST = "products:list";
    private const string PRODUCT_BY_ID = "product:id:{0}";
    
    // Category cache keys
    private const string CATEGORIES_LIST = "categories:list";
    private const string CATEGORY_BY_ID = "category:id:{0}";

    public static string UserById(Guid userId) => string.Format(USER_BY_ID, userId);
    public static string UserByEmail(string email) => string.Format(USER_BY_EMAIL, email.ToLowerInvariant());
    public static string ProductById(Guid productId) => string.Format(PRODUCT_BY_ID, productId);
    public static string ProductsList(int page, int pageSize) => $"{PRODUCTS_LIST}:page:{page}:size:{pageSize}";
    public static string CategoriesList() => CATEGORIES_LIST;
    public static string CategoryById(Guid categoryId) => string.Format(CATEGORY_BY_ID, categoryId);
}

public static class CacheSettings
{
    public static readonly TimeSpan UserCacheExpiry = TimeSpan.FromMinutes(30);
    public static readonly TimeSpan ProductCacheExpiry = TimeSpan.FromMinutes(15);
    public static readonly TimeSpan CategoryCacheExpiry = TimeSpan.FromHours(1);
}
