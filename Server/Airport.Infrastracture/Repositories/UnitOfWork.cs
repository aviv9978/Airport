using Castle.Core.Logging;
using Core.Entities;
using Core.Entities.ForFlight;
using Core.Interfaces;
using Core.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Threading;

namespace Airport.Infrastracture.Repositories
{
    public sealed class UnitOfWork : IUnitOfWork
    {
        private readonly ILogger<UnitOfWork> _logger;
        private readonly AirportDataContext _dbContext;
        private readonly IServiceProvider _services;
        public IFlightRepository Flight { get; private set; }
        public ILegRepostiroy Leg { get; private set; }
        public IProcLogRepository ProcessLog { get; private set; }
        public IPilotRepository Pilot { get; private set; }

        public UnitOfWork(AirportDataContext dbContext,
            ILogger<UnitOfWork> logger, IServiceProvider services,
            IFlightRepository flightRepos, ILegRepostiroy legRepos,
            IProcLogRepository procLogRepos, IPilotRepository pilotRepos)
        {
            _dbContext = dbContext;
            _logger = logger;
            Flight = flightRepos;
            Leg = legRepos;
            ProcessLog = procLogRepos;
            Pilot = pilotRepos;
            _services = services;
        }
        public AirportDataContext DatabaseContext() => _dbContext;

        public async Task CommitAsync()
        {
            try
            {
                    _logger.LogInformation("Added pilot");
                    await _dbContext.SaveChangesAsync();
                
            }
            catch (Exception e)
            {
                _logger.LogWarning($"Exepction in UnitOfWork, SaveChanges FAILED: {e.Message}");
                throw;
            }
        }
        public void Rollback()
        {
            foreach (var entry in _dbContext.ChangeTracker.Entries())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.State = EntityState.Detached;
                        break;
                }
            }
        }
        //public IGenericRepository<T> GenericRepository<T>() where T : BaseEntity
        //{
        //    return new GenericRepository<T>(_dbContext);
        //}
        //public void Dispose()
        //{
        //    Dispose(true);
        //    GC.SuppressFinalize(this);
        //}
        //private void Dispose(bool disposing)
        //{
        //    if (!_disposed)
        //    {
        //        if (disposing)
        //        {
        //            _dbContext.Dispose();
        //        }
        //    }
        //    _disposed = true;
        //}

    }
}

