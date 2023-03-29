using Core.Entities;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Airport.Infrastracture.Repositories
{
    public class ProcLogRepository : IProcLogRepository
    {
        private readonly AirportDataContext _dBContext;
        private readonly ILogger<ProcLogRepository> _logger;

        public ProcLogRepository(AirportDataContext dbContext, ILogger<ProcLogRepository> logger)
        {
            _dBContext = dbContext;
            _logger = logger;
        }
        public async Task AddProcLogAsync(ProcessLog log)
        {
            try
            {
                _dBContext.AddAsync(log);
                await _dBContext.SaveChangesAsync();
                _logger.LogInformation("Added log");
            }
            catch (Exception)
            {
                _logger.LogWarning("Exepction");
                throw;
            }
        }

        public async Task UpdateOutLog(int procLogId)
        {
            try
            {
                var reqProcLog = await _dBContext.ProcessLogger
                    .FirstOrDefaultAsync(log => log.Id == procLogId);
                reqProcLog.ExitTime = DateTime.Now;
                _dBContext.Update(reqProcLog);
                await _dBContext.SaveChangesAsync();
                _logger.LogInformation("updated log");
            }
            catch (Exception)
            {
                _logger.LogWarning("Exepction");
                throw;
            }
        }
    }
}
