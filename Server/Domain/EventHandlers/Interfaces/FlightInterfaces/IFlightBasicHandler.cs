using Core.Entities.Terminal;
using Core.EventHandlers.Enums;

namespace Core.EventHandlers.Interfaces.FlightInterfaces
{
    public interface IFlightBasicEventHandler
    {
        public FlightTopic FlightTopic{ get; set; }

        Task Notify(Flight flight);
    }
}
