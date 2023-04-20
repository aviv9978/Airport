using Core.Entities.Terminal;

namespace Core.Interfaces.Repositories
{
    public interface IFlightRepository : IGenericRepository<Flight>
    {
        IEnumerable<Flight> GetFlights(string Gender);
    }
}
