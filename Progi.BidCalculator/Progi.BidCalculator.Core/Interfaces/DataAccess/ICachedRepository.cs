namespace Progi.BidCalculator.Core.Interfaces.DataAccess;

public interface ICachedRepository<T> : IRepository<T> where T : class
{
}