using Microsoft.Extensions.DependencyInjection;
using Progi.BidCalculator.Application.Interfaces;
using Progi.BidCalculator.Core.Entities;
using Progi.BidCalculator.Core.Exceptions;
using Progi.BidCalculator.Core.Interfaces;

namespace Progi.BidCalculator.Application.Services;

public sealed class BidCalculatorFactory(IServiceProvider serviceProvider) : IBidCalculatorFactory
{
    public IBidCalculator CreateBidCalculator(VehicleType vehicleType)
    {
        var calculator = serviceProvider.GetKeyedService<IBidCalculator>(vehicleType.ToString());

        return calculator ?? throw new ResourceNotFoundException("Unsupported vehicle type");
    }
}