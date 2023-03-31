using Core.Entities;
using Core.Interfaces.Repositories;
using Microsoft.Extensions.Logging;

namespace Airport.Infrastracture.Repositories
{
    public class FlightRepository : IFlightRepository
    {
        private readonly AirportDataContext _dBContext;
        private readonly ILogger<FlightRepository> _logger;

        public FlightRepository(AirportDataContext dbContext, ILogger<FlightRepository> logger)
        {
            _dBContext = dbContext;
            _logger = logger;
        }
        public async Task AddFlightAsync(Flight flight)
        {
            try
            {
                await _dBContext.AddAsync(flight);
                await _dBContext.SaveChangesAsync();
                _logger.LogWarning("Added flight");
            }
            catch (Exception)
            {
                _logger.LogWarning("Exepction");
                throw;
            }
        }
    }
}
