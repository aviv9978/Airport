using Core.DTOs.Outgoing;
using Core.Entities;
using Core.Entities.Terminal;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Hubs
{
    public class TerminalHub : Hub, IHUB
    {
        private readonly IHubContext<TerminalHub> _terminalHubContext;

        public TerminalHub(IHubContext<TerminalHub> flightHub)
        {
            _terminalHubContext = flightHub;
        }

        public async Task SendEnteringUpdateAsync(Flight flight, int legId) => await _terminalHubContext.Clients.All.SendAsync("Update", $"{flight} + {legId}");

        public async Task SendLogAsync(ProcessLogOutDTO processLogOutDTO) => await _terminalHubContext.Clients.All.SendAsync("addLog", processLogOutDTO);

        public async Task SendLogOutUpdateAsync(int procLogID, DateTime exitTime) => await _terminalHubContext.Clients.All.SendAsync("logExitUpdate", JsonConvert.SerializeObject(new { procLogID, exitTime }));

        public async Task UpdateEnterLegAsync(LegStatusOutDTO legStatus) => await _terminalHubContext.Clients.All.SendAsync("updateLegStatus", legStatus);
        public async Task UpdateLogOutLegAsync(LegStatusOutDTO legStatus) => await _terminalHubContext.Clients.All.SendAsync("updateLegStatus", legStatus);

    }
}
