using Airport.Application.ILogicServices;
using AutoMapper;
using Core.DTOs.Outgoing;
using Core.Entities;
using Core.Entities.Terminal;
using Core.Enums;
using Core.Hubs;
using Core.Interfaces;
using Core.Interfaces.Repositories;
using EnumsNET;

namespace Airport.Application.LogicServices
{
    public class TerminalService : ITerminalService
    {
        private readonly IGenericRepository<T> _repos;
        private readonly ITerminalHub _terminalHub;
        private readonly ILegRepostiroy _legRepos;
        private readonly IProcLogRepository _procLogRepos;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IFlightRepository _flightRepos;
        public static List<Leg> _legs;
        public static List<Leg> Legs => _legs;
        private readonly IMapper _mapper;
        public TerminalService(ILegRepostiroy legRepos, IProcLogRepository procLog,
            IFlightRepository rep, ITerminalHub flightHub,
            IMapper mapper, IUnitOfWork unitOfWork, IGenericRepository<T> repos)
        {
            _legRepos = legRepos;
            _procLogRepos = procLog;
            _flightRepos = rep;
            _terminalHub = flightHub;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _repos = repos;
        }

        public async Task StartFlightAsync(Flight flight, bool isDeparture)
        {
            IEnumerable<Leg> flightFirstLegs;
            await AddingFlightInitArrAsync(flight);

            var legType = isDeparture ? LegType.StartForDeparture : LegType.StartForLand;
            flightFirstLegs = _legs.Where(leg => leg.LegType == legType);

            while (true) //instead of event for now
            {
                foreach (var leg in flightFirstLegs)
                {
                    if (leg.IsOccupied == false)
                    {
                        flight.Leg = leg;
                        leg.IsOccupied = true;
                        leg.Flight = flight;
                        await NextLegAsync(flight, isDeparture);
                        return;
                    }
                }
            }
        }

        private async Task AddingFlightInitArrAsync(Flight flight)
        {
            await _unitOfWork.Flights.AddAsync(flight);
            await _unitOfWork.CommitAsync();
            await InitLegsArrayAsync();
        }

        private async Task NextLegAsync(Flight flight, bool isDeparture)
        {

            int procLogId = await InLegProcessAsync(flight);
            if (isDeparture)
            {
                if (flight.Leg.LegType.HasFlag(LegType.BeforeFly))
                {
                    await FinishingFlight(flight, procLogId);
                    return;
                }
            }
            else if (flight.Leg.LegType == LegType.StartForDeparture)
            {
                await FinishingFlight(flight, procLogId);
                return;
            }

            await MoveLegAsync(flight, isDeparture, procLogId);
        }

        private async Task<int> InLegProcessAsync(Flight flight)
        {
            await _terminalHub?.SendEnteringUpdateAsync(flight, flight.Leg.Id);
            int procLogId = await AddProcLogAsync(flight, $"Leg number {flight.Leg.CurrentLeg}, leg id: {flight.Leg.Id}");
            Thread.Sleep(flight.Leg.PauseTime * 1000);
            return procLogId;
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
                        await ChangingLegStatusAsync(flight, procLogId, leg);
                        await NextLegAsync(flight, isDeparture);
                        exit = true;
                        break;
                    }
                }
                if (exit) break;
            }
        }

        private async Task ChangingLegStatusAsync(Flight flight, int procLogId, Leg? leg)
        {
            var currentLeg = flight.Leg.CurrentLeg;
            flight.Leg.Flight = null;
            flight.Leg.IsOccupied = false;
            flight.Leg = leg;
            leg.Flight = flight;
            leg.IsOccupied = true;
            await UpdateLogExit(procLogId, DateTime.Now, currentLeg);
        }

        private async Task FinishingFlight(Flight flight, int procLogId)
        {
            Thread.Sleep(flight.Leg.PauseTime * 1000);
            await UpdateLogExit(procLogId, DateTime.Now, flight.Leg.CurrentLeg);
            Console.WriteLine("Flight finished!");
            flight.Leg.IsOccupied = false;
            flight.Leg.Flight = null;
        }

        private async Task<int> AddProcLogAsync(Flight flight, string message)
        {
            var procLog = new ProcessLog()
            {
                Message = message,
                Flight = flight,
                LegNumber = flight?.Leg?.CurrentLeg.AsString(EnumFormat.Description),
                EnterTime = DateTime.Now
            };
            await UpdatingLogsAsync(procLog);
            return procLog.Id;
        }

        private async Task UpdatingLogsAsync(ProcessLog procLog)
        {
            await _procLogRepos.AddProcLogAsync(procLog);
            var procLogOutDTO = _mapper.Map<ProcessLogOutDTO>(procLog);
            await _terminalHub.SendLogAsync(procLogOutDTO);
            var legStatusOutDTO = _mapper.Map<LegStatusOutDTO>(procLog);
            legStatusOutDTO.IsOccupied = true;
            await _terminalHub.UpdateEnterLeg(legStatusOutDTO);
        }

        private async Task UpdateLogExit(int procLogId, DateTime exitTime, LegNumber currentLeg)
        {
            await _procLogRepos.UpdateOutLogAsync(procLogId, exitTime);
            await _terminalHub.SendLogOutUpdateAsync(procLogId, exitTime);
            var legNumber = currentLeg.AsString(EnumFormat.Description);
            await _terminalHub.UpdateLogOutLeg(new LegStatusOutDTO { IsOccupied = false, LegNumber = legNumber, Flight = null });
        }
        private async Task InitLegsArrayAsync()
        {
            if (_legs == null)
                _legs = await _legRepos.GetLegsAsync();
        }
    }
}

