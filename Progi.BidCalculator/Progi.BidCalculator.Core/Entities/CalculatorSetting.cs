using System.ComponentModel.DataAnnotations;

namespace Progi.BidCalculator.Core.Entities;

public sealed class CalculatorSetting : Auditable
{
    [Required]
    public FeeType FeeType { get; set; }

    public VehicleType? VehicleType { get; set; }

    public bool ApplyMinMaxLimits { get; set; }

    public decimal Minimum { get; set; }

    public decimal Maximum { get; set; }

    [Required]
    public RateType RateType { get; set; }

    [Required]
    public decimal Rate { get; set; }

    public decimal From { get; set; }

    public decimal? To { get; set; }
}