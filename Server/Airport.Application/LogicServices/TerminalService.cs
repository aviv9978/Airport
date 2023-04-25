using Airport.Application.ILogicServices;
using AutoMapper;
using Core.DTOs.Outgoing;
using Core.Entities;
using Core.Entities.Terminal;
using Core.Enums;
using Core.Events;
using Core.Hubs;
using Core.Interfaces;
using Core.Interfaces.Events;
using EnumsNET;
using Microsoft.VisualStudio.Threading;
using System;

namespace Airport.Application.LogicServices
{
    public class TerminalService : ITerminalService
    {
        private readonly ITerminalHub _terminalHub;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ISubject _subjet;
        private readonly IMapper _mapper;
        private int _lastProcId;
        public TerminalService(ITerminalHub flightHub,
            IMapper mapper, IUnitOfWork unitOfWork,
            ISubject subjet)
        {
            _terminalHub = flightHub;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _subjet = subjet;
        }

        public async Task StartFlightAsync(Flight flight, bool isDeparture)
        {
            IEnumerable<Leg> flightFirstLegs;
            await _unitOfWork.Flight.AddAsync(flight);
            await _unitOfWork.CommitAsync();

            var legType = isDeparture ? LegType.StartForDeparture : LegType.StartForLand;
            flightFirstLegs = await _unitOfWork.Leg.FindListAsync(leg => leg.LegType == legType);
            foreach (var enteringLeg in flightFirstLegs)
            {
                if (enteringLeg.IsOccupied == false)
                {
                    EnteringLegByCode(flight, enteringLeg);
                    await UOWUpdateFlightAndLegAsync(flight, enteringLeg);
                    await _unitOfWork.CommitAsync();
                    await NextLegAsync(flight, isDeparture);
                    return;
                }
            }
            await RegisterToSubjectAndEventAsync(flight, flightFirstLegs);
        }

        private async Task NextLegAsync(Flight flight, bool isDeparture)
        {
            _lastProcId = await InLegProcessAsync(flight);
            if (isDeparture)
            {
                if (flight.Leg.LegType.HasFlag(LegType.BeforeFly))
                {
                    await FinishingFlightAsync(flight, _lastProcId);
                    return;
                }
            }
            else if (flight.Leg.LegType == LegType.StartForDeparture)
            {
                await FinishingFlightAsync(flight, _lastProcId);
                return;
            }
            await MoveLegAsync(flight, isDeparture, _lastProcId);
        }

        private async Task<int> InLegProcessAsync(Flight flight)
        {
            int procLogId = await AddProcLogAsync(flight, $"Leg number {flight.Leg.CurrentLeg}, leg id: {flight.Leg.Id}");
            await _terminalHub.SendEnteringUpdateAsync(flight, flight.Leg.Id);
            Thread.Sleep(flight.Leg.PauseTime * 1000);
            return procLogId;
        }

        private async Task MoveLegAsync(Flight flight, bool isDeparture, int procLogId)
        {
            IEnumerable<Leg>? nextLegs;
            var allLegs = await _unitOfWork.Leg.GetAllAsync();
            var nextPosLegs = flight.Leg.NextPosibbleLegs;
            nextLegs = null;
            nextLegs = await _unitOfWork.Leg.FindListAsync(leg => nextPosLegs.HasFlag(leg.CurrentLeg));
            foreach (var leg in nextLegs)
            {
                if (leg.IsOccupied == false)
                {
                    await LeavingCurrentLegEnteringNewLegAsync(flight, isDeparture, procLogId, leg);
                    return;
                }
            }
            await RegisterToSubjectAndEventAsync(flight, nextLegs);
            return;
        }

        private async Task LeavingCurrentLegEnteringNewLegAsync(Flight flight, bool isDeparture, int procLogId, Leg enteringLeg)
        {
            await ChangingLegStatusAsync(flight, procLogId, enteringLeg);
            await NextLegAsync(flight, isDeparture);
        }

        private async Task RegisterToSubjectAndEventAsync(Flight flight, IEnumerable<Leg> nextLegs)
        {
            foreach (var leg in nextLegs)
                _subjet.Attach(leg, flight);

            var tcs = new TaskCompletionSource<bool>();
            AsyncEventHandler? eventHandler = null;
            eventHandler = async (sender, args) =>
            {
                await UsingEventAsync(sender, args);
                (sender as Leg).ClearedLeg -= eventHandler;
                tcs.SetResult(true);
            };
            flight.Leg.ClearedLeg += eventHandler;
            await tcs.Task;

            //if (flight.Leg.ClearedLeg is null)
            //    flight.Leg.ClearedLeg += UsingEventAsync;
        }

