using Core.DTOs.Incoming;
using Core.Entities;
using Core.Entities.Terminal;
using Core.EventHandlers.Enums;
using Core.EventHandlers.Interfaces;
using Core.EventHandlers.Interfaces.DAL;
using Core.EventHandlers.Interfaces.FlightInterfaces;
using Core.EventHandlers.Interfaces.Subjects.DAL;
using Core.EventHandlers.Interfaces.Subjects.EventHandlersSubjects;
using Core.EventHandlers.Interfaces.Subjects.Subscribers;
using Core.Interfaces;
using Core.Interfaces.Subject;

namespace Airport.Application.Events
{
    public class SSubject : IISUbject
    {
        private readonly IDalSubject _dalSubject;
        private readonly IEventHandlerSubject _eventHandlerSubject;
        //private readonly ISubscribeSubject _subscribeSubject;
        public SSubject(IDalSubject dalSubject,
                        IEventHandlerSubject eventHandlerSubject)
                        //ISubscribeSubject subscribeSubject)
        {
            _dalSubject = dalSubject;
            _eventHandlerSubject = eventHandlerSubject;
            //_subscribeSubject = subscribeSubject;
        }


        public void AttachFlightHandlerToEventType(FlightTopic flightTopic, IFlightBasicEventHandler flightHandler) =>
            _eventHandlerSubject.AttachFlightHandlerToEventType(flightTopic, flightHandler);
        public void DetachFlightHandlerFromEventType(FlightTopic flightTopic, IFlightBasicEventHandler flightHandler) =>
            _eventHandlerSubject.DetachFlightHandlerFromEventType(flightTopic, flightHandler);
        public void AttachFlightDalHandlerToEventType(DalTopic dalTopic, IFlightDalEventHandler flightDalHandler)
            => _dalSubject.AttachFlightDalHandlerToEventType(dalTopic, flightDalHandler);
        public void DetachFlightDalHandlerFromEventType(DalTopic dalTopic, IFlightDalEventHandler flightDalHandler) =>
            _dalSubject.DetachFlightDalHandlerFromEventType(dalTopic, flightDalHandler);

        public void AttachLegDalHandlerToEventType(DalTopic dalTopic, ILegDalEventHandler legDalEventHandler)
            => _dalSubject.AttachLegDalHandlerToEventType(dalTopic, legDalEventHandler);

        public void DetachLegDalHandlerFromEventType(DalTopic dalTopic, ILegDalEventHandler legDalEventHandler) =>
            _dalSubject.DetachLegDalHandlerFromEventType(dalTopic, legDalEventHandler);
        public void AttachFlightLegDalHandlerToEventType(DalTopic dalTopic, IFlightLegDalEventHandler flightLegDalEventHandler)
            => _dalSubject.AttachFlightLegDalHandlerToEventType(dalTopic, flightLegDalEventHandler);

        public void DetachFlightLegDalHandlerFromEventType(DalTopic dalTopic, IFlightLegDalEventHandler flightLegDalEventHandler)
            => _dalSubject.DetachFlightLegDalHandlerFromEventType(dalTopic, flightLegDalEventHandler);
        public void NotifyIncomingFlight(Flight incomingFlight) => _dalSubject.NotifyIncomingFlight(incomingFlight);
        public async Task NotifyFlightToDalAsync(DalTopic topic, Flight flight) =>
            await _dalSubject.NotifyFlightToDalAsync(topic, flight);
        public async Task NotifyLegToDalAsync(DalTopic topic, Leg leg) =>
            await _dalSubject.NotifyLegToDalAsync(topic, leg);
        public void NotifyFlightEnteredLeg(Flight flight) =>
            _eventHandlerSubject.NotifyFlightEnteredLeg(flight);
        public void NotifyFlightFinishedLeg(Flight flight) => _dalSubject.NotifyFlightFinishedLeg(flight);
        public void AttatchFlightToLegQueue(Flight flight, Leg leg) => _dalSubject.AttatchFlightToLegQueue(flight, leg);
        public void NotifyFlightNextLegClear(Flight flight, Leg leg) => _dalSubject.NotifyFlightNextLegClear(flight, leg);
        public void NotifyLegHasBeenCleared(Leg leg) => _dalSubject.NotifyLegHasBeenCleared(leg);
        public void NotifyFlightCompleted(Flight flight) => _dalSubject.NotifyFlightCompleted(flight);
        public void NotifyFlightOutOfTerminal(Flight flight) => _eventHandlerSubject.NotifyFlightOutOfTerminal(flight);
        public void Detach(Flight flight, Leg leg) => _dalSubject.Detach(flight, leg);
        //public void AddingFlight(Flight flight) => _subscribeSubject.AddingFlight(flight);
    }
}
