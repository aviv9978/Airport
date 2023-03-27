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
        private static bool[]? _legsStatus;
        private static ICollection<Leg> _legs;
        public TerminalService(ILegRepostiroy legRepos)
        {
            _legRepos = legRepos;
            if (_legsStatus == null)
                _legsStatus = new bool[_legRepos.GetLegsCount()];
        }

        private async Task<ICollection<Leg>> CommonStartAsync()
        {
            _legsStatus = new bool[_legRepos.GetLegsCount()];
            return await _legRepos.GetLegsAsync();
        }

        public async Task StartDepartureAsync(Flight flight)
        {
            if (flight == null)
                throw new ArgumentNullException();


        }

        public async Task StartLandAsync(Flight flight)
        {
            if (_legs == null)
                _legs = await CommonStartAsync();
            int legLandCount = 0;

            if (_legsStatus[0] == true)
                Thread.Sleep(5000);
            foreach (var leg in _legs)
                if (leg.LegType == Core.Enums.LegType.Land)
                    legLandCount++;

            for (int i = 0; i < _legs.Count;)
            {
                if (_legsStatus[i] == true)
                    Thread.Sleep(10000);
                else
                {
                    _legsStatus[i] = true;
                    ProcessLog procLog = new ProcessLog() { EnterTime = DateTime.Now, Leg =  };
                    flight.ProcessLogs.Add(new ProcessLog() { })

                }
            }
            _legsStatus[0] = true;
            throw new NotImplementedException();
        }
    }
}
}
