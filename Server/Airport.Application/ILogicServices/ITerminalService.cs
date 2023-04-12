using Core.Entities.Terminal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airport.Application.ILogicServices
{
    public interface ITerminalService
    {
        Task StartFlightAsync(Flight flight, bool isDeparture);
        static ICollection<Leg>? Legs { get; }
    }
}
