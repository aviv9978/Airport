using Castle.Components.DictionaryAdapter.Xml;
using Castle.Core.Resource;
using Core.Entities;
using Core.Interfaces;
using Core.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using Microsoft.Extensions.DependencyInjection;
using System.Linq.Expressions;

namespace Airport.Infrastracture.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private readonly AirportDataContext _dbContext;
        private readonly DbSet<T> _dbSet;
        private readonly IServiceProvider _services;
        public GenericRepository(AirportDataContext dbContext, IServiceProvider services)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<T>();
            _services = services;
        }
        public async Task<T> GetByIdAsync(int id)
        {

            return await _dbSet.FindAsync(id);

        }

        public async Task<IEnumerable<T>> GetAllAsync() => await _dbSet.AsNoTracking().ToListAsync();
        public async Task<IEnumerable<T>> FindListAsync(Expression<Func<T, bool>> expression)
        {
            try
            {
                return await _dbContext.Set<T>().Where(expression).AsNoTracking().ToListAsync();
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

        public async Task AddAsync(T entity)
        {


            await _dbContext.Set<T>().AddAsync(entity);

        }

        public void Remove(T entity) => _dbContext.Set<T>().Remove(entity);


        public void Update(T entity)
        {
            try
            {
                //_dbSet.Attach(entity);
                //var entry = _dbContext.Entry(entity);  
                _dbContext.Set<T>().Update(entity);

                //entry.State = EntityState.Modified;
            }
            catch (Exception)
            {

                throw;
            }
            //_dbSet.Attach(entity);
        }

    }
}
