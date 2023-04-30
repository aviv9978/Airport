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
    public interface ILegDalSubject
    {
        void AttachLegDalHandlerToEventType(DalTopic dalTopic, ILegDalEventHandler legDalEventHandler);
        void DetachLegDalHandlerFromEventType(DalTopic dalTopic, ILegDalEventHandler legDalEventHandler);
        Task NotifyLegToDalAsync(DalTopic topic, Leg leg);
    }
}
