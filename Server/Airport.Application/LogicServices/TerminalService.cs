using Airport.Application.ILogicServices;
using Core.Entities;
using Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airport.Application.LogicServices
{
    public class TerminalService : ITerminalService
    {
        private readonly ILegRepostiroy _legRepos;

        public TerminalService(ILegRepostiroy legRepos)
        {
            _legRepos = legRepos;
        }

        public async Task StartDepartureAsync(Flight flight)
        {
            if (flight == null)
                throw new ArgumentNullException();


        }

        public async Task StartLandAsync(Flight flight)
        {
            throw new NotImplementedException();
        }
    }
}
