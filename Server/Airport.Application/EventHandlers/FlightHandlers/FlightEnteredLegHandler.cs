using Core.Entities.Terminal;
using Core.Enums;
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
using Core.Entities;

namespace Airport.Application.EventHandlers.FlightHandlers
{
    public class FlightEnteredLegHandler : IFlightBasicEventHandler
    {
        public FlightTopic FlightTopic { get; set; } = FlightTopic.FlightEnteredLeg;

        private readonly IISUbject _subject;
        private readonly ILogger<FlightEnteredLegHandler> _logger;

        public FlightEnteredLegHandler(IISUbject subject, ILogger<FlightEnteredLegHandler> logger)
        {
            _subject = subject;
            _logger = logger;
        }
        public async Task Notify(Flight flight)
        {
            //_lastProcId = await InLegProcessAsync(flight);
            await Task.Delay(flight.Leg.PauseTime * 1000);
            _logger.LogInformation($"Flight {flight.Id} completed leg {flight.Leg.Id}");
            if (flight.IsDeparture)
            {
                if (flight.Leg.LegType.HasFlag(LegType.BeforeFly))
                {
                    _subject.NotifyFlightCompleted(flight);
                    return;
                }
            }
            else if (flight.Leg.LegType == LegType.StartForDeparture)
            {
                _subject.NotifyFlightCompleted(flight);
                return;
            }
            _subject.NotifyFlightFinishedLeg(flight);
        }
    }
}
