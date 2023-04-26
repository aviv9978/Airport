using Core.DTOs.Incoming;
using Core.Entities.Terminal;
using Core.EventHandlers.Enums;
using Core.EventHandlers.Interfaces;
using Core.EventHandlers.Interfaces.DAL;
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
        void AttachToEventType(FlightTopic topic, IBaseAirportHandler observer);
        void DetachFromEventType(FlightTopic topic, IBaseAirportHandler observer);
        Task AddObjAsync(FlightTopic topic, object T);
        Task FlightToDalAsync(DalTopic topic, Flight flight);
        Task LegToDalAsync(DalTopic topic, Leg flight);
        void NotifyInComingFlight(Flight flight);
        void NotifyFlightLeftLeg(Flight flight);
        void NotifyFlightFinished(Flight flight);
    }
}
