using Core.Entities.Terminal;

namespace Core.EventHandlers.Interfaces.FlightInterfaces
{
    public interface IFlightBasicEventHandler
    {
        Task Notify(Flight flight);
    }
}
