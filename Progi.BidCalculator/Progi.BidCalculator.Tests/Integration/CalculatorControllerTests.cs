using System.Net;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Testing;
using Progi.BidCalculator.API.Models.Request;
using Progi.BidCalculator.API.Models.Response;
using Progi.BidCalculator.Core.Entities;

namespace Progi.BidCalculator.Tests.Integration;

[TestFixture]
public class CalculatorControllerTests
{
    private WebApplicationFactory<Program> _factory;
    private HttpClient _client;

    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        _factory = TestSetup.GetWebApplicationFactory();
        _client = _factory.CreateClient();
    }

    [OneTimeTearDown]
    public void OneTimeTearDown()
    {
        _client.Dispose();
        _factory.Dispose();
    }

    [TestCase(398.00, VehicleType.Common, 39.80, 7.96, 5.00, 100.00, 550.76)]
    [TestCase(501.00, VehicleType.Common, 50.00, 10.02, 10.00, 100.00, 671.02)]
    [TestCase(57.00, VehicleType.Common, 10.00, 1.14, 5.00, 100.00, 173.14)]
    [TestCase(1800.00, VehicleType.Luxury, 180.00, 72.00, 15.00, 100.00, 2167.00)]
    [TestCase(1100.00, VehicleType.Common, 50.00, 22.00, 15.00, 100.00, 1287.00)]
    [TestCase(1000000.00, VehicleType.Luxury, 200.00, 40000.00, 20.00, 100.00, 1040320.00)]
    public async Task CalculateBid_WithVariousScenarios_ReturnsCorrectResults(
        decimal basePrice,
        VehicleType vehicleType,
        decimal expectedBasicFee,
        decimal expectedSpecialFee,
        decimal expectedAssociationFee,
        decimal expectedStorageFee,
        decimal expectedTotal)
    {
        // Arrange
        var request = new CalculateRequest { Price = basePrice, VehicleType = vehicleType };

        // Act
        var response = await _client.PostAsJsonAsync("/api/v1/calculator/calculateBid", request);

        // Assert
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        var result = await response.Content.ReadFromJsonAsync<CalculateResponse>();

        Assert.That(result, Is.Not.Null);
        Assert.Multiple(() =>
        {
            Assert.That(result.BasePrice, Is.EqualTo(basePrice));
            Assert.That(result.VehicleType, Is.EqualTo(vehicleType.ToString()));
            Assert.That(result.BasicUserFee, Is.EqualTo(expectedBasicFee));
            Assert.That(result.SellerSpecialFee, Is.EqualTo(expectedSpecialFee));
            Assert.That(result.AssociationFee, Is.EqualTo(expectedAssociationFee));
            Assert.That(result.StorageFee, Is.EqualTo(expectedStorageFee));
            Assert.That(result.TotalPrice, Is.EqualTo(expectedTotal));
        });
    }

    [Test]
    public async Task CalculateBid_WithNegativeBasePrice_ReturnsBadRequest()
    {
        // Arrange
        var request = new CalculateRequest { Price = -100, VehicleType = VehicleType.Common };

        // Act
        var response = await _client.PostAsJsonAsync("/api/v1/calculator/calculateBid", request);
        var result = await response.Content.ReadFromJsonAsync<ProblemDetails>();

        Assert.Multiple(() =>
        {
            // Assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.UnprocessableEntity));
            Assert.That(result, Is.Not.Null);
        });
        Assert.That(result?.Detail, Is.EqualTo("base price must be greater than zero"));
    }

    [Test]
    public async Task CalculateBid_WithInvalidVehicleType_ReturnsBadRequest()
    {
        // Arrange
        var request = new CalculateRequest { Price = 1000, VehicleType = (VehicleType)999 };

        // Act
        var response = await _client.PostAsJsonAsync("/api/v1/calculator/calculateBid", request);
        var result = await response.Content.ReadFromJsonAsync<ProblemDetails>();

        // Assert

        Assert.Multiple(() =>
        {
            // Assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
            Assert.That(result, Is.Not.Null);
        });
        Assert.That(result?.Detail, Is.EqualTo("Unsupported vehicle type"));
    }

}