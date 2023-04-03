using Core.Entities.ForFlight;

namespace Core.Interfaces.Repositories
{
    public interface IPilotRepository
    {
        Task AddPilotAsync(Pilot pilot);
        Task<ICollection<Pilot>> GetAllPilotsAsync();
    }
}
