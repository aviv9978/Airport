using Core.Interfaces;
using Core.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Airport.Infrastracture.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly AirportDataContext _dbContext;
        private readonly DbSet<T> _dbSet;
        public GenericRepository(AirportDataContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<T>();
        }
        public async Task<T> GetByIdAsync(int id) => await _dbSet.FindAsync(id);

        public async Task<IEnumerable<T>> GetAllAsync() => await _dbSet.ToListAsync();
        public async Task<IEnumerable<T>> FindListAsync(Expression<Func<T, bool>> expression) => await _dbSet.Where(expression).ToListAsync();

        public async Task AddAsync(T entity) => await _dbSet.AddAsync(entity);

        public void Remove(T entity) => _dbSet.Remove(entity);


        public void Update(T entity) => _dbSet.Update(entity);

    }
}
