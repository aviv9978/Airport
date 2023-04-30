using Core.Entities.Terminal;
using Core.EventHandlers.Interfaces.FlightInterfaces;
using Core.Interfaces.Subject;
using Core.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.EventHandlers.Enums;
using Core.EventHandlers.Interfaces.DAL;

namespace Airport.Infrastracture.Handlers.FlightHandlers
{
    public class FlightCompletedHandler : IFlightDalEventHandler
    {
        public DalTopic DalTopic { get; set; } = DalTopic.FlightCompleted;

        private readonly IISUbject _subject;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<FlightCompletedHandler> _logger;

        public FlightCompletedHandler(IISUbject subject, IUnitOfWork unitOfWork, ILogger<FlightCompletedHandler> logger)
        {
            _subject = subject;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }
        public async Task NotifyAsync(Flight flight)
        {
            var leg = flight.Leg;
            UpdateFlightAndLegCode(flight, leg);
            await _subject.NotifyFlightToDalAsync(DalTopic.UpdateFlight, flight);
            await _subject.NotifyLegToDalAsync(DalTopic.UpdateLeg, leg);
            await _unitOfWork.CommitAsync();
            _logger.LogInformation($"Flight {flight.Id} Completed.");
            _subject.NotifyFlightOutOfTerminal(flight);
        }

        private static void UpdateFlightAndLegCode(Flight flight, Leg leg)
        {
            leg.IsOccupied = false;
            leg.Flight = null;
            flight.Leg = null;
        }

    }
}
