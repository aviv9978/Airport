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
        public void NotifyFlightNextLegClear(Flight flight, Leg leg)
        {
            var eventHandlers = _topicToFlightLegDalHandler[DalTopic.FlightNextLegClear];
            var FlightLeg = new FlightLeg(flight, leg);
            foreach (var eventHandler in eventHandlers)
                eventHandler.NotifyAsync(FlightLeg);
        }
    }
}
