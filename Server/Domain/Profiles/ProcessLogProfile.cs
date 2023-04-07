using AutoMapper;
using Core.DTOs.Outgoing;
using Core.Entities;

namespace Core.Profiles
{
    public class ProcessLogProfile : Profile
    {
        public ProcessLogProfile()
        {
            CreateMap<ProcessLog, ProcessLogOutDTO>()
                .ForMember(dest => dest.Flight,
                    opt => opt.MapFrom(src => src.Flight))
                 .ForMember(dest => dest.Id,
                    opt => opt.MapFrom(src => src.Id))
                 .ForMember(dest => dest.Message,
                    opt => opt.MapFrom(src => src.Message))
                    .ForMember(dest => dest.EnterTime,
                    opt => opt.MapFrom(src => src.EnterTime))
                    .ForMember(dest => dest.ExitTime,
                    opt => opt.MapFrom(src => src.ExitTime))
                    .ForMember(dest => dest.LegNum,
                    opt => opt.MapFrom(src => src.LegNum));

        }
    }
}
