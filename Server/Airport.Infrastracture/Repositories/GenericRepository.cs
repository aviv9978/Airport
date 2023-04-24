using Core.Entities;
using Core.Interfaces;
using Core.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Airport.Infrastracture.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
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
        public async Task<IEnumerable<T>> FindListAsync(Expression<Func<T, bool>> expression)
        {
            List<T> entities;
            try
            {
                entities = await _dbSet.Where(expression).ToListAsync();
                return entities;
            }
            catch (Exception)
            {
                throw;
            }
            //foreach (var entity in entities)
            //{
            //    var entry = _dbContext.Entry(entity);
            //    entry.State = EntityState.Detached;
            //}
        }

        public async Task AddAsync(T entity) => await _dbSet.AddAsync(entity);

        public void Remove(T entity) => _dbSet.Remove(entity);


        public async Task UpdateAsync(T entity)
        {
            try
            {
                //_dbContext.ChangeTracker.Clear();
                var trackedEntity = await _dbSet.SingleOrDefaultAsync(e => e.Id == entity.Id);
                _dbContext.Entry<T>(trackedEntity).State = EntityState.Detached;
                _dbSet.Attach(entity);
                _dbContext.Entry<T>(entity).State = EntityState.Modified;
                _dbSet.Update(entity);
            }
            catch (Exception)
            {

                throw;
            }
            //_dbSet.Attach(entity);
        }

    }
}
