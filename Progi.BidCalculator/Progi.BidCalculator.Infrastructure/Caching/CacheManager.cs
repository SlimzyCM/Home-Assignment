using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;

namespace Progi.BidCalculator.Infrastructure.Caching;

public class CacheManager(IMemoryCache memoryCache, IOptions<CacheOptions> cacheOptions) : ICacheManager
{
    private readonly CacheOptions _cacheOptions = cacheOptions.Value;

    public async Task<T> GetOrCreateAsync<T>(object key, Func<Task<T>> factory, int? expirationInSeconds = null)
    {
#pragma warning disable CS8603 // Possible null reference return.
        return await memoryCache.GetOrCreateAsync(
            key,
            async cacheEntry =>
            {
                cacheEntry.Size = 1;
                cacheEntry.AbsoluteExpirationRelativeToNow = expirationInSeconds.HasValue ? TimeSpan.FromSeconds(expirationInSeconds.Value) : _cacheOptions.Expiration;

                var value = await factory();
                return value;
            });
#pragma warning restore CS8603 // Possible null reference return.
    }
    public void ClearCacheByKey(object key)
    {
        memoryCache.Remove(key);
    }
    public void Refresh<T>(string key, T value)
    {
        var cacheEntryOptions = new MemoryCacheEntryOptions()
        {
            Size = 1
        };

        memoryCache.Set(key, value, cacheEntryOptions);
    }

    public void Remove(string key)
    {
        memoryCache.Remove(key);
    }
}