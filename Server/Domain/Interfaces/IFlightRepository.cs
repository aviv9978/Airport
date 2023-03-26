
using Core.Entities;

namespace Core.Interfaces
{
    public interface IFlightRepository
    {
        Task AddFlightAsync(Flight flight);
    }
}
