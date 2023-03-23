using FlightSimulator.Dal;
using FlightSimulator.Dal.Repositories.Flights;
using FlightSimulator.Dal.Repositories.Logger;
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
        private readonly ILoggerRepository _procLogger;
        private readonly IFlightRepository _flightRepos;
        private readonly ILogger<FlightsController> _logger;

        public FlightsController(IFlightRepository flightRepos, ILogger<FlightsController> logger, ILoggerRepository processLogger)
        {
            _procLogger = processLogger;
            _flightRepos = flightRepos;
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> AddStam([FromBody] Flight flight)
        {
            try
            {
                await _flightRepos.AddFlight(flight);
                var newLog = new ProcessLog { Flight = flight, EnterTime = DateTime.Now, Message = "A plain has entered to leg", ExitTime = null };
                await _procLogger.AddLog(newLog);
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

    }
}
