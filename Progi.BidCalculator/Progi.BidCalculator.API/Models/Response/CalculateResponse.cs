using System.Text.Json.Serialization;

namespace Progi.BidCalculator.API.Models.Response;

public sealed record CalculateResponse
{
    [JsonPropertyName("basePrice")]
    public decimal BasePrice { get; set; }


    [JsonPropertyName("vehicleType")]
    public string VehicleType { get; set; } = string.Empty;


    [JsonPropertyName("basicUserFee")]
    public decimal BasicUserFee { get; set; }


    [JsonPropertyName("sellerSpecialFee")]
    public decimal SellerSpecialFee { get; set; }


    [JsonPropertyName("associationFee")]
    public decimal AssociationFee { get; set; }


    [JsonPropertyName("storageFee")]
    public decimal StorageFee { get; set; }


    [JsonPropertyName("totalPrice")]
    public decimal TotalPrice { get; set; }
}