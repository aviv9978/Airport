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
    public class FlightLegDalSubject : IFlightLegDalSubject
    {
        private Dictionary<DalTopic, List<IFlightLegDalEventHandler>> _topicToFlightLegDalHandler =
            new Dictionary<DalTopic, List<IFlightLegDalEventHandler>>();

        public void AttachFlightLegDalHandlerToEventType(DalTopic dalTopic, IFlightLegDalEventHandler flightLegDalEventHandler)
        {
            List<IFlightLegDalEventHandler>? listFlightLegDalHandlers = null;
            if (!_topicToFlightLegDalHandler.TryGetValue(dalTopic, out listFlightLegDalHandlers))
                listFlightLegDalHandlers = _topicToFlightLegDalHandler[dalTopic] = new List<IFlightLegDalEventHandler>();

            listFlightLegDalHandlers.Add(flightLegDalEventHandler);
        }

        public void DetachFlightLegDalHandlerFromEventType(DalTopic dalTopic, IFlightLegDalEventHandler flightLegDalEventHandler)
        {
            var KV = _topicToFlightLegDalHandler.FirstOrDefault(KV => KV.Key == dalTopic);
            KV.Value.Remove(flightLegDalEventHandler);
        }

        public void NotifyFlightNextLegClear(Flight flight, Leg leg)
        {
            var eventHandlers = _topicToFlightLegDalHandler[DalTopic.FlightNextLegClear];
            var FlightLeg = new FlightAndNextLeg(flight, leg);
            foreach (var eventHandler in eventHandlers)
                eventHandler.NotifyAsync(FlightLeg);
        }
    }
}
