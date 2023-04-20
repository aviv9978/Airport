﻿using Castle.Core.Logging;
using Core.Entities.ForFlight;
using Core.Interfaces;
using Core.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Airport.Infrastracture.Repositories
{
    public sealed class UnitOfWork : IUnitOfWork
    {
        private readonly ILogger<UnitOfWork> _logger;
        private readonly AirportDataContext _dbContext;
        private bool _disposed = false;
        public UnitOfWork(AirportDataContext dbContext, ILogger<UnitOfWork> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }
        public AirportDataContext DatabaseContext() => _dbContext;

        public async Task CommitAsync()
        {
            try
            {
                int res = await _dbContext.SaveChangesAsync().ConfigureAwait(false);
                _logger.LogInformation("Added pilot");
            }
            catch (Exception e)
            {
                _logger.LogWarning("Exepction in UnitOfWork, SaveChanges FAILED", e.Message);
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
        public IGenericRepository<T> GenericRepository<T>() where T : class
        {
            return new GenericRepository<T>(_dbContext);
        }
        private void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _dbContext.Dispose();
                }
            }
            _disposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

    }
}

