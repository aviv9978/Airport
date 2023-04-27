using Core.DTOs.Incoming;
using Core.Entities;
using Core.Entities.Terminal;
using Core.EventHandlers.Enums;
using Core.EventHandlers.Interfaces;
using Core.EventHandlers.Interfaces.DAL;
using Core.EventHandlers.Interfaces.FlightInterfaces;
using Core.Interfaces.Subject;

namespace Airport.Application.Events
{
    public class SSubject : IISUbject
    {
        private Dictionary<FlightTopic, List<INotify>> _topicToHandlers = new Dictionary<FlightTopic, List<INotify>>();
        //private Dictionary<Topic, BaseAirportEvent> topicToEventType = new Dictionary<Topic, BaseAirportEvent>();
        private Dictionary<FlightTopic, List<IFlightBasicEventHandler>> _topicToFlightHandlers = new Dictionary<FlightTopic, List<IFlightBasicEventHandler>>();
        private Dictionary<DalTopic, List<IDalBasicEventHandler<BaseEntity>>> _topicToDalHandlers = new Dictionary<DalTopic, List<IDalBasicEventHandler<BaseEntity>>>();
        private static Dictionary<Leg, Queue<Flight>> _legQueueMap = new Dictionary<Leg, Queue<Flight>>();

        public void AttachFlightHandlerToEventType(FlightTopic topic, IFlightBasicEventHandler observer)
        {
            var KV = _topicToFlightHandlers.FirstOrDefault(KV => KV.Key == topic);
            KV.Value.Add(observer);
            throw new NotImplementedException();
        }
        public void DetachFlightHandlerFromEventType(FlightTopic topic, IFlightBasicEventHandler observer)
        {
            var KV = _topicToFlightHandlers.FirstOrDefault(KV => KV.Key == topic);
            KV.Value.Remove(observer);
            Console.WriteLine("Subject: Detached an observer.");
        }
        public void AttachDalHandlerToEventType(DalTopic dalTopic, IDalBasicEventHandler<BaseEntity> observer)
        {
            var KV = _topicToDalHandlers.FirstOrDefault(KV => KV.Key == dalTopic);
            KV.Value.Add(observer);
            throw new NotImplementedException();
        }
        public void DetachDalHandlerFromEventType(DalTopic dalTopic, IDalBasicEventHandler<BaseEntity> observer)
        {
            var KV = _topicToDalHandlers.FirstOrDefault(KV => KV.Key == dalTopic);
            KV.Value.Remove(observer);
            Console.WriteLine("Subject: Detached an observer.");
        }
        public void NotifyIncomingFlight(Flight incomingFlight)
        {
            var incomingFlightHandlers = _topicToDalHandlers[DalTopic.FlightInComing];
            foreach (var handler in incomingFlightHandlers)
                handler.NotifyAsync(incomingFlight);
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
            var eventHandlers = _topicToDalHandlers[DalTopic.FlightFinishedLeg];
            foreach (var eventHandler in eventHandlers)
                eventHandler.NotifyAsync(flight);
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

                foreach (var kvp in _legQueueMap)
                {
                    if (kvp.Key == leg) continue; 

                    Queue<Flight> otherLegQueue = kvp.Value;
                    int count = otherLegQueue.Count;
                    while (count > 0)
                    {
                        Flight otherFlight = otherLegQueue.Dequeue();
                        if (!otherFlight.Equals(flightToContinue))
                        {
                            otherLegQueue.Enqueue(otherFlight);
                        }
                        count--;
                    }
                }
            }

        }
        public void NotifyFlightCompleted(Flight flight)
        {
            var eventHandlers = _topicToDalHandlers[DalTopic.FlightCompleted];
            foreach (var eventHandler in eventHandlers)
                eventHandler.NotifyAsync(flight);
        }
        public void NotifyFlightOutOfTerminal(Flight flight)
        {
            var eventHandlers = _topicToFlightHandlers[FlightTopic.FlightOutOfTereminal];
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
