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
    public class FlightNextLegClearHandler : IFlightLegDalHandler
    {
        private readonly IISUbject _subject;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<FlightNextLegClearHandler> _logger;

        public FlightNextLegClearHandler(IISUbject subject, IUnitOfWork unitOfWork, ILogger<FlightNextLegClearHandler> logger)
        {
            _subject = subject;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }
        public async Task NotifyAsync(FlightLeg flightAndLeg)
        {
            var flight = flightAndLeg.Flight;
            var currentLeg = flight.Leg;
            var nextLeg = flightAndLeg.Leg;
            UpdateFlightAndLegCode(flight, currentLeg, nextLeg);
            await UpdateFlightAndLegsInDBAsync(flight, currentLeg, nextLeg);
            _logger.LogInformation($"Flight {flight.Id} entered log number {nextLeg.Id}");
            _subject.NotifyFlightEnteredLeg(flight);
        }

        private async Task UpdateFlightAndLegsInDBAsync(Flight? flight, Leg? currentLeg, Leg? nextLeg)
        {
            await _subject.NotifyFlightToDalAsync(DalTopic.UpdateFlight, flight);
            await _subject.NotifyLegToDalAsync(DalTopic.UpdateLeg, currentLeg);
            await _subject.NotifyLegToDalAsync(DalTopic.UpdateLeg, nextLeg);
        }

        private static void UpdateFlightAndLegCode(Flight? flight, Leg? currentLeg, Leg? nextLeg)
        {
            currentLeg.Flight = null;
            currentLeg.IsOccupied = false;
            nextLeg.Flight = flight;
            nextLeg.IsOccupied = true;
            flight.Leg = nextLeg;
        }
    }
}
