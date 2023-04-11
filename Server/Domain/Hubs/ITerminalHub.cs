using Core.DTOs.Outgoing;
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
        Task SendEnteringUpdateAsync(Flight flight, int legId);
        Task SendLogAsync(ProcessLogOutDTO procLogOutDTO);
        Task SendLogOutUpdateAsync(int procLogID, DateTime exitTime);
        Task UpdateEnterLeg(LegStatusOutDTO legStatus);
        Task UpdateLogOutLeg(LegStatusOutDTO legStatus);
    }
}
