using Core.DTOs.Outgoing;
using Core.Entities.Terminal;
using AutoMapper;
using Core.Entities;

namespace Core.Profiles
{
    public class LegProfile : Profile
    {
        public LegProfile()
        {
            CreateMap<Leg, LegStatusOutDTO>()
               .ForMember(dest => dest.IsOccupied,
               opt => opt.MapFrom(src => src.IsOccupied))
               .ForMember(dest => dest.LegNumber,
               opt => opt.MapFrom(src => (int)src.CurrentLeg))
               .ForMember(dest => dest.Flight,
               opt => opt.MapFrom(src => src.Flight));

            CreateMap<ProcessLog, LegStatusOutDTO>()
             .ForMember(dest => dest.Flight,
             opt => opt.MapFrom(src => src.Flight))
             .ForMember(dest => dest.LegNumber,
             opt => opt.MapFrom(src => (int)src.LegNum));
        }
    }
}
