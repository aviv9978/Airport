using Core.DTOs.Incoming;
using Core.Entities;
using Core.Entities.Terminal;
using Core.EventHandlers.Enums;
using Core.EventHandlers.Interfaces;
using Core.EventHandlers.Interfaces.DAL;
using Core.EventHandlers.Interfaces.FlightInterfaces;
using Core.EventHandlers.Interfaces.Subjects.DAL;
using Core.EventHandlers.Interfaces.Subjects.EventHandlersSubjects;
using Core.EventHandlers.Interfaces.Subjects.Subscribers;
using Core.Interfaces.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces.Subject
{
    public interface IISUbject : IDalSubject, IEventHandlerSubject//, ISubscribeSubject
    {
    }
}
