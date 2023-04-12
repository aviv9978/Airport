using Airport.Application.ILogicServices;
using AutoMapper;
using Core.DTOs.Outgoing;
using Core.Interfaces.Repositories;

namespace Airport.Application.LogicServices
{
    public class LegStatusService : ILegStatusService
    {
        private readonly IMapper _mapper;

        public LegStatusService(IMapper mapper)
        {
            _mapper = mapper;
        }
        public async Task<ICollection<LegStatusOutDTO>> GetLegsStatusAsync()
        {
            ICollection<LegStatusOutDTO> legsStatus = new List<LegStatusOutDTO>();
            var legs = TerminalService.Legs;
            if (legs != null)
                foreach (var leg in legs)
                {
                    var legStatus = _mapper.Map<LegStatusOutDTO>(leg);
                    legsStatus.Add(legStatus);
                }
            return legsStatus;
        }
    }
}
