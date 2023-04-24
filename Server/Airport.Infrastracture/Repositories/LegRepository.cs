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
using System.Xml;

namespace Airport.Infrastracture.Repositories
{
    public class LegRepository : GenericRepository<Leg>, ILegRepostiroy
    {
        private readonly AirportDataContext _dbContext;
      //  private readonly ILogger<LegRepository> _logger;
        public LegRepository(AirportDataContext dbContext)
            : base(dbContext)
        {
            _dbContext = dbContext;
            //_logger = logger;
        }
        public int GetLegsCount() => _dbContext.Legs.Count();

    }
}
