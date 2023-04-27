using Core.Entities;
using Core.EventHandlers.Enums;
using Core.EventHandlers.Interfaces.DAL;
using Core.EventHandlers.Interfaces.FlightInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.ApiHandlers
{
    public interface IFlightControllerHandler
    {
        void SubscribeToFlightBasicEventHandler(IFlightBasicEventHandler flightDalHandler, FlightTopic flightTopic);
        void SubscribeToLegDalHandler(ILegDalEventHandler flightDalHandler, DalTopic dalTopic);
        void SubscribeToBasicDalHandler(IDalBasicEventHandler<BaseEntity> flightDalHandler, DalTopic dalTopic);


    }
}
