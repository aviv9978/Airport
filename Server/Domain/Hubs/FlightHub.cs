using Core.Entities;
using Core.Interfaces.Hub;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Hubs
{
    public class FlightHub : Hub, IFlightHub
    {
        private readonly IHubContext<FlightHub> _flightHubContext;

        public FlightHub(IHubContext<FlightHub> flightHub)
        {
            _flightHubContext = flightHub;
        }
        public async Task SendEnteringUpdate(Flight flight, int legId) => await _flightHubContext.Clients.All.SendAsync("Update", $"{flight} + {legId}");

    }
}
