using FlightSimulator.Models;


namespace FlightSimulator.Dal.Repositories
{
    public class FlightRepository : IFlightRepository
    {
        private readonly AirportDataContext _dBContext;
        private readonly ILogger<AirportDataContext> _logger;

        public FlightRepository(AirportDataContext dbContext, ILogger<AirportDataContext> logger)
        {
            _dBContext = dbContext;
            _logger = logger;
        }
        public async Task AddFlight(Flight flight)
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
