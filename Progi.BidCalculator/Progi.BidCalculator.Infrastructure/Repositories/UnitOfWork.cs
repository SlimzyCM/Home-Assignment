using Progi.BidCalculator.Infrastructure.Data;

namespace Progi.BidCalculator.Infrastructure.Repositories;

public sealed class UnitOfWork(CalculatorContext context) : IUnitOfWork
{
    public async Task CompleteAsync()
    {
        await context.SaveChangesAsync();
    }
}