using Airport.Application.ILogicServices;
using Core.Entities;
using Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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

        public async Task StartFlightAsync(Flight flight, bool isDeparture)
        {
            IEnumerable<Leg> flightFirstLet;
            await CommonStartAsync();

            if (isDeparture)
                flightFirstLet = _legs.Where(leg => leg.LegType == Core.Enums.LegType.StartForDeparture);
            else flightFirstLet = _legs.Where(leg => leg.LegType == Core.Enums.LegType.StartForLand);

            while (true) //instead of event for now
            {
                foreach (var leg in flightFirstLet)
                {
                    if (leg.isTaken == false)
                    {
                        flight.Leg = leg;
                        leg.isTaken = true;
                        await NextLegAsync(flight, isDeparture);
                        //leg.isTaken = false;
                        return;
                    }
                }
                Thread.Sleep(3000);
            }
        }

        private async Task NextLegAsync(Flight flight, bool isDeparture)
        {
            int procLogId = await AddProcLogAsync(flight, flight.Leg, $"Leg number {flight.Leg.CurrentLeg}, leg id: {flight.Leg.Id}");
            if (isDeparture)
            {
                if (flight.Leg.LegType.HasFlag(Core.Enums.LegType.BeforeFly))
                {
                    await FinishingFlight(flight, procLogId);
                    return;
                }
            }

            else if (flight.Leg.LegType == Core.Enums.LegType.StartForDeparture)
            {
                await FinishingFlight(flight, procLogId);
                return;
            }

            Thread.Sleep(flight.Leg.PauseTime * 1000);
            var nextPosLegs = flight.Leg.NextPosibbleLegs;

            var nextLegs = _legs.Where(leg => nextPosLegs.HasFlag(leg.CurrentLeg));
            foreach (var leg in nextLegs)
            {
                if (leg != null && leg.isTaken == false)
                {
                    flight.Leg.isTaken = false;
                    flight.Leg = leg;
                    leg.isTaken = true;
                    await UpdateLogExit(procLogId);
                    await NextLegAsync(flight, isDeparture);
                    break;
                }
                // else rise event to wait
            }
        }

        private async Task FinishingFlight(Flight flight, int procLogId)
        {
            Thread.Sleep(flight.Leg.PauseTime * 1000);
            await UpdateLogExit(procLogId);
            Console.WriteLine("Flight finished!");
            flight.Leg.isTaken = false;
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
            await _procLogRepos.UpdateOutLogAsync(procLogId);
        }
        private async Task CommonStartAsync()
        {
            if (_legs == null)
                _legs = await _legRepos.GetLegsAsync();
        }
    }
}

