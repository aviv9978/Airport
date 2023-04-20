using Core.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Core.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        Task CommitAsync();
        void Rollback();
        IGenericRepository<T> GenericRepository<T>() where T : class;
    }
}
