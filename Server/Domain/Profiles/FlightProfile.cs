using AutoMapper;
using Core.DTOs.Incoming;
using Core.DTOs.Outgoing;
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
                opt => opt.MapFrom(src => src.Plain))
                .ForMember(dest => dest.Code,
                opt => opt.MapFrom(src => src.Code));

            CreateMap<Flight, FlightOutDTO>()
               .ForMember(dest => dest.Pilot,
               opt => opt.MapFrom(src => src.Pilot))
               .ForMember(dest => dest.Plain,
               opt => opt.MapFrom(src => src.Plain))
               .ForMember(dest => dest.Code,
               opt => opt.MapFrom(src => src.Code))
               .ForMember(dest => dest.IsDeparture,
               opt => opt.MapFrom(src => src.IsDeparture)); ;
        }
    }
}
