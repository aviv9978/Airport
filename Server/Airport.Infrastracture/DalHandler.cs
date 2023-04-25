using Airport.Application.Events;
using Core.Entities;
using Core.Entities.Terminal;
using Core.Interfaces;
using Core.Interfaces.EventHandlers;
using Core.Interfaces.Repositories;
using Core.Interfaces.Subject;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airport.Application.EventHandlers
{
    internal class DalHandler : IDalHandler, IUnitOfWork
    {
        private readonly IISUbject _subject;
        private readonly ILogger<DalHandler> _logger;
        private readonly AirportDataContext _dbContext;

        public IFlightRepository Flight { get; private set; }
        public ILegRepostiroy Leg { get; private set; }
        public IProcLogRepository ProcessLog { get; private set; }
        public IPilotRepository Pilot { get; private set; }
        private bool _disposed = false;
        public UnitOfWork(AirportDataContext dbContext,
            ILogger<DalHandler> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
            Flight = new FlightRepository(dbContext);
            Leg = new LegRepository(dbContext);
            ProcessLog = new ProcLogRepository(dbContext);
            Pilot = new PilotRepository(dbContext);
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
        public IGenericRepository<T> GenericRepository<T>() where T : BaseEntity
        {
            return new GenericRepository<T>(_dbContext);
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
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

        public DalHandler(IISUbject subject)
        {
            _subject = subject;
        }

        public async Task<Flight> GetFlightByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateAsync()
        {
            throw new NotImplementedException();
        }

        public async Task UpdateFlightAsync(Flight flight)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateLegAndFlightAsync(Leg leg, Flight flight)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateLegAsync(Leg leg)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateLegsAndFlightsAsync(Leg[] legs, Flight[] flights)
        {
            throw new NotImplementedException();
        }
    }
}

