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
        private static ICollection<Leg> _legs;
        private static int _legsCur = Enum.GetNames(typeof(Core.Enums.LegNumber)).Length;
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
            if (_legs == null)
            _legs = await _legRepos.GetLegsAsync();

            await LandMoveLeg(flight);
        }
        private async Task LandMoveLeg(Flight flight)
        {
            if (flight.Leg.LegType == Core.Enums.LegType.Departure)
            {
                Thread.Sleep(10000);
                Console.WriteLine("Flight finished!");
                flight.Leg.Flight = null;
                return;
            }
            var nextPosLegs = flight.Leg.NextPosibbleLegs;

            var nextLegs = _legs.Where(leg => leg.CurrentLeg.HasFlag(nextPosLegs));
            foreach (var leg in nextLegs)
            {
                if (leg != null && leg.Flight != null)
                {
                    flight.Leg = leg;
                    await LandMoveLeg(flight);
                    break;
                }
                return;
            }
        }
    }
}

