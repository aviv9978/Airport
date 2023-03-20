using FlightSimulator.Models;

namespace FlightSimulator.Dal.Repositories.Flights
{
    public interface IFlightRepository
    {
        Task AddFlight(Flight flight);
    }
}
