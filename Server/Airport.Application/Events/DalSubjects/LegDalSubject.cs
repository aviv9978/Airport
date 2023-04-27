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
    public class LegDalSubject : ILegDalSubject
    {
        private Dictionary<DalTopic, List<ILegDalEventHandler>> _topicToLegDalHandler =
            new Dictionary<DalTopic, List<ILegDalEventHandler>>();

        private readonly IFlightLegDalSubject _iFlightLegDalSub;

        public LegDalSubject(IFlightLegDalSubject iFlightLegDalSub)
        {
            _iFlightLegDalSub = iFlightLegDalSub;
        }
        public async Task NotifyLegToDalAsync(DalTopic topic, Leg leg)
        {
            var eventHandlers = _topicToLegDalHandler[topic];
            foreach (var eventHandler in eventHandlers)
                await eventHandler.NotifyAsync(leg);
        }
    }
}
