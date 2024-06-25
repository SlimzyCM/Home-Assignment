using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Progi.BidCalculator.Core.Entities;

namespace Progi.BidCalculator.API.Models.Request;

public sealed class CalculateRequest
{
    [JsonPropertyName("vehicleType")]
    [Required]
    public required VehicleType VehicleType { get; set; }

    [JsonPropertyName("price")]
    [Required]
    public decimal Price { get; set; }
}