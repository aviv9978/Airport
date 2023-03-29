
using Core.Entities;

namespace Core.Interfaces
{
    public interface IProcLogRepository
    {
        Task AddProcLogAsync(ProcessLog log);
        Task UpdateOutLogAsync(int procLogId);
    }
}
