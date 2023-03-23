using Core.Entities;
using Core.Interfaces;
using Microsoft.Extensions.Logging;

namespace Airport.Infrastracture.Repositories
{
    public class PilotRepository : IPilotRepository
    {
        private readonly AirportDataContext _dBContext;
        private readonly ILogger<PilotRepository> _logger;

        public PilotRepository(AirportDataContext dbContext, ILogger<PilotRepository> logger)
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
