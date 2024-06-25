using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Primitives;

namespace Progi.BidCalculator.Tests.Unit.Helpers;

public class FakeMemoryCache : IMemoryCache
{
    private readonly Dictionary<object, object> _cache = [];

    public ICacheEntry CreateEntry(object key)
    {
        return new FakeCacheEntry(key, _cache);
    }

    public void Dispose()
    {
    }

    public void Remove(object key)
    {
    }

    public bool TryGetValue(object key, out object value)
    {
        return _cache.TryGetValue(key, out value);
    }
}

public class FakeCacheEntry(object key, IDictionary<object, object> cache) : ICacheEntry
{
    public object Key => key;


    public void Dispose()
    {
    }

    public object Value
    {
        get => cache[key];
        set => cache[key] = value;
    }

    public DateTimeOffset? AbsoluteExpiration { get; set; }

    public TimeSpan? AbsoluteExpirationRelativeToNow { get; set; }

    public TimeSpan? SlidingExpiration { get; set; }

    public IList<IChangeToken> ExpirationTokens { get; }

    public IList<PostEvictionCallbackRegistration> PostEvictionCallbacks { get; }

    public CacheItemPriority Priority { get; set; }

    public long? Size { get; set; }
}