using AutoMapper;
using Core.DTOs.Incoming;
using Core.DTOs.Outcoming;
using Core.Entities.ForFlight;

namespace Airport.Profiles
{
    public class PilotProfile : Profile
    {
        public PilotProfile()
        {
            CreateMap<PilotInDTO, Pilot>()
                .ForMember(dest => dest.FirstName,
                opt => opt.MapFrom(src => src.FirstName))
                .ForMember(dest => dest.LastName,
                opt => opt.MapFrom(src => src.LastName))
                .ForMember(dest => dest.Rank,
                opt => opt.MapFrom(src => src.Rank))
                .ForMember(dest => dest.Age,
                opt => opt.MapFrom(src => src.Age));

            CreateMap<Pilot, PilotOutDTO>()
                .ForMember(dest => dest.FullName,
                opt => opt.MapFrom(src => $" {src.FirstName} {src.LastName}"))
                .ForMember(dest => dest.Age,
                opt => opt.MapFrom(src => src.Age))
                .ForMember(dest => dest.Rank,
                opt => opt.MapFrom(src => src.Rank));
        }
    }
}
