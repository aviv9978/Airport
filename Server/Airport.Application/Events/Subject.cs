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
                //var waitingLeg = _legWaitingMap.FirstOrDefault(x => x.Value == leg);
                //foreach (var item in _legWaitingMap.Where(kv => kv.Key == waitingLeg.Key))
                //    _legWaitingMap.Remove(item.Key);
                 await flightToContinue.Leg.UpdateAsync(leg);
                //waitingLeg.Key.Update();
            }

        }
    }
}
