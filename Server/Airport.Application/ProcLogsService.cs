using Airport.Application.Interfaces;
using AutoMapper;
using Core.DTOs.Outgoing;
using Core.Entities;
using Core.Interfaces;
using Core.Interfaces.Repositories;

namespace Airport.Application
{
    public class ProcLogsService : IProcLogService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProcLogsService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<List<ProcessLogOutDTO>> GetProcessLogsAsync()
        {
            var procLogsOutDTO = new List<ProcessLogOutDTO>();
            var allProcLogs = await _unitOfWork.ProcessLog.GetAllAsync();
            if (allProcLogs != null)
                foreach (var procLog in allProcLogs)
                {
                    var prcLogOut = _mapper.Map<ProcessLogOutDTO>(procLog);
                    procLogsOutDTO.Add(prcLogOut);
                }
            return procLogsOutDTO;
        }

    }
}
