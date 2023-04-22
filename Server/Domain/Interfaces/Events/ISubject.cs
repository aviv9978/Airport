using Core.Entities.Terminal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces.Events
{
    public interface ISubject
    {
        // Attach an observer to the subject.
        void Attach(IObserver observer,Leg leg);

        // Detach an observer from the subject.
        void Detach(IObserver observer);

        // Notify all observers about an event.
        void Notify(Leg leg);
    }
}
