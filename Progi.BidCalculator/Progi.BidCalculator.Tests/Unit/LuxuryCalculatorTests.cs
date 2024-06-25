using Microsoft.Extensions.Logging;
using Moq;
using Progi.BidCalculator.Application.BidCalculators;
using Progi.BidCalculator.Application.Interfaces;
using Progi.BidCalculator.Core.Entities;

namespace Progi.BidCalculator.Tests.Unit;

[TestFixture]
public class LuxuryCalculatorTests
{
    private Mock<ICalculatorSettingsService> _mockSettingsService;
    private Mock<ILogger<LuxuryCalculator>> _mockLogger;
    private LuxuryCalculator _calculator;

    [SetUp]
    public void Setup()
    {
        _mockSettingsService = new Mock<ICalculatorSettingsService>();
        _mockLogger = new Mock<ILogger<LuxuryCalculator>>();
        _calculator = new LuxuryCalculator(_mockSettingsService.Object, _mockLogger.Object);

        SetupMockSettings();
    }

    private void SetupMockSettings()
    {
        // Basic User Fee
        _mockSettingsService.Setup(s => s.GetSettingAsync(FeeType.Basic, VehicleType.Luxury))
            .ReturnsAsync(new CalculatorSetting
            {
                RateType = RateType.Percentage,
                Rate = 10,
                ApplyMinMaxLimits = true,
                Minimum = 25,
                Maximum = 200
            });

        // Special Fee
        _mockSettingsService.Setup(s => s.GetSettingAsync(FeeType.Special, VehicleType.Luxury))
            .ReturnsAsync(new CalculatorSetting
            {
                RateType = RateType.Percentage,
                Rate = 4
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
        _mockSettingsService.Setup(s => s.GetSettingAsync(FeeType.Storage, VehicleType.Luxury))
            .ReturnsAsync(new CalculatorSetting { RateType = RateType.Fixed, Rate = 100 });
    }

    [TestCase(1800.00, 180.00, 72.00, 15.00, 100.00, 2167.00)]
    [TestCase(1000000.00, 200.00, 40000.00, 20.00, 100.00, 1040320.00)]
    [TestCase(250.00, 25.00, 10.00, 5.00, 100.00, 390.00)]
    [TestCase(2500.00, 200.00, 100.00, 15.00, 100.00, 2915.00)]
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
            Assert.That(result.VehicleType, Is.EqualTo(VehicleType.Luxury));
        });
    }


    [Test]
    public async Task CalculateBid_WithVeryHighBasePrice_AppliesMaximumBasicFee()
    {
        // Arrange
        const decimal veryHighBasePrice = 10000000;

        // Act
        var result = await _calculator.CalculateBidAsync(veryHighBasePrice);

        Assert.Multiple(() =>
        {
            // Assert
            Assert.That(result.BasicUserFee, Is.EqualTo(200));
            Assert.That(result.SellerSpecialFee, Is.EqualTo(400000));
            Assert.That(result.AssociationFee, Is.EqualTo(20));
            Assert.That(result.StorageFee, Is.EqualTo(100));
            Assert.That(result.TotalPrice, Is.EqualTo(10400320));
        });
    }
}