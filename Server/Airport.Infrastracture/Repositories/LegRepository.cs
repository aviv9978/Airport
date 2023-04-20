using Core.Entities.Terminal;
using Core.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airport.Infrastracture.Repositories
{
    public class LegRepository : GenericRepository<Leg>, ILegRepostiroy
    {
        private readonly AirportDataContext _dBContext;
        private readonly ILogger<LegRepository> _logger;
        public LegRepository(AirportDataContext dbContext, ILogger<LegRepository> logger)
            : base(dbContext)
        {
            _dBContext = dbContext;
            _logger = logger;
        }
        public int GetLegsCount() => _dBContext.Legs.Count();

    }
}
