using Application.Common.Interfaces;
using Microsoft.Extensions.Options;
using StackExchange.Redis;
using System.Text.Json;

namespace Infrastructure.Caching;

public class RedisCacheService : ICacheService
{
    private readonly IDatabase _database;
    private readonly IConnectionMultiplexer _connectionMultiplexer;
    private readonly CacheConfiguration _configuration;

    public RedisCacheService(IConnectionMultiplexer connectionMultiplexer, IOptions<CacheConfiguration> configuration)
    {
        _connectionMultiplexer = connectionMultiplexer;
        _database = connectionMultiplexer.GetDatabase();
        _configuration = configuration.Value;
    }

    public async Task<T?> GetAsync<T>(string key, CancellationToken cancellationToken = default) where T : class
    {
        var fullKey = GetFullKey(key);
        var value = await _database.StringGetAsync(fullKey);
        
        return value.HasValue ? JsonSerializer.Deserialize<T>(value!) : null;
    }

    public async Task SetAsync<T>(string key, T value, TimeSpan? expiration = null, CancellationToken cancellationToken = default) where T : class
    {
        var fullKey = GetFullKey(key);
        var serializedValue = JsonSerializer.Serialize(value);
        
        var exp = expiration ?? TimeSpan.FromMinutes(_configuration.DefaultExpirationMinutes);
        await _database.StringSetAsync(fullKey, serializedValue, exp);
    }

    public async Task RemoveAsync(string key, CancellationToken cancellationToken = default)
    {
        var fullKey = GetFullKey(key);
        await _database.KeyDeleteAsync(fullKey);
    }

    public async Task RemoveByPatternAsync(string pattern, CancellationToken cancellationToken = default)
    {
        var fullPattern = GetFullKey(pattern);
        var server = _connectionMultiplexer.GetServer(_connectionMultiplexer.GetEndPoints()[0]);
        var keys = server.Keys(pattern: fullPattern).ToArray();
        
        if (keys.Length > 0)
        {
            await _database.KeyDeleteAsync(keys);
        }
    }

    public async Task ClearAsync(CancellationToken cancellationToken = default)
    {
        var server = _connectionMultiplexer.GetServer(_connectionMultiplexer.GetEndPoints()[0]);
        await server.FlushDatabaseAsync();
    }

    public async Task<bool> ExistsAsync(string key, CancellationToken cancellationToken = default)
    {
        var fullKey = GetFullKey(key);
        return await _database.KeyExistsAsync(fullKey);
    }

    public async Task<T> GetOrSetAsync<T>(string key, Func<Task<T>> getItem, TimeSpan? expiration = null, CancellationToken cancellationToken = default) where T : class
    {
        var cachedValue = await GetAsync<T>(key, cancellationToken);
        if (cachedValue != null)
        {
            return cachedValue;
        }

        var value = await getItem();
        if (value != null)
        {
            await SetAsync(key, value, expiration, cancellationToken);
        }

        return value;
    }

    private string GetFullKey(string key)
    {
        return string.IsNullOrEmpty(_configuration.KeyPrefix) 
            ? key 
            : $"{_configuration.KeyPrefix}:{key}";
    }
}
