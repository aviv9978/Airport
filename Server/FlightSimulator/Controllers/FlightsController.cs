using FlightSimulator.Dal;
using FlightSimulator.Dal.Repositories;
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
        private readonly IFlightRepository flightRepos;
        private readonly ILogger<FlightsController> logger;

        public FlightsController(IFlightRepository _flightRepos, ILogger<FlightsController> logger)
        {
            this.flightRepos= _flightRepos;
            this.logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> AddStam(Flight stam)
        {
            try
            {
                return Ok();

            }
            catch (Exception e)
            {

                this.logger.LogError(e, e.Message);
                return StatusCode(500);
                throw;
            }

        }
    }
}
