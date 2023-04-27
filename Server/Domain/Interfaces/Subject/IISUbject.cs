using Core.DTOs.Incoming;
using Core.Entities;
using Core.Entities.Terminal;
using Core.EventHandlers.Enums;
using Core.EventHandlers.Interfaces;
using Core.EventHandlers.Interfaces.DAL;
using Core.EventHandlers.Interfaces.FlightInterfaces;
using Core.Interfaces.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces.Subject
{
    public interface IISUbject
    {
        void AttachFlightHandlerToEventType(FlightTopic flightTopic, IFlightBasicEventHandler observer);
        void DetachFlightHandlerFromEventType(FlightTopic flightTopic, IFlightBasicEventHandler observer);
        void AttachDalHandlerToEventType(DalTopic dalTopic, IDalBasicEventHandler<BaseEntity> observer);
        void DetachDalHandlerFromEventType(DalTopic dalTopic, IDalBasicEventHandler<BaseEntity> observer);
        Task NotifyFlightToDalAsync(DalTopic dalTopic, Flight flight);
        Task NotifyLegToDalAsync(DalTopic dalTopic, Leg flight);
        void NotifyFlightNextLegClear(Flight flight, Leg leg);
        void NotifyIncomingFlight(Flight flight);
        void NotifyFlightEnteredLeg(Flight flight);
        void NotifyFlightFinishedLeg(Flight flight);
        void AttatchFlightToLegQueue(Flight flight, Leg leg);
        void NotifyLegClear(Leg leg);
        void NotifyFlightCompleted(Flight flight);
        void NotifyFlightOutOfTerminal(Flight flight);
        void Detach(Flight flight, Leg leg);
    }
}
