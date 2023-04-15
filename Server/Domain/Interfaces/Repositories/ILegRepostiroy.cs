using Core.Entities.Terminal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces.Repositories
{
    public interface ILegRepostiroy
    {
        int GetLegsCount();
        Task AddLegAsync(Leg leg);

        Task<List<Leg>> GetLegsAsync();
    }
}
