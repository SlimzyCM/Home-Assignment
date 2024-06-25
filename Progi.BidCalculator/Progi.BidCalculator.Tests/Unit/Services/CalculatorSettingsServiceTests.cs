using System.Linq.Expressions;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using MockQueryable.Moq;
using Moq;
using Progi.BidCalculator.Application.Services;
using Progi.BidCalculator.Core.Entities;
using Progi.BidCalculator.Core.Interfaces.DataAccess;
using Progi.BidCalculator.Infrastructure.Caching;
using Progi.BidCalculator.Tests.Unit.Helpers;

namespace Progi.BidCalculator.Tests.Unit.Services;

[TestFixture]
public class CalculatorSettingsServiceTests
{
    private Mock<ICachedRepository<CalculatorSetting>> _mockRepository;
    private ICacheManager _mockCacheManager;
    private IMemoryCache _mockMemoryCache;
    private CalculatorSettingsService _service;

    [SetUp]
    public void Setup()
    {
        _mockMemoryCache = new FakeMemoryCache();

        _mockRepository = new Mock<ICachedRepository<CalculatorSetting>>();
        var mockCacheOptions = new Mock<IOptions<CacheOptions>>();
        var cacheOptions = new CacheOptions
        {
            Size = 100,
            Expiration = TimeSpan.FromMinutes(30)
        };
        mockCacheOptions.Setup(m => m.Value).Returns(cacheOptions);

        _mockCacheManager = new CacheManager(_mockMemoryCache, mockCacheOptions.Object);
        _service = new CalculatorSettingsService(_mockRepository.Object, _mockCacheManager);
    }

    [TearDown]
    public void TearDown()
    {
        _mockMemoryCache.Dispose();
    }

    [Test]
    public async Task GetSettingsAsync_Cache_Is_Working_As_It_Should()
    {
        // Arrange
        var settings = new List<CalculatorSetting>
        {
            new() { From = 0, To = 500, Rate = 5 },
            new() { From = 501, To = 1000, Rate = 10 },
            new() { From = 1001, To = null, Rate = 15 }
        };

        const FeeType feeType = FeeType.Association;
        var cacheKey = $"CalculatorSettingsService_{feeType}";

        _mockRepository.Setup(x => x.GetByExpressionAsync(It.IsAny<Expression<Func<CalculatorSetting, bool>>>())).Returns(settings.AsQueryable().BuildMock());

        // Act
        var result = await _service.GetSettingsAsync(feeType);
        var cachedResult = await _service.GetSettingsAsync(feeType);

        var isCached = _mockMemoryCache.TryGetValue(cacheKey, out object? cachedValue);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(isCached);
            Assert.That(cachedValue, Is.EqualTo(settings));
            Assert.That(result, Is.Not.Null);
            Assert.That(cachedResult, Is.Not.Null);
            Assert.That(result, Is.EqualTo(settings));
            Assert.That(cachedResult, Is.EqualTo(settings));
        });

        _mockRepository.Verify(x => x.GetByExpressionAsync(It.IsAny<Expression<Func<CalculatorSetting, bool>>>()), Times.Once);
    }
}