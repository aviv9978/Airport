using Airport.Application.ILogicServices;
using AutoMapper;
using Core.DTOs.Outgoing;
using Core.Entities;
using Core.Entities.Terminal;
using Core.Enums;
using Core.Hubs;
using Core.Interfaces.Repositories;
using Microsoft.AspNetCore.SignalR;
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
        private readonly ITerminalHub _terminalHub;
        private readonly ILegRepostiroy _legRepos;
        private readonly IProcLogRepository _procLogRepos;
        private readonly IFlightRepository _flightRepos;
        private static ICollection<Leg> _legs;
        private readonly IMapper _mapper;
        private event Func<Flight, bool, Task> _nextLegEvent;
        public TerminalService(ILegRepostiroy legRepos, IProcLogRepository procLog,
            IFlightRepository rep, ITerminalHub flightHub,
            IMapper mapper)
        {
            _legRepos = legRepos;
            _procLogRepos = procLog;
            _flightRepos = rep;
            _terminalHub = flightHub;
            _mapper = mapper;
        }

        public async Task StartFlightAsync(Flight flight, bool isDeparture)
        {
            await _flightRepos.AddFlightAsync(flight);
            IEnumerable<Leg> flightFirstLeg;
            await CommonStartAsync();

            if (isDeparture)
                flightFirstLeg = _legs.Where(leg => leg.LegType == Core.Enums.LegType.StartForDeparture);
            else flightFirstLeg = _legs.Where(leg => leg.LegType == Core.Enums.LegType.StartForLand);

            while (true) //instead of event for now
            {
                foreach (var leg in flightFirstLeg)
                {
                    if (leg.IsOccupied == false)
                    {
                        flight.Leg = leg;
                        leg.IsOccupied = true;
                        await NextLegAsync(flight, isDeparture);
                        return;
                    }
                }
                Thread.Sleep(3000);
            }
        }

        private async Task NextLegAsync(Flight flight, bool isDeparture)
        {
            await _terminalHub?.SendEnteringUpdateAsync(flight, flight.Leg.Id);
            int procLogId = await AddProcLogAsync(flight, $"Leg number {flight.Leg.CurrentLeg}, leg id: {flight.Leg.Id}");
            Thread.Sleep(flight.Leg.PauseTime * 1000);
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

            await MoveLegAsync(flight, isDeparture, procLogId);
        }

        private async Task MoveLegAsync(Flight flight, bool isDeparture, int procLogId)
        {
            var nextPosLegs = flight.Leg.NextPosibbleLegs;

            var nextLegs = _legs.Where(leg => nextPosLegs.HasFlag(leg.CurrentLeg));
            bool exit = false;
            while (true)
            {
                foreach (var leg in nextLegs)
                {
                    if (leg != null && leg.IsOccupied == false)
                    {
                        var currentLeg = flight.Leg.CurrentLeg;
                        flight.Leg.IsOccupied = false;
                        flight.Leg = leg;
                        // _nextLegEvent.Invoke(flight, isDeparture);
                        leg.IsOccupied = true;
                        await UpdateLogExit(procLogId, DateTime.Now, currentLeg);
                        await NextLegAsync(flight, isDeparture);
                        exit = true;

                        break;
                    }
                }
                // _nextLegEvent += (a, b) => NextLegAsync(flight, isDeparture);
                if (exit) break;
            }
        }

        private async Task FinishingFlight(Flight flight, int procLogId)
        {
            Thread.Sleep(flight.Leg.PauseTime * 1000);
            await UpdateLogExit(procLogId, DateTime.Now,flight.Leg.CurrentLeg);
            Console.WriteLine("Flight finished!");
            flight.Leg.IsOccupied = false;
        }

        private async Task<int> AddProcLogAsync(Flight flight, string message)
        {
            var procLog = new ProcessLog()
            {
                Message = message,
                Flight = flight,
                LegNum = ((int)flight.Leg.CurrentLeg),
                EnterTime = DateTime.Now
            };
            await _procLogRepos.AddProcLogAsync(procLog);
            var procLogOutDTO = _mapper.Map<ProcessLogOutDTO>(procLog);
            await _terminalHub.SendLogAsync(procLogOutDTO);
            await _terminalHub.UpdateEnterLeg(new LegStatusOutDTO { IsOccupied = true, LegNumber = flight.Leg.CurrentLeg });
            return procLog.Id;
        }

        private async Task UpdateLogExit(int procLogId, DateTime exitTime, LegNumber currentLeg)
        {
            await _procLogRepos.UpdateOutLogAsync(procLogId, exitTime);
            await _terminalHub.SendLogOutUpdateAsync(procLogId, exitTime);
            await _terminalHub.UpdateLogOutLeg(new LegStatusOutDTO { IsOccupied = false, LegNumber = currentLeg });

        }
        private async Task CommonStartAsync()
        {
            if (_legs == null)
                _legs = await _legRepos.GetLegsAsync();
        }
    }
}

