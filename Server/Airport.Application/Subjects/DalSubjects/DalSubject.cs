using Core.Entities.Terminal;
using Core.EventHandlers.Enums;
using Core.EventHandlers.Interfaces.DAL;
using Core.EventHandlers.Interfaces.Subjects.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airport.Application.Events.DalSubjects
{
    public class DalSubject : IDalSubject
    {
        private readonly IFlightDalSubject _flightDalSubject;
        private readonly IFlightLegDalSubject _flightLegDalSubject;
        private readonly ILegDalSubject _legDalSubject;

        public DalSubject(IFlightDalSubject flightDalSubject, IFlightLegDalSubject flightLegDalSubject,
            ILegDalSubject legDalSubject)
        {
            _flightDalSubject = flightDalSubject;
            _flightLegDalSubject = flightLegDalSubject;
            _legDalSubject = legDalSubject;
        }

        public void AttachFlightDalHandlerToEventType(DalTopic dalTopic, IFlightDalEventHandler flightDalEventHandler)
            => _flightDalSubject.AttachFlightDalHandlerToEventType(dalTopic, flightDalEventHandler);
        public void DetachFlightDalHandlerFromEventType(DalTopic dalTopic, IFlightDalEventHandler flightDalEventHandler)
            => _flightDalSubject.DetachFlightDalHandlerFromEventType(dalTopic, flightDalEventHandler);
        public void AttachLegDalHandlerToEventType(DalTopic dalTopic, ILegDalEventHandler legDalEventHandler)
            => _legDalSubject.AttachLegDalHandlerToEventType(dalTopic, legDalEventHandler);
        public void DetachLegDalHandlerFromEventType(DalTopic dalTopic, ILegDalEventHandler legDalEventHandler)
            => _legDalSubject.DetachLegDalHandlerFromEventType(dalTopic, legDalEventHandler);
        public void AttachFlightLegDalHandlerToEventType(DalTopic dalTopic, IFlightLegDalEventHandler flightLegDalEventHandler)
            => _flightLegDalSubject.AttachFlightLegDalHandlerToEventType(dalTopic, flightLegDalEventHandler);

        public void DetachFlightLegDalHandlerFromEventType(DalTopic dalTopic, IFlightLegDalEventHandler flightLegDalEventHandler)
            => _flightLegDalSubject.DetachFlightLegDalHandlerFromEventType(dalTopic, flightLegDalEventHandler);
        public void NotifyIncomingFlight(Flight incomingFlight) => _flightDalSubject.NotifyIncomingFlight(incomingFlight);
        public async Task NotifyFlightToDalAsync(DalTopic topic, Flight flight) => await _flightDalSubject.NotifyFlightToDalAsync(topic, flight);
        public void NotifyFlightFinishedLeg(Flight flight) => _flightDalSubject.NotifyFlightFinishedLeg(flight);
        public void AttatchFlightToLegQueue(Flight flight, Leg leg) => _flightDalSubject.AttatchFlightToLegQueue(flight, leg);
        public void NotifyLegHasBeenCleared(Leg leg) => _flightDalSubject.NotifyLegHasBeenCleared(leg);
        public void NotifyFlightCompleted(Flight flight) => _flightDalSubject.NotifyFlightCompleted(flight);
        public void Detach(Flight flight, Leg leg) => _flightDalSubject.Detach(flight, leg);
        public async Task NotifyLegToDalAsync(DalTopic topic, Leg leg) => await _legDalSubject.NotifyLegToDalAsync(topic, leg);

        public void NotifyFlightNextLegClear(Flight flight, Leg leg) => _flightLegDalSubject.NotifyFlightNextLegClear(flight, leg);

    }
}
