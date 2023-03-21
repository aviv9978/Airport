using FlightSimulator.Dal.Repositories.Flights;
using FlightSimulator.Models;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace FlightSimulator.Dal.Repositories.Logger
{
    public class LoggerRepository : ILoggerRepository
    {
        private readonly AirportDataContext _dBContext;
        private readonly ILogger<AirportDataContext> _logger;

        public LoggerRepository(AirportDataContext dbContext, ILogger<AirportDataContext> logger)
        {
            _dBContext = dbContext;
            _logger = logger;
        }
        public async Task AddLog(ProcessLog log)
        {
            try
            {
                 _dBContext.Add(log);
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
                var reqProcLog = await _dBContext.ProcessLogger.OrderByDescending(e => e.Id).FirstOrDefaultAsync((log) => log.Flight.Id == flightId);
                reqProcLog.Out = DateTime.Now;
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
