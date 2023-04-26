using Core.DTOs.Incoming;
using Core.Entities;
using Core.Entities.Terminal;
using Core.EventHandlers.Enums;
using Core.EventHandlers.Interfaces;
using Core.EventHandlers.Interfaces.DAL;
using Core.Interfaces.Subject;

namespace Airport.Application.Events
{
    public class SSubject : IISUbject
    {
        private Dictionary<FlightTopic, List<INotify>> _topicToHandlers = new Dictionary<FlightTopic, List<INotify>>();
        //private Dictionary<Topic, BaseAirportEvent> topicToEventType = new Dictionary<Topic, BaseAirportEvent>();
        private Dictionary<FlightTopic, List<IBaseAirportHandler>> _topicToEventType = new Dictionary<FlightTopic, List<IBaseAirportHandler>>();
        private Dictionary<DalTopic, List<IDalHandler<BaseEntity>>> _topicToDalType = new Dictionary<DalTopic, List<IDalHandler<BaseEntity>>>();

        public async Task FlightToDalAsync(DalTopic topic, Flight flight)
        {
            var eventHandlers = _topicToDalType[topic];
            foreach (var eventHandler in eventHandlers)
                await eventHandler.UpdateAsync(flight);
        }
        public async Task LegToDalAsync(DalTopic topic, Leg leg)
        {
            var eventHandlers = _topicToDalType[topic];
            foreach (var eventHandler in eventHandlers)
                await eventHandler.UpdateAsync(leg);
        }
        public Task AddObjAsync(FlightTopic topic, object T)
        {

            throw new NotImplementedException();
        }

        // The subscription management methods.
        public void AttachToEventType(FlightTopic topic, IBaseAirportHandler observer)
        {
            var KV = _topicToEventType.FirstOrDefault(KV => KV.Key == topic);
            KV.Value.Add(observer);
            throw new NotImplementedException();
        }
        public void DetachFromEventType(FlightTopic topic, IBaseAirportHandler observer)
        {
            var KV = _topicToEventType.FirstOrDefault(KV => KV.Key == topic);
            KV.Value.Remove(observer);
            Console.WriteLine("Subject: Detached an observer.");
        }
        public void NotifyAdding(Flight flight)
        {

        }
        public void NotifyFlightFinished(Flight flight)
        {
            throw new NotImplementedException();
        }

        public void NotifyFlightLeftLeg(Flight flight)
        {
            throw new NotImplementedException();
        }


        public void NotifyInComingFlight(Flight flight)
        {
            var incomingFlightHandlers = _topicToEventType[FlightTopic.FlightInComing];
            foreach (var handler in incomingFlightHandlers)
                handler.Update(flight);
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
