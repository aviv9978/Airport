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
        private readonly IProcLogRepository _procLogRepos;
        private static ICollection<Leg> _legs;
        public TerminalService(ILegRepostiroy legRepos, IProcLogRepository procLog)
        {
            _legRepos = legRepos;
            _procLogRepos = procLog;
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
            int procLogId = await AddProcLogAsync(flight, flight.Leg, $"Entering Leg {flight.Leg}");
            if (flight.Leg.LegType == Core.Enums.LegType.Departure)
            {
                Thread.Sleep(10000);
                Console.WriteLine("Flight finished!");
                flight.Leg.Flight = null;
                return;
            }
            Thread.Sleep(flight.Leg.PauseTime * 1000);
            var nextPosLegs = flight.Leg.NextPosibbleLegs;

            var nextLegs = _legs.Where(leg => leg.CurrentLeg.HasFlag(nextPosLegs));
            foreach (var leg in nextLegs)
            {
                if (leg != null && leg.Flight != null)
                {
                    flight.Leg = leg;
                    await UpdateLogExit(procLogId);
                    await LandMoveLeg(flight);
                    break;
                }
                // else rise event to wait
            }
        }

        private async Task<int> AddProcLogAsync(Flight flight, Leg leg, string message)
        {
            var procLog = new ProcessLog()
            {
                Leg = leg,
                Message = message,
                Flight = flight,
                EnterTime = DateTime.Now
            };
            await _procLogRepos.AddProcLogAsync(procLog);
            return procLog.Id;
        }

        private async Task UpdateLogExit(int procLogId)
        {
            await _procLogRepos.UpdateOutLog(procLogId);
        }
    }
}

