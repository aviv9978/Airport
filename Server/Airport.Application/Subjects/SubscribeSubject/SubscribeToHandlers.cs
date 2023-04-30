using Core.Entities.Terminal;
using Core.EventHandlers.Enums;
using Core.EventHandlers.Interfaces;
using Core.EventHandlers.Interfaces.DAL;
using Core.EventHandlers.Interfaces.FlightInterfaces;
using Core.Interfaces.Subject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airport.Application.Subjects.SubscribeSubject
{
    public class SubscribeToHandlers : ISubscribeToHandlers
    {
        private readonly IEnumerable<IFlightDalEventHandler> _flightDalEventHandlers;
        private readonly ILegDalEventHandler _legDalEventHandler;
        private readonly IFlightLegDalEventHandler _flightLegDalEventHandler;
        private readonly IFlightBasicEventHandler _flightBasicEventHandler;
        private readonly IISUbject _subject;

        public SubscribeToHandlers(IISUbject subject,
                IEnumerable<IFlightDalEventHandler> flightDalEventHandlers,
                IFlightLegDalEventHandler flightLegDalEventHandler,
                ILegDalEventHandler legDalEventHandler,
                IFlightBasicEventHandler flightBasicEventHandler)
        {
            _flightDalEventHandlers = flightDalEventHandlers;
            _legDalEventHandler = legDalEventHandler;
            _flightLegDalEventHandler = flightLegDalEventHandler;
            _flightBasicEventHandler = flightBasicEventHandler;
            _subject = subject;
            SubscribeToBasicDalHandler();
            SubscribeToFlightBasicEventHandler();
        }

        public void AddingFlight(Flight flight) => _subject.NotifyFlightToDalAsync(DalTopic.AddFlight, flight);


        private void SubscribeToBasicDalHandler()
        {
            foreach (var flightDalEventHandler in _flightDalEventHandlers)
            {
                _subject.AttachFlightDalHandlerToEventType(flightDalEventHandler.DalTopic, flightDalEventHandler);
            }
            _subject.AttachFlightLegDalHandlerToEventType(_flightLegDalEventHandler.DalTopic, _flightLegDalEventHandler);
            _subject.AttachLegDalHandlerToEventType(_legDalEventHandler.DalTopic, _legDalEventHandler);
        }

        private void SubscribeToFlightBasicEventHandler()
        {
            _subject.AttachFlightHandlerToEventType(_flightBasicEventHandler.FlightTopic, _flightBasicEventHandler);
        }
    }
}
