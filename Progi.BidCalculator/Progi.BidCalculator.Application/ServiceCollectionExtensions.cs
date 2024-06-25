using Microsoft.Extensions.DependencyInjection;
using Progi.BidCalculator.Application.BidCalculators;
using Progi.BidCalculator.Application.Interfaces;
using Progi.BidCalculator.Application.Services;
using Progi.BidCalculator.Core.Entities;
using Progi.BidCalculator.Core.Interfaces;

namespace Progi.BidCalculator.Application;

public static class ServiceCollectionExtensions
{
    public static void AddCalculatorServices(this IServiceCollection services)
    {
        services.AddScoped<ICalculatorSettingsService, CalculatorSettingsService>();
        services.AddScoped<IBidCalculationService, BidCalculationService>();
        services.AddScoped<IBidCalculatorFactory, BidCalculatorFactory>();
        services.AddKeyedScoped<IBidCalculator, CommonCalculator>(VehicleType.Common.ToString());
        services.AddKeyedScoped<IBidCalculator, LuxuryCalculator>(VehicleType.Luxury.ToString());
    }
}