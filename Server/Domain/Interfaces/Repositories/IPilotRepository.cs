using Core.Entities;

namespace Core.Interfaces.Repositories
{
    public interface IPilotRepository
    {
        Task AddPilotAsync(Pilot pilot);

    }
}
