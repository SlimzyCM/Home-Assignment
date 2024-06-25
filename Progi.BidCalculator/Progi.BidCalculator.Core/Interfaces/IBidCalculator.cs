using Progi.BidCalculator.Core.Models;

namespace Progi.BidCalculator.Core.Interfaces;

public interface IBidCalculator
{
    Task<BidCalculationResult> CalculateBidAsync(decimal basePrice);
}