
using Core.Entities;

namespace Core.Interfaces
{
    public interface IPilotRepository
    {
        Task AddPilot(Pilot pilot);

    }
}
