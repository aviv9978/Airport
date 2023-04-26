using Core.Entities.Terminal;
using Core.EventHandlers.Interfaces.FlightInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airport.Infrastracture.Handlers.FlightHandlers
{
    public class FlightCompletedHandler : IFlightBasicHandler
    {

        public Task Notify(Flight flight)
        {
            UpdateFlightAndLegCode(flight);
        }

        private static void UpdateFlightAndLegCode(Flight flight)
        {
            var leg = flight.Leg;
            leg.IsOccupied = false;
            leg.Flight = null;
            flight.Leg = null;
        }
    }
}
