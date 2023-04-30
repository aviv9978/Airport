using Core.Entities.Terminal;
using Core.Interfaces;
using Core.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Airport.Infrastracture.Repositories
{
    public class FlightRepository : GenericRepository<Flight>, IFlightRepository
    {
        private readonly AirportDataContext _dbContext;
        private readonly ILogger<FlightRepository> _logger;

        public FlightRepository(AirportDataContext dbContext, IServiceProvider services)
            : base(dbContext, services) 
        { 
            _dbContext = dbContext;
        }

        public IEnumerable<Flight> GetFlights(string Gender)
        {
            return _dbContext.Flights.ToList();
        }
    }
}
