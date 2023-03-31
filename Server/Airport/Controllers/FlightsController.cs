using Airport.Application.ILogicServices;
using Core.Entities;
using Core.Interfaces.Repositories;
using FlightSimulator.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FlightSimulator.Controllers
{
    [Route("api/Flights")]
    [ApiController]
    public class FlightsController : ControllerBase
    {

        private readonly IFlightRepository _flightRepos;
        private readonly ILogger<FlightsController> _logger;
        private readonly ITerminalService _terminalService;

        public FlightsController(ITerminalService terminal, ILogger<FlightsController> logger,
              IFlightRepository flightRepos)
        {
            _flightRepos = flightRepos;
            _logger = logger;
            _terminalService = terminal;
        }

        [HttpPost]
        [Route("AddDepartureFlight")]
        public async Task<IActionResult> AddDepartureFlight([FromBody] FlightDto flightDto)
        {
            try
            {

                var flight = new Flight { Name = flightDto.Name, IsDeparture = true, };
                await _flightRepos.AddFlightAsync(flight);
                await _terminalService.StartFlightAsync(flight, true);
                _logger.LogError("Successssss");
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
        [Route("AddLandingFlight")]
        public async Task<IActionResult> AddLandingFlight([FromBody] FlightDto flightDto)
        {
            try
            {

                var flight = new Flight { Name = flightDto.Name, IsDeparture = true, };
                await _flightRepos.AddFlightAsync(flight);
                await _terminalService.StartFlightAsync(flight, false);
                _logger.LogError("Successssss");
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
            return Ok();

        }
    }
}
