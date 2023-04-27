using Core.Entities.Terminal;
using Core.EventHandlers.Enums;
using Core.EventHandlers.Interfaces.FlightInterfaces;
using Core.EventHandlers.Interfaces.Subjects.EventHandlersSubjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airport.Application.Events.EventHandlersSubjects
{
    public class EventHandlerSubject : IEventHandlerSubject
    {
        private readonly IFlightEventHandlerSubject _flightEventHandlerSubject;

        public EventHandlerSubject(IFlightEventHandlerSubject flightEventHandlerSubject)
        {
            _flightEventHandlerSubject = flightEventHandlerSubject;
        }

        public void AttachFlightHandlerToEventType(FlightTopic flightTopic, IFlightBasicEventHandler observer)
        {
            throw new NotImplementedException();
        }

        public void DetachFlightHandlerFromEventType(FlightTopic flightTopic, IFlightBasicEventHandler observer)
        {
            throw new NotImplementedException();
        }

        public void NotifyFlightEnteredLeg(Flight flight) => _flightEventHandlerSubject.NotifyFlightEnteredLeg(flight);

        public void NotifyFlightOutOfTerminal(Flight flight) => _flightEventHandlerSubject.NotifyFlightOutOfTerminal(flight);
    }
}
