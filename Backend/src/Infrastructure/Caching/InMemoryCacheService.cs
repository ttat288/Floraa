using Application.Common.Interfaces;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using System.Collections.Concurrent;
using System.Text.Json;

namespace Infrastructure.Caching;

public class InMemoryCacheService : ICacheService
{
    private readonly IMemoryCache _cache;
    private readonly CacheConfiguration _configuration;
    private readonly ConcurrentDictionary<string, bool> _cacheKeys;

    public InMemoryCacheService(IMemoryCache cache, IOptions<CacheConfiguration> configuration)
    {
        _cache = cache;
        _configuration = configuration.Value;
        _cacheKeys = new ConcurrentDictionary<string, bool>();
    }

    public Task<T?> GetAsync<T>(string key, CancellationToken cancellationToken = default) where T : class
    {
        var fullKey = GetFullKey(key);
        var value = _cache.Get<T>(fullKey);
        return Task.FromResult(value);
    }

    public Task SetAsync<T>(string key, T value, TimeSpan? expiration = null, CancellationToken cancellationToken = default) where T : class
    {
        var fullKey = GetFullKey(key);
        var options = new MemoryCacheEntryOptions();
        
        if (expiration.HasValue)
        {
            options.AbsoluteExpirationRelativeToNow = expiration;
        }
        else
        {
            options.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(_configuration.DefaultExpirationMinutes);
        }

        options.RegisterPostEvictionCallback((k, v, r, s) =>
        {
            _cacheKeys.TryRemove(k.ToString()!, out _);
        });

        _cache.Set(fullKey, value, options);
        _cacheKeys.TryAdd(fullKey, true);
        
        return Task.CompletedTask;
    }

    public Task RemoveAsync(string key, CancellationToken cancellationToken = default)
    {
        var fullKey = GetFullKey(key);
        _cache.Remove(fullKey);
        _cacheKeys.TryRemove(fullKey, out _);
        return Task.CompletedTask;
    }

    public Task RemoveByPatternAsync(string pattern, CancellationToken cancellationToken = default)
    {
        var fullPattern = GetFullKey(pattern);
        var keysToRemove = _cacheKeys.Keys
            .Where(k => IsMatch(k, fullPattern))
            .ToList();

        foreach (var key in keysToRemove)
        {
            _cache.Remove(key);
            _cacheKeys.TryRemove(key, out _);
        }

        return Task.CompletedTask;
    }

    public Task ClearAsync(CancellationToken cancellationToken = default)
    {
        var keysToRemove = _cacheKeys.Keys.ToList();
        
        foreach (var key in keysToRemove)
        {
            _cache.Remove(key);
            _cacheKeys.TryRemove(key, out _);
        }

        return Task.CompletedTask;
    }

    public Task<bool> ExistsAsync(string key, CancellationToken cancellationToken = default)
    {
        var fullKey = GetFullKey(key);
        var exists = _cache.TryGetValue(fullKey, out _);
        return Task.FromResult(exists);
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

    private static bool IsMatch(string key, string pattern)
    {
        // Simple pattern matching for * wildcard
        if (pattern.EndsWith("*"))
        {
            var prefix = pattern[..^1];
            return key.StartsWith(prefix);
        }
        
        return key == pattern;
    }
}
