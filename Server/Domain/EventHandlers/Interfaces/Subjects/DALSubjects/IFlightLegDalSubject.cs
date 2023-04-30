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
    public interface IFlightLegDalSubject
    {
        void AttachFlightLegDalHandlerToEventType(DalTopic dalTopic, IFlightLegDalEventHandler flightLegDalEventHandler);
        void DetachFlightLegDalHandlerFromEventType(DalTopic dalTopic, IFlightLegDalEventHandler flightLegDalEventHandler);
        void NotifyFlightNextLegClear(Flight flight, Leg leg);
    }
}
