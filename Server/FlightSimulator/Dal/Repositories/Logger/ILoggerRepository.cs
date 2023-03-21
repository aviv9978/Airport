using FlightSimulator.Models;

namespace FlightSimulator.Dal.Repositories.Flights
{
    public interface ILoggerRepository
    {
        Task AddLog(ProcessLog log);
        Task UpdateOutLog(int flightId);
    }
}
