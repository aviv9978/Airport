using Airport.Application.ILogicServices;
using AutoMapper;
using Core.DTOs.Outgoing;
using Core.Entities.Terminal;
using Core.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airport.Application.LogicServices
{
    public class LegStatusService : ILegStatusService
    {
        private readonly ILegRepostiroy _legRepos;
        private readonly IMapper _mapper;

        public LegStatusService(ILegRepostiroy legRepos, IMapper mapper)
        {
            _legRepos = legRepos;
            _mapper = mapper;
        }
        public async Task<ICollection<LegStatusOutDTO>> GetLegsStatusAsync()
        {
            ICollection<LegStatusOutDTO> legsStatus = new List<LegStatusOutDTO>();
            var legs = await _legRepos.GetLegsAsync();
            foreach (var leg in legs)
            {
                var legStatus = _mapper.Map<LegStatusOutDTO>(leg);
                legsStatus.Add(legStatus);
            }
            return legsStatus;
        }
    }
}
