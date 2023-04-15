using Airport.Application.Interfaces;
using AutoMapper;
using Core.DTOs.Outgoing;
using Core.Entities;
using Core.Interfaces.Repositories;

namespace Airport.Application
{
    public class ProcLogsService : IProcLogService
    {
        private readonly IProcLogRepository _procLogRepos;
        private readonly IMapper _mapper;

        public ProcLogsService(IProcLogRepository procLogRepos, IMapper mapper)
        {
            _procLogRepos = procLogRepos;
            _mapper = mapper;
        }
        public async Task<ICollection<ProcessLogOutDTO>> GetProcessLogsAsync()
        {
            ICollection<ProcessLogOutDTO> procLogsOutDTO = new List<ProcessLogOutDTO>();
            var allProcLogs = await _procLogRepos.GetAllProcLogsAsync();
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
