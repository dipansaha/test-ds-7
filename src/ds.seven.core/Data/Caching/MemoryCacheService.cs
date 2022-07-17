using Microsoft.Extensions.Caching.Memory;

namespace ds.seven.core.Data.Caching;

public class MemoryCacheService : ICacheService
{
    private readonly IMemoryCache _cache;

    public MemoryCacheService(IMemoryCache cache)
    {
        _cache = cache;
    }
    
    public string Get(string key)
    {
        var inCache = _cache.TryGetValue(key, out string cachedToken);
        return inCache ? cachedToken : string.Empty;
    }

    public void Set(string key, string value, int expiryInSeconds)
    {
        var cacheOptions = new MemoryCacheEntryOptions()
            .SetAbsoluteExpiration(TimeSpan.FromSeconds(expiryInSeconds))
            .SetPriority(CacheItemPriority.NeverRemove);

        _cache.Set(key, value, cacheOptions);
    }

    public void Remove(string key)
    {
        _cache.Remove(key);
    }
}