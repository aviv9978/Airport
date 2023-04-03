using Core.Entities.Terminal;

namespace Core.Interfaces.Repositories
{
    public interface IFlightRepository
    {
        Task AddFlightAsync(Flight flight);
    }
}
