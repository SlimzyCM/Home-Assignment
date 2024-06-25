using AutoMapper;
using Progi.BidCalculator.API.Models.Response;
using Progi.BidCalculator.Core.Models;

namespace Progi.BidCalculator.API.Models.Mappings;

public sealed class ProfileMappings : Profile
{
    public ProfileMappings()
    {
        CreateMap<BidCalculationResult, CalculateResponse>().ReverseMap()
            .ForMember(dest => dest.VehicleType, opt => opt.MapFrom(src => src.VehicleType.ToString()));
    }
}