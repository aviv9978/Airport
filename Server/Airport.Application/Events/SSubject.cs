using Core.DTOs.Incoming;
using Core.Entities;
using Core.Entities.Terminal;
using Core.EventHandlers.Enums;
using Core.EventHandlers.Interfaces;
using Core.EventHandlers.Interfaces.DAL;
using Core.EventHandlers.Interfaces.Flight;
using Core.EventHandlers.Interfaces.FlightInterfaces;
using Core.Interfaces.Subject;

namespace Airport.Application.Events
{
    public class SSubject : IISUbject
    {
        private Dictionary<FlightTopic, List<INotify>> _topicToHandlers = new Dictionary<FlightTopic, List<INotify>>();
        //private Dictionary<Topic, BaseAirportEvent> topicToEventType = new Dictionary<Topic, BaseAirportEvent>();
        private Dictionary<FlightTopic, List<IFlightBasicHandler>> _topicToFlightHandlers = new Dictionary<FlightTopic, List<IFlightBasicHandler>>();
        private Dictionary<DalTopic, List<IDalBasicHandler<BaseEntity>>> _topicToDalHandlers = new Dictionary<DalTopic, List<IDalBasicHandler<BaseEntity>>>();
        private static Dictionary<Leg, Queue<Flight>> _legQueueMap = new Dictionary<Leg, Queue<Flight>>();

        public void AttachFlightHandlerToEventType(FlightTopic topic, IFlightBasicHandler observer)
        {
            var KV = _topicToFlightHandlers.FirstOrDefault(KV => KV.Key == topic);
            KV.Value.Add(observer);
            throw new NotImplementedException();
        }
        public void DetachFlightHandlerFromEventType(FlightTopic topic, IFlightBasicHandler observer)
        {
            var KV = _topicToFlightHandlers.FirstOrDefault(KV => KV.Key == topic);
            KV.Value.Remove(observer);
            Console.WriteLine("Subject: Detached an observer.");
        }
        public void AttachDalHandlerToEventType(DalTopic dalTopic, IDalBasicHandler<BaseEntity> observer)
        {
            var KV = _topicToDalHandlers.FirstOrDefault(KV => KV.Key == dalTopic);
            KV.Value.Add(observer);
            throw new NotImplementedException();
        }
        public void DetachDalHandlerFromEventType(DalTopic dalTopic, IDalBasicHandler<BaseEntity> observer)
        {
            var KV = _topicToDalHandlers.FirstOrDefault(KV => KV.Key == dalTopic);
            KV.Value.Remove(observer);
            Console.WriteLine("Subject: Detached an observer.");
        }

        public void NotifyIncomingFlight(Flight incomingFlight)
        {
            var incomingFlightHandlers = _topicToFlightHandlers[FlightTopic.FlightInComing];
            foreach (var handler in incomingFlightHandlers)
                handler.Notify(incomingFlight);
        }
        public void NotifyFlightNextLegClear(Flight flight, Leg leg)
        {
            var eventHandlers = _topicToDalHandlers[DalTopic.FlightNextLegClear];
            var FlightLeg = new FlightLeg(flight, leg);
            foreach (var eventHandler in eventHandlers)
            {
                eventHandler.NotifyAsync(FlightLeg);
            }
        }
        public async Task NotifyFlightToDalAsync(DalTopic topic, Flight flight)
        {
            var eventHandlers = _topicToDalHandlers[topic];
            foreach (var eventHandler in eventHandlers)
                await eventHandler.NotifyAsync(flight);
        }
        public async Task NotifyLegToDalAsync(DalTopic topic, Leg leg)
        {
            var eventHandlers = _topicToDalHandlers[topic];
            foreach (var eventHandler in eventHandlers)
                await eventHandler.NotifyAsync(leg);
        }
        public void NotifyFlightEnteredLeg(Flight flight)
        {
            var eventHandlers = _topicToFlightHandlers[FlightTopic.FlightEnteredLeg];
            foreach (var eventHandler in eventHandlers)
                eventHandler.Notify(flight);
        }
        public void NotifyFlightFinishedLeg(Flight flight)
        {
            var eventHandlers = _topicToFlightHandlers[FlightTopic.FlightFinishedLeg];
            foreach (var eventHandler in eventHandlers)
                eventHandler.Notify(flight);
        }
        public void AttatchFlightToLegQueue(Flight flight, Leg leg)
        {
            if (!_legQueueMap.ContainsKey(leg))
            {
                _legQueueMap[leg] = new Queue<Flight>();
            }
            _legQueueMap[leg].Enqueue(flight);
        }
        public void NotifyLegClear(Leg leg)
        {
            if (_legQueueMap.ContainsKey(leg) && _legQueueMap[leg].Count > 0)
            {
                Flight flightToContinue = _legQueueMap[leg].Dequeue();
                NotifyFlightNextLegClear(flightToContinue, leg);
            }
        }
        public void NotifyFlightCompleted(Flight flight)
        {
            var eventHandlers = _topicToFlightHandlers[FlightTopic.FlightCompleted];
            foreach (var eventHandler in eventHandlers)
                eventHandler.Notify(flight);
        }
        public void Detach(Flight flight, Leg leg)
        {
            if (_legQueueMap.ContainsKey(leg))
                _legQueueMap[leg].Dequeue();
        }

    }
}
