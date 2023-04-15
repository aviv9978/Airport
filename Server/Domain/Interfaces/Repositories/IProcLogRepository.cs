using Core.DTOs.Outgoing;
using Core.Entities;

namespace Core.Interfaces.Repositories
{
    public interface IProcLogRepository
    {
        Task AddProcLogAsync(ProcessLog log);
        Task UpdateOutLogAsync(int procLogId, DateTime exitTime);
        Task<List<ProcessLog>> GetAllProcLogsAsync();
    }
}
