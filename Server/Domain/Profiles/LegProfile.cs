using Core.DTOs.Outgoing;
using Core.Entities.Terminal;
using AutoMapper;
using Core.Entities;
using Newtonsoft.Json.Linq;
using System;
using Core.Enums;
using EnumsNET;

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
            opt => opt.MapFrom(src =>((LegNumber)src.CurrentLeg).AsString(EnumFormat.Description)))
               .ForMember(dest => dest.Flight,
               opt => opt.MapFrom(src => src.Flight));

            CreateMap<ProcessLog, LegStatusOutDTO>()
             .ForMember(dest => dest.Flight,
             opt => opt.MapFrom(src => src.Flight))
             .ForMember(dest => dest.LegNumber,
             opt => opt.MapFrom(src => src.LegNumber));
        }
    }
}
