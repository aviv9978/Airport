using Airport.Application.ILogicServices;
using AutoMapper;
using Core.DTOs.Outgoing;
using Core.Interfaces;
using Core.Interfaces.Repositories;

namespace Airport.Application.LogicServices
{
    public class LegStatusService : ILegStatusService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public LegStatusService(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }
        public async Task<IEnumerable<LegStatusOutDTO>> GetLegsStatusAsync()
        {
            try
            {

            var legsStatus = new List<LegStatusOutDTO>();
            var legs = await _unitOfWork.Leg.GetAllAsync();
            if (legs != null)
                foreach (var leg in legs)
                {
                    var legStatus = _mapper.Map<LegStatusOutDTO>(leg);
                    legsStatus.Add(legStatus);
                }
            return legsStatus;
            }
            catch (Exception e)
            {

                throw;
            }
        }
    }
}
