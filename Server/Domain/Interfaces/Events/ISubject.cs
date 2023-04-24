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
        void Attach(Leg leg, Flight flight);

        // Detach an observer from the subject.
        void Detach(Leg leg, Flight flight);

        // Notify all observers about an event.
        void Notify(Leg leg);
    }
}
