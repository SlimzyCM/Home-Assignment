namespace Progi.BidCalculator.Infrastructure.Caching;

public class CacheOptions
{
    public int Size { get; set; }

    public TimeSpan Expiration { get; set; }
}