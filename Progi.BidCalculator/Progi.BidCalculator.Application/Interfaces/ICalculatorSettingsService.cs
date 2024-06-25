
using Progi.BidCalculator.Core.Entities;

namespace Progi.BidCalculator.Application.Interfaces;

public interface ICalculatorSettingsService
{
    Task<CalculatorSetting?> GetSettingAsync(FeeType feeType, VehicleType vehicleType);

    Task<IReadOnlyList<CalculatorSetting>> GetSettingsAsync(FeeType feeType);
}