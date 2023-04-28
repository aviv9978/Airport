using Airport.Infrastracture.Handlers.FlightHandlers;
using Core.Entities.Terminal;
using Core.EventHandlers.Interfaces.DAL;
using Core.Interfaces.Subject;
using Core.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.EventHandlers.Enums;
namespace Airport.Infrastracture.Handlers.FlightLegHandlers
{
    public class FlightNextLegClearHandler : IFlightLegDalEventHandler
    {
        public DalTopic DalTopic { get; set; } = DalTopic.FlightNextLegClear;

        private readonly IISUbject _subject;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<FlightNextLegClearHandler> _logger;

        public FlightNextLegClearHandler(IISUbject subject, IUnitOfWork unitOfWork, ILogger<FlightNextLegClearHandler> logger)
        {
            _subject = subject;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }
        public async Task NotifyAsync(FlightAndNextLeg flightAndLeg)
        {
            var flight = flightAndLeg.Flight;
            var currentLeg = flight?.Leg;
            var nextLeg = flightAndLeg.NextLeg;
            if (currentLeg == null)
            {
                UpdateFlightAndLegCode(flight, nextLeg);
                UpdateFlightAndLegInDBAsync(flight, nextLeg);
            }
            else
            {
                UpdateFlightAndBothLegCode(flight, currentLeg, nextLeg);
                UpdateFlightAndBothLegsInDBAsync(flight, currentLeg, nextLeg);
            }
            await _unitOfWork.CommitAsync();
            _logger.LogInformation($"Flight {flight.Id} entered log number {nextLeg.Id}");
            _subject.NotifyFlightEnteredLeg(flight);
        }

        private async Task UpdateFlightAndLegInDBAsync(Flight? flight, Leg? nextLeg)
        {
             _subject.NotifyFlightToDalAsync(DalTopic.UpdateFlight, flight);
             _subject.NotifyLegToDalAsync(DalTopic.UpdateLeg, nextLeg);
        }
        private async Task UpdateFlightAndBothLegsInDBAsync(Flight? flight, Leg? currentLeg, Leg? nextLeg)
        {
             UpdateFlightAndLegInDBAsync(flight, nextLeg);
             _subject.NotifyLegToDalAsync(DalTopic.UpdateLeg, currentLeg);
        }
        private static void UpdateFlightAndLegCode(Flight? flight, Leg? nextLeg)
        {
            nextLeg.Flight = flight;
            nextLeg.IsOccupied = true;
            flight.Leg = nextLeg;
        }
        private static void UpdateFlightAndBothLegCode(Flight? flight, Leg currentLeg, Leg? nextLeg)
        {
            UpdateFlightAndLegCode(flight, nextLeg);
            currentLeg.Flight = null;
            currentLeg.IsOccupied = false;
        }
    }
}
