using Core.Entities.Terminal;

namespace Core.Interfaces.Repositories
{
    public interface IFlightRepository : IGenericRepository<Flight>
    {
        Task AddFlightAsync(Flight flight);
        IEnumerable<Flight> GetFlights(string Gender);
    }
}
