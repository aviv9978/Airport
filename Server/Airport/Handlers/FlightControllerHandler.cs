using Airport.Infrastracture.Handlers.FlightHandlers;
using Core.ApiHandlers;
using Core.Entities;
using Core.Entities.Terminal;
using Core.EventHandlers.Enums;
using Core.EventHandlers.Interfaces.DAL;
using Core.EventHandlers.Interfaces.FlightInterfaces;
using Core.Interfaces;
using Core.Interfaces.Subject;

namespace Airport.Handlers
{
    public class FlightControllerHandler : IFlightControllerHandler
    {
        private readonly IEnumerable<IFlightDalEventHandler> _flightDalEventHandlers;
        private readonly IFlightLegDalEventHandler _flightLegDalEventHandler;
        private readonly ILegDalEventHandler _legDalEventHandler;
        private readonly IFlightBasicEventHandler _flightBasicEventHandler;
        private readonly IISUbject _subject;

        public FlightControllerHandler(IISUbject subject,
            IEnumerable<IFlightDalEventHandler> flightDalEventHandlers,
            IFlightLegDalEventHandler flightLegDalEventHandler,
            ILegDalEventHandler legDalEventHandler,
            IFlightBasicEventHandler flightBasicEventHandler)
        {
            _flightDalEventHandlers = flightDalEventHandlers;
            _flightLegDalEventHandler = flightLegDalEventHandler;
            _legDalEventHandler = legDalEventHandler;
            _flightBasicEventHandler = flightBasicEventHandler;
            _subject = subject;
            SubscribeToBasicDalHandler();
            SubscribeToFlightBasicEventHandler();
        }

        public async Task AddFlightAsync(Flight flight)
        {
            await _subject.NotifyFlightToDalAsync(DalTopic.AddFlight, flight);
        }

        private void SubscribeToBasicDalHandler()
        {
            foreach (var flightDalEventHandler in _flightDalEventHandlers)
            {
                _subject.AttachDalHandlerToEventType(flightDalEventHandler.DalTopic, flightDalEventHandler);
            }
            _subject.AttachDalHandlerToEventType(_flightLegDalEventHandler.DalTopic, _flightLegDalEventHandler);
            _subject.AttachDalHandlerToEventType(_legDalEventHandler.DalTopic, _legDalEventHandler);
        }

        private void SubscribeToFlightBasicEventHandler()
        {
            _subject.AttachFlightHandlerToEventType(_flightBasicEventHandler.FlightTopic, _flightBasicEventHandler);
        }
    }
}
