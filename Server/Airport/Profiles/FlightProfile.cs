using AutoMapper;
using Core.DTOs.Incoming;
using Core.Entities.Terminal;

namespace Airport.Profiles
{
    public class FlightProfile : Profile
    {
        public FlightProfile()
        {
            CreateMap<FlightInDTO, Flight>()
                .ForMember(dest => dest.Pilot,
                opt => opt.MapFrom(src => src.Pilot))
                .ForMember(dest => dest.Plain,
                opt => opt.MapFrom(src => src.Plane))
                .ForMember(dest => dest.Code,
                opt => opt.MapFrom(src => src.Code));
        }
    }
}
