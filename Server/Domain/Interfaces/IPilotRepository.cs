
using Core.Entities;

namespace Core.Interfaces
{
    public interface IPilotRepository
    {
        Task AddPilotAsync(Pilot pilot);

    }
}
