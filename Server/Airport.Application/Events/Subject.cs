using Core.Entities.Terminal;
using Core.Interfaces.Events;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airport.Application.Events
{
    //enum Topic
    //{
    //    FlightIncoming,
    //}

    //public class BaseAirportEvent { }

    //public class FlightIncomingEvent : BaseAirportEvent
    //{
    //    public string FlightId { get; set; }
    //}
    //configure handlers in program
    public class Subject : ISubject
    {
        //private Dictionary<Topic, List<INotify>> topicToHandlers = new Dictionary<string, List<INotify>>();
        //private Dictionary<Topic, BaseAirportEvent> topicToEventType = new Dictionary<Topic, BaseAirportEvent>();

        private Dictionary<IObserver, Leg> _legWaitingMap = new Dictionary<IObserver, Leg>();
        private Dictionary<Leg, Queue<Flight>> _legQueueMap = new Dictionary<Leg, Queue<Flight>>();

        //public void listen(string topic, INotify notifier)
        //{
        //    topicToHandlers.Add(topic, notifier);
        //}

        //public void publish(string topic)
        //{
        //    topicToHandlers.TryGetValue(topic)
        //        foreach ()
        //        NotifyAsync()
        //}

        public void Attach(Leg leg, Flight flight)
        {
            if (!_legQueueMap.ContainsKey(leg))
            {
                _legQueueMap[leg] = new Queue<Flight>();
            }
            //_legWaitingMap.Add(flight.Leg, leg);
            _legQueueMap[leg].Enqueue(flight);
        }


        public void Detach(Leg leg, Flight flight)
        {
            if(_legQueueMap.ContainsKey(leg))
                _legQueueMap[leg].Dequeue();
        }


        // Trigger an update in each subscriber.
        public async Task NotifyAsync(Leg leg)
        {
            if (_legQueueMap.ContainsKey(leg) && _legQueueMap[leg].Count > 0)
            {
                Flight flightToContinue = _legQueueMap[leg].Dequeue();
                 await flightToContinue.Leg.UpdateAsync(leg);
            }
        }
    }
}
