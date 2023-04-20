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
        public IEnumerable<Flight> GetFlights(string Gender)
        {
            return _dBContext.Flights.ToList();
        }
    }
}
