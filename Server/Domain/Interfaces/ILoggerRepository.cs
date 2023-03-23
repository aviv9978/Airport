
using Core.Entities;

namespace Core.Interfaces
{
    public interface ILoggerRepository
    {
        Task AddLog(ProcessLog log);
        Task UpdateOutLog(int flightId);
    }
}
