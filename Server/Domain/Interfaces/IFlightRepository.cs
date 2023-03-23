
using Core.Entities;

namespace Core.Interfaces
{
    public interface IFlightRepository
    {
        Task AddFlight(Flight flight);
    }
}
