using Airport.Infrastracture.Handlers.FlightHandlers;
using Core.ApiHandlers;
using Core.Entities;
using Core.EventHandlers.Enums;
using Core.EventHandlers.Interfaces.DAL;
using Core.EventHandlers.Interfaces.FlightInterfaces;
using Core.Interfaces.Subject;

namespace Airport.Handlers
{
    public class FlightControllerHandler : IFlightControllerHandler
    {
        private readonly IISUbject _subject;

        public FlightControllerHandler(IISUbject subject, IFlightDalEventHandler addFlightHandler)
        {
            _subject = subject;
            _subject.AttachDalHandlerToEventType(DalTopic.AddFlight, addFlightHandler);
        }

        public void SubscribeToBasicDalHandler(IDalBasicEventHandler<BaseEntity> flightDalHandler, DalTopic dalTopic)
        {
            throw new NotImplementedException();
        }

        public void SubscribeToFlightBasicEventHandler(IFlightBasicEventHandler flightDalHandler, FlightTopic flightTopic)
        {
            throw new NotImplementedException();
        }
    }
}
