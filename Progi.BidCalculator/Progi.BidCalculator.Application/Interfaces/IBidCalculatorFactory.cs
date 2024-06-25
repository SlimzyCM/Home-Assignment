using Progi.BidCalculator.Core.Entities;
using Progi.BidCalculator.Core.Interfaces;

namespace Progi.BidCalculator.Application.Interfaces;

public interface IBidCalculatorFactory
{
    IBidCalculator CreateBidCalculator(VehicleType vehicleType);
}