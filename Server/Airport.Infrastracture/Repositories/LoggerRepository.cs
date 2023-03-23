using Core.Entities;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Airport.Infrastracture.Repositories
{
    public class LoggerRepository : ILoggerRepository
    {
        private readonly AirportDataContext _dBContext;
        private readonly ILogger<LoggerRepository> _logger;

        public LoggerRepository(AirportDataContext dbContext, ILogger<LoggerRepository> logger)
        {
            _dBContext = dbContext;
            _logger = logger;
        }
        public async Task AddLog(ProcessLog log)
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

        public async Task UpdateOutLog(int flightId)
        {
            try
            {
                var reqProcLog = await _dBContext.ProcessLogger.OrderByDescending(log => log.Id)
                    .FirstOrDefaultAsync(log => log.Flight.Id == flightId);
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
