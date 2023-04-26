using Core.Entities.Terminal;

namespace Core.EventHandlers.Interfaces.FlightInterfaces
{
    public interface IFlightBasicHandler
    {
        Task Notify(Flight flight);
    }
}
