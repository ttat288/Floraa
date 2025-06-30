namespace Infrastructure.Caching;

public class CacheConfiguration
{
    public const string SectionName = "Cache";
    
    public bool EnableCaching { get; set; } = true;
    public CacheType Type { get; set; } = CacheType.InMemory;
    public string? RedisConnectionString { get; set; }
    public int DefaultExpirationMinutes { get; set; } = 30;
    public bool EnableCompression { get; set; } = false;
    public string? KeyPrefix { get; set; }
}

public enum CacheType
{
    InMemory,
    Redis,
    None
}
