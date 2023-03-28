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
        }

        private async Task CommonStartAsync()
        {
            _legs = await _legRepos.GetLegsAsync();
            _legsStatus = new bool[_legs.Count];
        }

        public async Task StartDepartureAsync(Flight flight)
        {
            if (flight == null)
                throw new ArgumentNullException();


        }

        public async Task StartLandAsync(Flight flight)
        {
            if (_legs == null)
                await CommonStartAsync();

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
                    //ProcessLog procLog = new ProcessLog() { EnterTime = DateTime.Now, Leg =  };
                    //flight.ProcessLogs.Add(new ProcessLog() { })

                }
            }

        }

        private async Task LandMoveLeg(Flight flight)
        {
            var flightNextLegs = flight.Leg.NextPosibbleLegs;
            if (flightNextLegs == Core.Enums.LegNumber.Eig)
            {
                Thread.Sleep(10000);
                Console.WriteLine("Flight finished!");
                flight = null;
                return;
            }
            else
            {
                var nextLeg = _legs.FirstOrDefault(leg => leg.CurrentLeg.HasFlag(flightNextLegs));
                if (nextLeg.CurrentLeg.HasFlag(Core.Enums.LegNumber.Six))
                {
                    var legSix = _legs.FirstOrDefault(
                        leg => leg.CurrentLeg.HasFlag(Core.Enums.LegNumber.Six));
                    var legSev = _legs.FirstOrDefault(
                        leg => leg.CurrentLeg.HasFlag(Core.Enums.LegNumber.Sev));
                     
                    while (true) //Just for now instead of an event
                    {
                        if(legSix != null && legSix.Flight != null)
                        {
                            flight.Leg = legSix;
                            await LandMoveLeg(flight);
                            return;
                        }
                        if (legSev != null && legSev.Flight != null)
                        {
                            flight.Leg = legSev;
                            await LandMoveLeg(flight);
                            return;
                        }
                    }

                }
                while (true)
                {
                    if (nextLeg != null && nextLeg.Flight != null)
                    {
                        flight.Leg = nextLeg;
                        await LandMoveLeg(flight);
                        return;
                    }
                }
            }

        }
    }
}

