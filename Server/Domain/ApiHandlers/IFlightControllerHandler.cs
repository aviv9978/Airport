using Core.Entities;
using Core.Entities.Terminal;
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
        Task AddFlightAsync(Flight flight);
    }
}
