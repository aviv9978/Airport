using Core.DTOs.Incoming;
using Core.Entities.Terminal;
using Core.EventHandlers.Enums;
using Core.Interfaces.EventHandlers;
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
        // Attach an observer to the subject.
        void AttachToTopic(Topic topic,INotify observer);
        void AttachDalHandlerToTopic(Topic topic,IDalHandler observer);
        // Detach an observer from the subject.
        void DetachFromTopic(Topic topic, INotify observer);
        void DetachDalHandlerFromTopic(Topic topic, IDalHandler observer);

        // Notify all observers about an event.
        //void Notify();
        void NotifyInComingFlight(FlightInDTO flightInDto);
        void NotifyFlightLeftLeg(Flight flight);
        void NotifyFlightFinished(Flight flight);
    }
}
