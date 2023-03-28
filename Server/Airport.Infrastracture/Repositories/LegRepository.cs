using Core.Entities;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airport.Infrastracture.Repositories
{
    public class LegRepository : ILegRepostiroy
    {
        private readonly AirportDataContext _dBContext;
        private readonly ILogger<LegRepository> _logger;
        public LegRepository(AirportDataContext dbContext, ILogger<LegRepository> logger)
        {
            _dBContext = dbContext;
            _logger = logger;
        }
        public int GetLegsCount() => _dBContext.Legs.Count();
        public async Task AddLegAsync(Leg leg)
        {
            try
            {
                await _dBContext.AddAsync(leg);
                await _dBContext.SaveChangesAsync();
                _logger.LogInformation($"Added Log! Leg's Id: {leg.Id}, Leg's Number: {leg.CurrentLeg}, Leg's Next Posibble Legs {leg.NextPosibbleLegs}");

            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                throw;
            }
        }

        public async Task<ICollection<Leg>> GetLegsAsync()
        {
            try
            {
                var legs = await _dBContext.Legs.ToListAsync();
                _logger.LogInformation("Got all legs from db!");
                return legs;
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                throw;
            }
        }

    }
}
