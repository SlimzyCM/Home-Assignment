using Progi.BidCalculator.Application.Interfaces;
using Progi.BidCalculator.Core.Entities;
using Progi.BidCalculator.Core.Exceptions;
using Progi.BidCalculator.Core.Models;

namespace Progi.BidCalculator.Application.Services;

public sealed class BidCalculationService(IBidCalculatorFactory bidCalculatorFactory) : IBidCalculationService
{
    public async Task<BidCalculationResult> CalculateBidAsync(VehicleType vehicleType, decimal price)
    {
        if (price <= 0)
            throw new ValidationException("base price must be greater than zero");
        
        var calculator = bidCalculatorFactory.CreateBidCalculator(vehicleType);
        var calculateResult = await calculator.CalculateBidAsync(price);
        
        return calculateResult;
    }
}