using Castle.Components.DictionaryAdapter.Xml;
using Castle.Core.Resource;
using Core.Entities;
using Core.Interfaces;
using Core.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
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

        public async Task<IEnumerable<T>> GetAllAsync() => await _dbSet.AsNoTracking().ToListAsync();
        public async Task<IEnumerable<T>> FindListAsync(Expression<Func<T, bool>> expression)
        {
            List<T> entities;
            try
            {
                entities = await _dbSet.Where(expression).AsNoTracking().ToListAsync();
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


        public void Update(T entity)
        {
            try
            {
                //_dbSet.Attach(entity);
                //var entry = _dbContext.Entry(entity);

                //entry.State = EntityState.Modified;
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
