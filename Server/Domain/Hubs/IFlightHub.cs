using Core.Entities.Terminal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Hubs
{
    public interface IFlightHub
    {
        Task SendEnteringUpdate(Flight flight, int legId);
    }
}
