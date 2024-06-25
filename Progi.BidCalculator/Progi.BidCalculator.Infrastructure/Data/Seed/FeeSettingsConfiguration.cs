using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Progi.BidCalculator.Core.Entities;

namespace Progi.BidCalculator.Infrastructure.Data.Seed;

public class FeeSettingsConfiguration : IEntityTypeConfiguration<CalculatorSetting>
{
    public void Configure(EntityTypeBuilder<CalculatorSetting> builder)
    {
        builder.HasData(
            // Basic User Fees
            new CalculatorSetting
            {
                Id = 1,
                FeeType = FeeType.Basic,
                VehicleType = VehicleType.Common,
                Minimum = 10,
                Maximum = 50,
                ApplyMinMaxLimits = true,
                RateType = RateType.Percentage,
                Rate = 10,
                CreationDate = DateTime.UtcNow
            },
            new CalculatorSetting
            {
                Id = 2,
                FeeType = FeeType.Basic,
                VehicleType = VehicleType.Luxury,
                Minimum = 25,
                Maximum = 200,
                ApplyMinMaxLimits = true,
                RateType = RateType.Percentage,
                Rate = 10,
                From = 0,
                CreationDate = DateTime.UtcNow
            },

            // Seller's Special Fees
            new CalculatorSetting
            {
                Id = 3,
                FeeType = FeeType.Special,
                VehicleType = VehicleType.Common,
                RateType = RateType.Percentage,
                Rate = 2,
                CreationDate = DateTime.UtcNow
            },
            new CalculatorSetting
            {
                Id = 4,
                FeeType = FeeType.Special,
                VehicleType = VehicleType.Luxury,
                RateType = RateType.Percentage,
                Rate = 4,
                CreationDate = DateTime.UtcNow
            },

            // Association Added Costs
            new CalculatorSetting
            {
                Id = 5,
                FeeType = FeeType.Association,
                RateType = RateType.Fixed,
                Rate = 5,
                From = 1,
                To = 500,
                CreationDate = DateTime.UtcNow
            },
            new CalculatorSetting
            {
                Id = 6,
                FeeType = FeeType.Association,
                RateType = RateType.Fixed,
                Rate = 10,
                From = 501,
                To = 1000,
                CreationDate = DateTime.UtcNow
            },
            new CalculatorSetting
            {
                Id = 7,
                FeeType = FeeType.Association,
                RateType = RateType.Fixed,
                Rate = 15,
                From = 1001,
                To = 3000,
                CreationDate = DateTime.UtcNow
            },
            new CalculatorSetting
            {
                Id = 8,
                FeeType = FeeType.Association,
                RateType = RateType.Fixed,
                Rate = 20,
                From = 3001,
                To = null,  
                CreationDate = DateTime.UtcNow
            },

            // Storage Fee
            new CalculatorSetting
            {
                Id = 9,
                VehicleType = VehicleType.Common,
                FeeType = FeeType.Storage,
                RateType = RateType.Fixed,
                Rate = 100,
                From = 0,
                CreationDate = DateTime.UtcNow
            },
            
            new CalculatorSetting
            {
                Id = 10,
                VehicleType = VehicleType.Luxury,
                FeeType = FeeType.Storage,
                RateType = RateType.Fixed,
                Rate = 100,
                From = 0,
                CreationDate = DateTime.UtcNow
            }
        );
    }

}