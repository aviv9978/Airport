using Core.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Core.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        Task CommitAsync();
        void Rollback();
        IFlightRepository Flight { get; }
        ILegRepostiroy Leg { get; }
        IPilotRepository Pilot { get; }
        IProcLogRepository ProcessLog { get; }
    }
}
