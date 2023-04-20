using Core.Entities.ForFlight;
using Core.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Airport.Infrastracture.Repositories
{
    public class PilotRepository : GenericRepository<Pilot>, IPilotRepository
    {
        private readonly AirportDataContext _dBContext;
        //private readonly ILogger<PilotRepository> _logger;

        public PilotRepository(AirportDataContext dbContext)
            :base(dbContext)
        {
            _dBContext = dbContext;
            //_logger = logger;
        }
        public async Task AddPilotAsync(Pilot pilot)
        {
            try
            {
                await _dBContext.AddAsync(pilot);
                await _dBContext.SaveChangesAsync();
              //  _logger.LogWarning("Added pilot");
            }
            catch (Exception)
            {
                //_logger.LogWarning("Exepction");
                throw;
            }
        }

        public async Task<ICollection<Pilot>> GetAllPilotsAsync()
        {
            var pilots = await _dBContext.Pilots.ToListAsync();
            return pilots;
        }
    }
}
