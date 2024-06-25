namespace Progi.BidCalculator.Infrastructure.Caching;

public interface ICacheManager
{
    Task<T> GetOrCreateAsync<T>(object key, Func<Task<T>> factory, int? expirationInSeconds = null);
    void ClearCacheByKey(object key);
    void Refresh<T>(string key, T value);
    void Remove(string key);
}