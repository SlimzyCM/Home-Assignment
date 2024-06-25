namespace Progi.BidCalculator.Infrastructure.Data;

public interface IUnitOfWork
{
    Task CompleteAsync();
}