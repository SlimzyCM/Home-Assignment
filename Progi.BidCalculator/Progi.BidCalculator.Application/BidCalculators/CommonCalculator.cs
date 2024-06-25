using Microsoft.Extensions.Logging;
using Progi.BidCalculator.Application.Interfaces;
using Progi.BidCalculator.Core.Entities;

namespace Progi.BidCalculator.Application.BidCalculators;

public class CommonCalculator(ICalculatorSettingsService settingsService, ILogger<CommonCalculator> logger)
    : BaseCalculator(settingsService, logger, VehicleType.Common);