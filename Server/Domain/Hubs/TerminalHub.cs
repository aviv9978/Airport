using Core.DTOs.Outgoing;
using Core.Entities;
using Core.Entities.Terminal;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Hubs
{
    public class TerminalHub : Hub, ITerminalHub
    {
        private readonly IHubContext<TerminalHub> _terminalHubContext;

        public TerminalHub(IHubContext<TerminalHub> flightHub)
        {
            _terminalHubContext = flightHub;
        }
       
        public async Task SendEnteringUpdateAsync(Flight flight, int legId) => await _terminalHubContext.Clients.All.SendAsync("Update", $"{flight} + {legId}");

        public async Task SendLogAsync(ProcessLogOutDTO processLogOutDTO) => await _terminalHubContext.Clients.All.SendAsync("logUpdate", processLogOutDTO);
    }
}
