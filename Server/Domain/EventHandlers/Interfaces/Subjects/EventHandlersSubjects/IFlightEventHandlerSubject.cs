using Core.Entities.Terminal;
using Core.EventHandlers.Enums;
using Core.EventHandlers.Interfaces.FlightInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.EventHandlers.Interfaces.Subjects.EventHandlersSubjects
{
    public interface IFlightEventHandlerSubject
    {
        void NotifyFlightEnteredLeg(Flight flight);
        void NotifyFlightOutOfTerminal(Flight flight);
        void AttachFlightHandlerToEventType(FlightTopic flightTopic, IFlightBasicEventHandler flightEventHandler);
        void DetachFlightHandlerFromEventType(FlightTopic flightTopic, IFlightBasicEventHandler flightEventHandler);
    }
}
