using FlightSimulator.Models;

namespace FlightSimulator.Dal.Repositories
{
    public interface IFlightRepository
    {
        Task AddFlight(Flight flight);
    }
}
