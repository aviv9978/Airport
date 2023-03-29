
using Core.Entities;

namespace Core.Interfaces
{
    public interface IProcLogRepository
    {
        Task AddProcLogAsync(ProcessLog log);
        Task UpdateOutLog(int procLogId);
    }
}
