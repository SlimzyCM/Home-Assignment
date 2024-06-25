using Microsoft.Extensions.Logging;
using Moq;
using Progi.BidCalculator.Application.BidCalculators;
using Progi.BidCalculator.Application.Interfaces;
using Progi.BidCalculator.Core.Entities;

namespace Progi.BidCalculator.Tests.Unit;

[TestFixture]
public class CommonCalculatorTests
{
    private Mock<ICalculatorSettingsService> _mockSettingsService;
    private Mock<ILogger<CommonCalculator>> _mockLogger;
    private CommonCalculator _calculator;

    [SetUp]
    public void Setup()
    {
        _mockSettingsService = new Mock<ICalculatorSettingsService>();
        _mockLogger = new Mock<ILogger<CommonCalculator>>();
        _calculator = new CommonCalculator(_mockSettingsService.Object, _mockLogger.Object);

        SetupMockSettings();
    }

    private void SetupMockSettings()
    {
        // Basic User Fee
        _mockSettingsService.Setup(s => s.GetSettingAsync(FeeType.Basic, VehicleType.Common))
            .ReturnsAsync(new CalculatorSetting
            {
                RateType = RateType.Percentage,
                Rate = 10,
                ApplyMinMaxLimits = true,
                Minimum = 10,
                Maximum = 50
            });

        // Special Fee
        _mockSettingsService.Setup(s => s.GetSettingAsync(FeeType.Special, VehicleType.Common))
            .ReturnsAsync(new CalculatorSetting
            {
                RateType = RateType.Percentage,
                Rate = 2
            });

        // Association Fee
        _mockSettingsService.Setup(s => s.GetSettingsAsync(FeeType.Association))
            .ReturnsAsync(new List<CalculatorSetting>
            {
                new() { From = 1, To = 500, Rate = 5 },
                new() { From = 501, To = 1000, Rate = 10 },
                new() { From = 1001, To = 3000, Rate = 15 },
                new() { From = 3001, To = null, Rate = 20 }
            });

        // Storage Fee
        _mockSettingsService.Setup(s => s.GetSettingAsync(FeeType.Storage, VehicleType.Common))
            .ReturnsAsync(new CalculatorSetting { RateType = RateType.Fixed, Rate = 100 });
    }

    [TestCase(398.00, 39.80, 7.96, 5.00, 100.00, 550.76)]
    [TestCase(501.00, 50.00, 10.02, 10.00, 100.00, 671.02)]
    [TestCase(57.00, 10.00, 1.14, 5.00, 100.00, 173.14)]
    [TestCase(1100.00, 50.00, 22.00, 15.00, 100.00, 1287.00)]
    public async Task CalculateBid_WithVariousScenarios_ReturnsCorrectResults(
        decimal basePrice,
        decimal expectedBasicFee,
        decimal expectedSpecialFee,
        decimal expectedAssociationFee,
        decimal expectedStorageFee,
        decimal expectedTotal)
    {
        // Act
        var result = await _calculator.CalculateBidAsync(basePrice);

        Assert.Multiple(() =>
        {
            // Assert
            Assert.That(result.BasePrice, Is.EqualTo(basePrice));
            Assert.That(result.BasicUserFee, Is.EqualTo(expectedBasicFee));
            Assert.That(result.SellerSpecialFee, Is.EqualTo(expectedSpecialFee));
            Assert.That(result.AssociationFee, Is.EqualTo(expectedAssociationFee));
            Assert.That(result.StorageFee, Is.EqualTo(expectedStorageFee));
            Assert.That(result.TotalPrice, Is.EqualTo(expectedTotal));
            Assert.That(result.VehicleType, Is.EqualTo(VehicleType.Common));
        });
    }
}