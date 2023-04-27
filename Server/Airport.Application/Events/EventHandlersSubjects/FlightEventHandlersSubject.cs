using Core.Entities.Terminal;
using Core.EventHandlers.Enums;
using Core.EventHandlers.Interfaces.DAL;
using Core.EventHandlers.Interfaces.FlightInterfaces;
using Core.EventHandlers.Interfaces.Subjects.EventHandlersSubjects;
using Core.Interfaces.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airport.Application.Events.EventHandlersSubjects
{
    public class FlightEventHandlersSubject : IFlightEventHandlerSubject
    {
        private Dictionary<FlightTopic, List<IFlightBasicEventHandler>> _topicToFlightHandlers = new Dictionary<FlightTopic, List<IFlightBasicEventHandler>>();

        public void AttachFlightHandlerToEventType(FlightTopic flightTopic, IFlightBasicEventHandler flightBasicEventHandler)
        {
            List<IFlightBasicEventHandler>? flightEventHandlerList = null;
            if (!_topicToFlightHandlers.TryGetValue(flightTopic, out flightEventHandlerList))
                flightEventHandlerList = _topicToFlightHandlers[flightTopic] = new List<IFlightBasicEventHandler>();

            flightEventHandlerList.Add(flightBasicEventHandler);
        }

        public void DetachFlightHandlerFromEventType(FlightTopic flightTopic, IFlightBasicEventHandler handler)
        {
            var KV = _topicToFlightHandlers.FirstOrDefault(KV => KV.Key == flightTopic);
            KV.Value.Remove(handler);
        }

        public void NotifyFlightEnteredLeg(Flight flight)
        {
            var eventHandlers = _topicToFlightHandlers[FlightTopic.FlightEnteredLeg];
            foreach (var eventHandler in eventHandlers)
                eventHandler.Notify(flight);
        }
        public void NotifyFlightOutOfTerminal(Flight flight)
        {
            var eventHandlers = _topicToFlightHandlers[FlightTopic.FlightOutOfTereminal];
            foreach (var eventHandler in eventHandlers)
                eventHandler.Notify(flight);
        }
    }
}
