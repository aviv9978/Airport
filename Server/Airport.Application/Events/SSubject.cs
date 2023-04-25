using Airport.Application.EventHandlers;
using Core.DTOs.Incoming;
using Core.Entities.Terminal;
using Core.EventHandlers.Enums;
using Core.Interfaces.EventHandlers;
using Core.Interfaces.Events;
using Core.Interfaces.Subject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airport.Application.Events
{
    public class SSubject : IISUbject
    {
        // For the sake of simplicity, the Subject's state, essential to all
        // subscribers, is stored in this variable.
        public int State { get; set; } = -0;

        private Dictionary<Topic, List<INotify>> _topicToHandlers = new Dictionary<Topic, List<INotify>>();
        //private Dictionary<Topic, BaseAirportEvent> topicToEventType = new Dictionary<Topic, BaseAirportEvent>();
        private Dictionary<Topic, List<IBaseAirportHandler>> _topicToEventType = new Dictionary<Topic, List<IBaseAirportHandler>>();
        private Dictionary<Topic, List<IDalHandler>> _topicToDalType = new Dictionary<Topic, List<IDalHandler>>();

        // The subscription management methods.
        public void AttachToTopic(Topic topic, INotify observer)
        {
            var KV = _topicToHandlers.FirstOrDefault(KV => KV.Key == topic);
            KV.Value.Add(observer);
            throw new NotImplementedException();
        }
        public void DetachFromTopic(Topic topic, INotify observer)
        {
            var KV = _topicToHandlers.FirstOrDefault(KV => KV.Key == topic);
            KV.Value.Remove(observer);
            Console.WriteLine("Subject: Detached an observer.");
        }
        public void AttachDalHandlerToTopic(Topic topic, IDalHandler dalObserver)
        {
            var KV = _topicToDalType.FirstOrDefault(KV => KV.Key == topic);
            KV.Value.Add(dalObserver);
        }
        public void DetachDalHandlerFromTopic(Topic topic, IDalHandler dalObserver)
        {
            var KV = _topicToDalType.FirstOrDefault(KV => KV.Key == topic);
            KV.Value.Remove(dalObserver);
        }
        public void NotifyFlightFinished(Flight flight)
        {
            throw new NotImplementedException();
        }

        public void NotifyFlightLeftLeg(Flight flight)
        {
            throw new NotImplementedException();
        }

        public void NotifyInComingFlight(FlightInDTO flightInDto)
        {
            throw new NotImplementedException();
        }

        // Trigger an update in each subscriber.
        //public void Notify()
        //{
        //    Console.WriteLine("Subject: Notifying observers...");

        //    foreach (var observer in _observers)
        //    {
        //        observer.Update(this);
        //    }
        //}


    }
}
