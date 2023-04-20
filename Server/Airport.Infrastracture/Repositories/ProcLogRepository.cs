using Core.DTOs.Outgoing;
using Core.Entities;
using Core.Entities.ForFlight;
using Core.Entities.Terminal;
using Core.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Airport.Infrastracture.Repositories
{
    public class ProcLogRepository : GenericRepository<ProcessLog>, IProcLogRepository
    {
        private readonly AirportDataContext _dBContext;
       // private readonly ILogger<ProcLogRepository> _logger;

        public ProcLogRepository(AirportDataContext dbContext)
            :base(dbContext) 
        {
            _dBContext = dbContext;
           // _logger = logger;
        }
        public async Task UpdateOutLogAsync(int procLogId, DateTime exitTime)
        {
            try
            {
                var reqProcLog = await _dBContext.ProcessLogger
                    .FirstOrDefaultAsync(log => log.Id == procLogId);
                reqProcLog.ExitTime = exitTime;
                _dBContext.Update(reqProcLog);
               // _logger.LogInformation("updated log");
            }
            catch (Exception e)
            {
              //  _logger.LogWarning("Exepction", e.Message);
                throw;
            }
        }

    }
}
