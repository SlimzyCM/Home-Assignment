using Microsoft.EntityFrameworkCore;
using Progi.BidCalculator.Application.Interfaces;
using Progi.BidCalculator.Core.Entities;
using Progi.BidCalculator.Core.Interfaces.DataAccess;
using Progi.BidCalculator.Infrastructure.Caching;

namespace Progi.BidCalculator.Application.Services;

public sealed class CalculatorSettingsService(ICachedRepository<CalculatorSetting> repository, ICacheManager cache) : ICalculatorSettingsService
{
    private const string KeyPrefix = nameof(CalculatorSettingsService);

    public async Task<IReadOnlyList<CalculatorSetting>> GetSettingsAsync(FeeType feeType)
    {
        return await cache.GetOrCreateAsync($"{KeyPrefix}_{feeType}", 
            async () => await repository.GetByExpressionAsync(s => s.FeeType == feeType)
                .ToListAsync());
    }

    public async Task<CalculatorSetting?> GetSettingAsync(FeeType feeType, VehicleType vehicleType)
    {
        return await cache.GetOrCreateAsync($"{KeyPrefix}_{feeType}_{vehicleType}",
            async () => await repository.GetByExpressionAsync(s => s.FeeType == feeType && s.VehicleType == vehicleType)
                .FirstOrDefaultAsync());
    }
}