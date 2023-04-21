using Core.DTOs.Outgoing;
using Core.Entities;
using Core.Entities.Terminal;

namespace Core.Interfaces.Repositories
{
    public interface IProcLogRepository : IGenericRepository<ProcessLog>
    {
        Task UpdateOutLogAsync(int procLogId, DateTime exitTime);
    }
}
