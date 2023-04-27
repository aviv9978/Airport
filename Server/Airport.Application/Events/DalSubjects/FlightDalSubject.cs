using Core.Entities.Terminal;
using Core.EventHandlers.Enums;
using Core.EventHandlers.Interfaces.DAL;
using Core.EventHandlers.Interfaces.Subjects.DAL;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airport.Application.Events.DalSubjects
{
    public class FlightDalSubject : IFlightDalSubject
    {
        private Dictionary<DalTopic, List<IFlightDalEventHandler>> _topicToFlightDalHandler =
            new Dictionary<DalTopic, List<IFlightDalEventHandler>>();

        private static Dictionary<Leg, Queue<Flight>> _legQueueMap = new Dictionary<Leg, Queue<Flight>>();

        private readonly IFlightLegDalSubject _iFlightLegDalSub;

        public FlightDalSubject(IFlightLegDalSubject iFlightLegDalSub)
        {
            _iFlightLegDalSub = iFlightLegDalSub;
        }
        public void AttachFlightDalHandlerToEventType(DalTopic dalTopic, IFlightDalEventHandler flightLegDalHandler)
        {
            List<IFlightDalEventHandler> listFlighLegtDalHandlers = null;
            if (!_topicToFlightDalHandler.TryGetValue(dalTopic, out listFlighLegtDalHandlers))
                listFlighLegtDalHandlers = _topicToFlightDalHandler[dalTopic] = new List<IFlightDalEventHandler>();

            listFlighLegtDalHandlers.Add(flightLegDalHandler);

        }
        public void DetachFlightDalHandlerFromEventType(DalTopic dalTopic, IFlightDalEventHandler observer)
        {
            var KV = _topicToFlightDalHandler.FirstOrDefault(KV => KV.Key == dalTopic);
            KV.Value.Remove(observer);
            Console.WriteLine("Subject: Detached an observer.");
        }
        public void NotifyIncomingFlight(Flight incomingFlight)
        {
            var eventHandlers = _topicToFlightDalHandler[DalTopic.FlightInComing];
            foreach (var handler in eventHandlers)
            {
                handler.NotifyAsync(incomingFlight);
            }
        }
        public async Task NotifyFlightToDalAsync(DalTopic topic, Flight flight)
        {
            var eventHandlers = _topicToFlightDalHandler[topic];
            foreach (var eventHandler in eventHandlers)
                await eventHandler.NotifyAsync(flight);
        }

        public void NotifyFlightFinishedLeg(Flight flight)
        {
            var eventHandlers = _topicToFlightDalHandler[DalTopic.FlightFinishedLeg];
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
        public void NotifyLegHasBeenCleared(Leg leg)
        {
            if (_legQueueMap.ContainsKey(leg) && _legQueueMap[leg].Count > 0)
            {
                Flight flightToContinue = _legQueueMap[leg].Dequeue();
                _iFlightLegDalSub.NotifyFlightNextLegClear(flightToContinue, leg);

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
            var eventHandlers = _topicToFlightDalHandler[DalTopic.FlightCompleted];
            foreach (var eventHandler in eventHandlers)
                eventHandler.NotifyAsync(flight);
        }
        public void Detach(Flight flight, Leg leg)
        {
            if (_legQueueMap.ContainsKey(leg))
                _legQueueMap[leg].Dequeue();
        }

    }
}

