using Airport.Application.ILogicServices;
using Core.Entities;
using Core.Interfaces;
using FlightSimulator.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FlightSimulator.Controllers
{
    [Route("api/Flights")]
    [ApiController]
    public class FlightsController : ControllerBase
    {
        private readonly ILegRepostiroy _leg;
        private readonly IProcLogRepository _procLogger;
        private readonly IFlightRepository _flightRepos;
        private readonly ILogger<FlightsController> _logger;
        private readonly ITerminalService _ter;

        public FlightsController(ITerminalService ter, ILogger<FlightsController> logger,
            ILegRepostiroy ileg, IProcLogRepository procLogger, IFlightRepository flightRepos)
        {
            _leg = ileg;
            _procLogger = procLogger;
            _flightRepos = flightRepos;
            _logger = logger;
            _ter = ter;
        }

        [HttpPost]
        [Route("AddFlight")]
        public async Task<IActionResult> AddStam([FromBody] FlightDto flightDto)
        {
            try
            {

                var flight = new Flight { Name = flightDto.Name, IsDeparture = true, };
                await _flightRepos.AddFlightAsync(flight);
                var newLog = new ProcessLog { Flight = flight, EnterTime = DateTime.Now, Message = "A plain has entered to leg", ExitTime = null };
                await _procLogger.AddProcLogAsync(newLog);
                _logger.LogError("Successssss");
                await _procLogger.UpdateOutLog(flight.Id);
                return Ok();

            }
            catch (Exception e)
            {

                this._logger.LogError(e, e.Message);
                return StatusCode(500);
                throw;
            }

        }
        [HttpPost]
        [Route("AddLegs")]
        public async Task<IActionResult> AddLegs()
        {
            for (int i = 0; i < 10; i++)
            {
                if (i == 6)
                    await _leg.AddLegAsync(new Leg { CurrentLeg = (Core.Enums.LegNumber)(i + 1), NextPosibbleLegs = (Core.Enums.LegNumber)(i + 2) | (Core.Enums.LegNumber)(i + 3) });
                else
                    await _leg.AddLegAsync(new Leg { CurrentLeg = (Core.Enums.LegNumber)(i + 1), NextPosibbleLegs = (Core.Enums.LegNumber)(i + 2) });
            }
            return Ok();

        }
    }
}
