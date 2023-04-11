using Core.DTOs.Outgoing;
using Core.Entities.Terminal;
using AutoMapper;

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
               opt => opt.MapFrom(src => src.CurrentLeg));
        }
    }
}
