using Progi.BidCalculator.Core.Entities;

namespace Progi.BidCalculator.Core.Models;

public sealed record BidCalculationResult
{
    public decimal BasePrice { get; init; }
    public VehicleType VehicleType { get; init; }
    public decimal BasicUserFee { get; init; }
    public decimal SellerSpecialFee { get; init; }
    public decimal AssociationFee { get; init; }
    public decimal StorageFee { get; init; }
    public decimal TotalPrice { get; init; }
}