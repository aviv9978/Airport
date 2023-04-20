﻿using Airport.Application.ILogicServices;
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
        private readonly ITerminalHub _terminalHub;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public TerminalService(ITerminalHub flightHub,
            IMapper mapper, IUnitOfWork unitOfWork)
        {
            _terminalHub = flightHub;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task StartFlightAsync(Flight flight, bool isDeparture)
        {

            IEnumerable<Leg> flightFirstLegs;
            await _unitOfWork.Flight.AddAsync(flight);
            await _unitOfWork.CommitAsync();

            var legType = isDeparture ? LegType.StartForDeparture : LegType.StartForLand;
            while (true) //instead of event for now
            {
                flightFirstLegs = await _unitOfWork.Leg.FindListAsync(leg => leg.LegType == legType);
                foreach (var leg in flightFirstLegs)
                {
                    if (leg.IsOccupied == false)
                    {
                        EnteringLegByCode(flight, leg);
                        UOWUpdateFlightAndLeg(flight, leg);
                        await _unitOfWork.CommitAsync();
                        await NextLegAsync(flight, isDeparture);
                        return;
                    }
                }
            }
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
            int procLogId = await AddProcLogAsync(flight, $"Leg number {flight.Leg.CurrentLeg}, leg id: {flight.Leg.Id}");
            await _terminalHub.SendEnteringUpdateAsync(flight, flight.Leg.Id);
            Thread.Sleep(flight.Leg.PauseTime * 1000);
            return procLogId;
        }

        private async Task MoveLegAsync(Flight flight, bool isDeparture, int procLogId)
        {
            IEnumerable<Leg> nextLegs;
            var allLegs = await _unitOfWork.Leg.GetAllAsync();
            var nextPosLegs = flight.Leg.NextPosibbleLegs;
            bool exit = false;
            while (true)
            {
                nextLegs = await _unitOfWork.Leg.FindListAsync(leg => nextPosLegs.HasFlag(leg.CurrentLeg));
                foreach (var leg in nextLegs)
                {
                    if (leg.IsOccupied == false)
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

        private async Task ChangingLegStatusAsync(Flight flight, int procLogId, Leg enteringLeg)
        {
            var leavingLeg = flight.Leg;
            var leavingLegEnum = leavingLeg.CurrentLeg;
            LegNotOccupiedByCode(leavingLeg);
            EnteringLegByCode(flight, enteringLeg);
            UOWUpdateFlightAndLeg(flight, leavingLeg);
            _unitOfWork.Leg.Update(enteringLeg);
            DateTime exitTime = await UpdateOutLogAndCommitAsync(procLogId);
            await SendLogOutHubUpdateAsync(procLogId, exitTime, leavingLegEnum);
        }


        private void EnteringLegByCode(Flight flight, Leg enteringLeg)
        {
            flight.Leg = enteringLeg;
            enteringLeg.IsOccupied = true;
            enteringLeg.Flight = flight;
        }

        private async Task FinishingFlight(Flight flight, int procLogId)
        {
            try
            {
            var finalLeg = flight.Leg;
            var finalLegEnum = finalLeg.CurrentLeg;
            Thread.Sleep(flight.Leg.PauseTime * 1000);
            LegNotOccupiedByCode(finalLeg);
            _unitOfWork.Leg.Update(finalLeg);
            flight.Leg = null;
            _unitOfWork.Flight.Update(flight);
            DateTime exitTime = await UpdateOutLogAndCommitAsync(procLogId);
            await SendLogOutHubUpdateAsync(procLogId, exitTime, finalLegEnum);
            Console.WriteLine("Flight finished!");
            }
            catch (Exception)
            {

                throw;
            }
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
            await _terminalHub.UpdateEnterLeg(legStatusOutDTO);
        }

        private async Task SendLogOutHubUpdateAsync(int procLogId, DateTime exitTime, LegNumber currentLeg)
        {
            await _terminalHub.SendLogOutUpdateAsync(procLogId, exitTime);
            var legNumber = currentLeg.AsString(EnumFormat.Description);
            await _terminalHub.UpdateLogOutLeg(new LegStatusOutDTO { IsOccupied = false, LegNumber = legNumber, Flight = null });
        }
        private void UOWUpdateFlightAndLeg(Flight flight, Leg leg)
        {
            _unitOfWork.Leg.Update(leg);
            _unitOfWork.Flight.Update(flight);
        }
        private async Task<DateTime> UpdateOutLogAndCommitAsync(int procLogId)
        {
            var exitTime = DateTime.Now;
            await _unitOfWork.ProcessLog.UpdateOutLogAsync(procLogId, exitTime);
            await _unitOfWork.CommitAsync();
            return exitTime;
        }
        private static void LegNotOccupiedByCode(Leg? leavingLeg)
        {
            leavingLeg.IsOccupied = false;
            leavingLeg.Flight = null;
        }
        public async Task ResetLegsAsync()
        {
            var allLegs = await _unitOfWork.Leg.GetAllAsync();
            var allFlights = await _unitOfWork.Flight.GetAllAsync();
            foreach (var leg in allLegs)
            {
                leg.IsOccupied = false;
                leg.Flight = null;
                _unitOfWork.Leg.Update(leg);
            }
            foreach (var flight in allFlights)
            {
                flight.Leg = null;
                _unitOfWork.Flight.Update(flight);
            }
            await _unitOfWork.CommitAsync();
        }

    }
}

