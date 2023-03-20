using FlightSimulator.Models;

namespace FlightSimulator.Dal.Repositories.Pilots
{
    public class PilotRepository : IPilotRepository
    {
        private readonly AirportDataContext _dBContext;
        private readonly ILogger<AirportDataContext> _logger;

        public PilotRepository(AirportDataContext dbContext, ILogger<AirportDataContext> logger)
        {
            _dBContext = dbContext;
            _logger = logger;
        }
        public async Task AddPilot(Pilot pilot)
        {
            try
            {
                await _dBContext.AddAsync(pilot);
                await _dBContext.SaveChangesAsync();
                _logger.LogWarning("Added pilot");
            }
            catch (Exception)
            {
                _logger.LogWarning("Exepction");
                throw;
            }
        }
    }
}
