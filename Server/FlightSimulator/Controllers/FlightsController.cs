using FlightSimulator.Dal;
using FlightSimulator.Dal.Repositories.Flights;
using FlightSimulator.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace FlightSimulator.Controllers
{
    [Route("api/Flights")]
    [ApiController]
    public class FlightsController : ControllerBase
    {
        private readonly IFlightRepository _flightRepos;
        private readonly ILogger<FlightsController> _logger;

        public FlightsController(IFlightRepository flightRepos, ILogger<FlightsController> logger)
        {
            _flightRepos = flightRepos;
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> AddStam(Flight flight)
        {
            try
            {
                await _flightRepos.AddFlight(flight);
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

    }
}
