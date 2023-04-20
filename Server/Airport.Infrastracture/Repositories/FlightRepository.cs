using Core.Entities.Terminal;
using Core.Interfaces;
using Core.Interfaces.Repositories;
using Microsoft.Extensions.Logging;

namespace Airport.Infrastracture.Repositories
{
    public class FlightRepository : GenericRepository<Flight>, IFlightRepository
    {
        private readonly AirportDataContext _dBContext;
        private readonly ILogger<FlightRepository> _logger;

        public FlightRepository(AirportDataContext dbContext)
            : base(dbContext) { }
        public async Task AddFlightAsync(Flight flight)
        {
            try
            {
                await _dBContext.AddAsync(flight);
                await _dBContext.SaveChangesAsync();
                _logger.LogWarning("Added flight");
            }
            catch (Exception e)
            {
                _logger.LogWarning($"Exepction {e.Message}");
                throw;
            }
        }

        public IEnumerable<Flight> GetFlights(string Gender)
        {
            return _dBContext.Flights.ToList();
        }
    }
}
