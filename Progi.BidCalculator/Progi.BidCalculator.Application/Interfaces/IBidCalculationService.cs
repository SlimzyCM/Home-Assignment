using Progi.BidCalculator.Core.Entities;
using Progi.BidCalculator.Core.Models;

namespace Progi.BidCalculator.Application.Interfaces;

public interface IBidCalculationService
{
    Task<BidCalculationResult> CalculateBidAsync(VehicleType vehicleType, decimal price);
}