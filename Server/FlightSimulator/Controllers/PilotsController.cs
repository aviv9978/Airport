using FlightSimulator.Dal.Repositories.Flights;
using FlightSimulator.Dal.Repositories.Pilots;
using FlightSimulator.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FlightSimulator.Controllers
{
    [Route("api/Pilots")]
    [ApiController]
    public class PilotsController : ControllerBase
    {
        private readonly IPilotRepository _pilotRepos;
        private readonly ILogger<FlightsController> _logger;

        public PilotsController(IPilotRepository pilotRepos, ILogger<FlightsController> logger)
        {
            _pilotRepos = pilotRepos;
            _logger = logger;
        }

        [HttpPost("AddPilot")]
        public async Task<IActionResult> AddStam(Pilot pilot)
        {
            try
            {
                await _pilotRepos.AddPilot(pilot);
                _logger.LogInformation("Successssss adding pilot");
                return Ok("Succes!");

            }
            catch (Exception e)
            {

                this._logger.LogError(e, e.Message);
                return StatusCode(500);
                throw;
            }

        }
    }
}
