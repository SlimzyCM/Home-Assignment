using Microsoft.Extensions.Logging;
using Progi.BidCalculator.Application.Interfaces;
using Progi.BidCalculator.Core.Entities;

namespace Progi.BidCalculator.Application.BidCalculators;

public sealed class LuxuryCalculator(ICalculatorSettingsService settingsService, ILogger<LuxuryCalculator> logger)
    : BaseCalculator(settingsService, logger, VehicleType.Luxury);