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
        private Dictionary<IObserver, Leg> _legWaitingMap = new Dictionary<IObserver, Leg>();
        private Dictionary<Leg, Queue<Flight>> _legQueueMap = new Dictionary<Leg, Queue<Flight>>();
        public void Attach(IObserver observer, Leg leg) => _legWaitingMap.Add(observer, leg);


        public void Detach(IObserver observer) => _legWaitingMap.Remove(observer);


        // Trigger an update in each subscriber.
        public void Notify(Leg leg)
        {
            if (_legQueueMap.ContainsKey(leg) && _legQueueMap[leg].Count > 0)
            {
                Flight nextFlight = _legQueueMap[leg].Dequeue();
                var waitingLeg = _legWaitingMap.FirstOrDefault(x => x.Value == leg);
                foreach (var item in _legWaitingMap.Where(kv => kv.Key == waitingLeg.Key))
                    _legWaitingMap.Remove(item.Key);

                waitingLeg.Key.Update();
            }

        }
    }
}
