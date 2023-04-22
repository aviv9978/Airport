using Core.Entities.Terminal;
using Core.Interfaces.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airport.Application.Events
{
    public class Subject : ISubject
    {
        // List of subscribers. In real life, the list of subscribers can be
        // stored more comprehensively (categorized by event type, etc.).
        //private List<IObserver> _observers = new List<IObserver>();
        private Dictionary<IObserver, Leg> _legWaitingMap= new Dictionary<IObserver, Leg>();
        private Dictionary<Leg, Queue<Flight>> _legQueueMap = new Dictionary<Leg, Queue<Flight>>();
        // The subscription management methods.
        public void Attach(IObserver observer, Leg leg)
        {
            Console.WriteLine("Subject: Attached an observer.");
            this._legWaitingMap.Add(observer, leg);
        }

        public void Detach(IObserver observer)
        {
            this._legWaitingMap.Remove(observer);
            Console.WriteLine("Subject: Detached an observer.");
        }

        // Trigger an update in each subscriber.
        public void Notify(Leg leg)
        {
            Console.WriteLine("Subject: Notifying observers...");
            if (_legQueueMap.ContainsKey(leg) && _legQueueMap[leg].Count > 0)
            {
                Flight nextFlight = _legQueueMap[leg].Dequeue();
               var waitingLeg = _legWaitingMap.FirstOrDefault(x => x.Value == leg);
                waitingLeg.Key.Update();
            }

        }
        // Usually, the subscription logic is only a fraction of what a Subject
        // can really do. Subjects commonly hold some important business logic,
        // that triggers a notification method whenever something important is
        // about to happen (or after it).
    }
}