        private async Task ChangingLegStatusAsync(Flight flight, int procLogId, Leg enteringLeg)
        {
            var leavingLeg = flight.Leg;
            var leavingLegEnum = leavingLeg.CurrentLeg;
            LegNotOccupiedByCode(leavingLeg);
            EnteringLegByCode(flight, enteringLeg);
            await UOWUpdateFlightTwoLegsAsync(flight, leavingLeg, enteringLeg);
            DateTime exitTime = await UpdateOutLogAndCommitAsync(procLogId);
            _subjet.NotifyAsync(leavingLeg);
            await SendLogOutHubUpdateAsync(procLogId, exitTime, leavingLegEnum);
        }
       
        private void EnteringLegByCode(Flight flight, Leg enteringLeg)

        {
            flight.Leg = enteringLeg;
            enteringLeg.IsOccupied = true;
            enteringLeg.Flight = flight;
        }

        private async Task FinishingFlightAsync(Flight flight, int procLogId)
        {
            try
            {
                var finalLeg = flight.Leg;
                var finalLegEnum = finalLeg.CurrentLeg;
                Thread.Sleep(flight.Leg.PauseTime * 1000);
                LegNotOccupiedByCode(finalLeg);
                flight.Leg = null;
                await UOWUpdateFlightAndLegAsync(flight, finalLeg);
                DateTime exitTime = await UpdateOutLogAndCommitAsync(procLogId);
                await SendLogOutHubUpdateAsync(procLogId, exitTime, finalLegEnum);
                Console.WriteLine("Flight finished!");
                _subjet.NotifyAsync(finalLeg);
                return;
            }
            catch (Exception)
            {

                throw;
            }
        }

        private async Task SendLogOutHubUpdateAsync(int procLogId, DateTime exitTime, LegNumber currentLeg)

        {
            await _terminalHub.SendLogOutUpdateAsync(procLogId, exitTime);
            var legNumber = currentLeg.AsString(EnumFormat.Description);
            await _terminalHub.UpdateLogOutLegAsync(new LegStatusOutDTO { IsOccupied = false, LegNumber = legNumber, Flight = null });
        }

        private async Task UOWUpdateFlightAndLegAsync(Flight flight, Leg leg)
        {
            await _unitOfWork.Leg.UpdateAsync(leg);
            await _unitOfWork.Flight.UpdateAsync(flight);
        }

        private async Task UOWUpdateFlightTwoLegsAsync(Flight flight, Leg leavingLeg, Leg enteringLeg)
        {
            await UOWUpdateFlightAndLegAsync(flight, leavingLeg);
            await _unitOfWork.Leg.UpdateAsync(enteringLeg);
        }

        private static void LegNotOccupiedByCode(Leg? leavingLeg)
        {
            leavingLeg.IsOccupied = false;
            leavingLeg.Flight = null;
        }

        public async Task ResetLegsAsync()
        {
            var allLegs = await _unitOfWork.Leg.GetAllAsync();
            foreach (var leg in allLegs)
            {
                if (leg.Flight != null)
                {
                    leg.Flight.Leg = null;
                    await _unitOfWork.Flight.UpdateAsync(leg.Flight);
                }
                leg.IsOccupied = false;
                leg.Flight = null;
                await _unitOfWork.Leg.UpdateAsync(leg);
            }
            await _unitOfWork.CommitAsync();
        }

        private async Task UsingEventAsync(object sender, object e)
        {
            var currentLeg = (Leg)sender;
            var flight = currentLeg.Flight;
            var legEventArgs = (LegEventArgs)e;
            var enteringLeg = legEventArgs.NextLeg;
            await LeavingCurrentLegEnteringNewLegAsync(flight, flight.IsDeparture, _lastProcId, enteringLeg);
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
            await _unitOfWork.ProcessLog.AddAsync(procLog);
            await _unitOfWork.CommitAsync();
            var procLogOutDTO = _mapper.Map<ProcessLogOutDTO>(procLog);
            await _terminalHub.SendLogAsync(procLogOutDTO);
            var legStatusOutDTO = _mapper.Map<LegStatusOutDTO>(procLog);
            legStatusOutDTO.IsOccupied = true;
            await _terminalHub.UpdateEnterLegAsync(legStatusOutDTO);
        }

        private async Task<DateTime> UpdateOutLogAndCommitAsync(int procLogId)
        {
            var exitTime = DateTime.Now;
            await _unitOfWork.ProcessLog.UpdateOutLogAsync(procLogId, exitTime);
            await _unitOfWork.CommitAsync();
            return exitTime;
        }
    }
}

