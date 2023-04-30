using Core.Entities.Terminal;
using Core.EventHandlers.Enums;
using Core.EventHandlers.Interfaces.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.EventHandlers.Interfaces.Subjects.DAL
{
    public interface IFlightDalSubject
    {
        void AttachFlightDalHandlerToEventType(DalTopic dalTopic, IFlightDalEventHandler flightDalEventHandler);
        void DetachFlightDalHandlerFromEventType(DalTopic dalTopic, IFlightDalEventHandler flightDalEventHandler);
        void NotifyIncomingFlight(Flight incomingFlight);
        Task NotifyFlightToDalAsync(DalTopic topic, Flight flight);
        void NotifyFlightFinishedLeg(Flight flight);
        void AttatchFlightToLegQueue(Flight flight, Leg leg);
        void NotifyLegHasBeenCleared(Leg leg);
        void NotifyFlightCompleted(Flight flight);
        void Detach(Flight flight, Leg leg);
    }
}
