using Core.Entities.Terminal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.EventHandlers.Interfaces.DAL
{
    public interface IFlightLegDalEventHandler : IDalBasicEventHandler<FlightAndNextLeg>
    {
    }
}
