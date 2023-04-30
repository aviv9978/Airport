using Core.Entities.Terminal;
using Core.EventHandlers.Enums;
using Core.EventHandlers.Interfaces;
using Core.EventHandlers.Interfaces.Subjects.Subscribers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airport.Application.Subjects.SubscribeSubject
{
    public class SubscriberSubject : ISubscribeSubject
    {
        private readonly ISubscribeToHandlers _subscribeToHandlers;
        public SubscriberSubject(ISubscribeToHandlers subscribeToHandlers)
        {
            _subscribeToHandlers = subscribeToHandlers;
        }
        public void AddingFlight( Flight flight) => _subscribeToHandlers.AddingFlight(flight);


    }
}
