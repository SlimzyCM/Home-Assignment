using Moq;
using Progi.BidCalculator.Application.Interfaces;
using Progi.BidCalculator.Application.Services;
using Progi.BidCalculator.Core.Entities;
using Progi.BidCalculator.Core.Exceptions;
using Progi.BidCalculator.Core.Interfaces;
using Progi.BidCalculator.Core.Models;

namespace Progi.BidCalculator.Tests.Unit.Services;

[TestFixture]
public class BidCalculationServiceTests
{
    private Mock<IBidCalculatorFactory> _mockBidCalculatorFactory;
    private Mock<IBidCalculator> _mockBidCalculator;
    private BidCalculationService _service;

    [SetUp]
    public void Setup()
    {
        _mockBidCalculatorFactory = new Mock<IBidCalculatorFactory>();
        _mockBidCalculator = new Mock<IBidCalculator>();

        _service = new BidCalculationService(_mockBidCalculatorFactory.Object);
    }

    [Test]
    public async Task CalculateBidAsync_WithValidInputs_ReturnsCalculateResultDto()
    {
        // Arrange
        const decimal price = 50000m;
        const VehicleType calculatorType = VehicleType.Common;
        var calculateResult = new BidCalculationResult { BasePrice = 1000000.00m, VehicleType = VehicleType.Luxury, TotalPrice = 1040320.00m };

        _mockBidCalculatorFactory.Setup(f => f.CreateBidCalculator(calculatorType)).Returns(_mockBidCalculator.Object);
        _mockBidCalculator.Setup(m => m.CalculateBidAsync(It.IsAny<decimal>())).ReturnsAsync(calculateResult);
        // Act
        var result = await _service.CalculateBidAsync(calculatorType, price);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.Multiple(() =>
        {
            Assert.That(result.TotalPrice, Is.EqualTo(calculateResult.TotalPrice));
            Assert.That(result.VehicleType, Is.EqualTo(calculateResult.VehicleType));
        });
    }

    [Test]
    public void CalculateBidAsync_WithInvalidPrice_ThrowsValidationException()
    {
        // Arrange
        const decimal invalidPrice = 0m;

        // Act & Assert
        Assert.ThrowsAsync<ValidationException>(async () => await _service.CalculateBidAsync(VehicleType.Common, invalidPrice));
    }
}