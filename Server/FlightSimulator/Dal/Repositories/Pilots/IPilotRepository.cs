using FlightSimulator.Models;

namespace FlightSimulator.Dal.Repositories.Pilots
{
    public interface IPilotRepository
    {
        Task AddPilot(Pilot pilot);

    }
}
