using Microsoft.Extensions.Logging;
using Progi.BidCalculator.Application.Interfaces;
using Progi.BidCalculator.Core.Entities;
using Progi.BidCalculator.Core.Exceptions;
using Progi.BidCalculator.Core.Interfaces;
using Progi.BidCalculator.Core.Models;

namespace Progi.BidCalculator.Application.BidCalculators;

public abstract class BaseCalculator(ICalculatorSettingsService settingsService, ILogger<BaseCalculator> logger, VehicleType type) : IBidCalculator
{
    public async Task<BidCalculationResult> CalculateBidAsync(decimal basePrice)
    {
        logger.LogInformation("Starting bid calculation for basePrice: {BasePrice}", basePrice);

        var basicUserFee = await CalculateFeeAsync(FeeType.Basic, type, basePrice);
        var sellersSpecialFee = await CalculateFeeAsync(FeeType.Special, type, basePrice);
        var associationFee = await CalculateAssociationFeeAsync(FeeType.Association, basePrice);
        var storageFee = await CalculateFeeAsync(FeeType.Storage, type, basePrice);

        var totalPrice = RoundToTwoDecimals(basePrice + basicUserFee + sellersSpecialFee + associationFee + storageFee);

        var result = new BidCalculationResult
        {
            BasePrice = basePrice,
            VehicleType = type,
            BasicUserFee = basicUserFee,
            SellerSpecialFee = sellersSpecialFee,
            AssociationFee = associationFee,
            StorageFee = storageFee,
            TotalPrice = totalPrice
        };

        logger.LogInformation("Completed bid calculation for vehicle type {VehicleType}. TotalPrice: {TotalPrice}", totalPrice, type);
        return result;
    }

    protected async Task<decimal> CalculateFeeAsync(FeeType feeType, VehicleType vehicleType, decimal basePrice)
    {
        logger.LogDebug("Attempting to retrieve setting for FeeType: {FeeType} and VehicleType: {VehicleType}", feeType, vehicleType);

        var setting = await settingsService.GetSettingAsync(feeType, vehicleType) 
                      ?? throw new ResourceNotFoundException($"Setting not found for FeeType: {feeType} and VehicleType: {vehicleType}");
        
        logger.LogInformation("Retrieved setting for FeeType: {FeeType} and VehicleType: {VehicleType}", feeType, vehicleType);

        var fee = setting.RateType == RateType.Fixed 
            ? setting.Rate 
            : basePrice * (setting.Rate / 100);

        if (setting.ApplyMinMaxLimits)
        {
            fee = Math.Clamp(fee, setting.Minimum, setting.Maximum);
            logger.LogDebug("Applied min/max limits. Fee after clamping: {Fee}", fee);
        }
        
        fee = RoundToTwoDecimals(fee);

        logger.LogInformation("Calculated fee for FeeType: {FeeType}, VehicleType: {VehicleType}. Fee: {Fee}", feeType, vehicleType, fee);
        return fee;
    }

    protected async Task<decimal> CalculateAssociationFeeAsync(FeeType feeType, decimal basePrice)
    {
        logger.LogDebug("Attempting to retrieve settings for FeeType: {FeeType}", feeType);

        var settings = await settingsService.GetSettingsAsync(feeType);

        logger.LogInformation("Retrieved setting for FeeType: {FeeType}", feeType);

        var applicableSetting = settings.FirstOrDefault(s => basePrice >= s.From && (s.To == null || basePrice <= s.To)) 
                                ?? throw new ResourceNotFoundException($"No applicable FeeType: {feeType} fee setting found");

        var fee = RoundToTwoDecimals(applicableSetting.Rate);

        logger.LogInformation("Calculated Association Fee: {Fee} for BasePrice: {BasePrice}", fee, basePrice);
        return fee;
    }

    private static decimal RoundToTwoDecimals(decimal value) => Math.Round(value, 2, MidpointRounding.AwayFromZero);
}