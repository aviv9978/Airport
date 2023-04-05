using Core.Entities;
using Core.Entities.Terminal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Hubs
{
    public interface ITerminalHub
    {
        Task SendEnteringUpdate(Flight flight, int legId);
        Task SendLog(ProcessLog procLog);
    }
}
