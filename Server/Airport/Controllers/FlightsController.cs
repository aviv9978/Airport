using Airport.Application.ILogicServices;
using AutoMapper;
using Core.DTOs.Incoming;
using Core.Entities.Terminal;
using Core.Interfaces.Repositories;
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
        private readonly IMapper _mapper;
        public FlightsController(ITerminalService terminal, ILogger<FlightsController> logger,
              IFlightRepository flightRepos, IMapper mapper)
        {
            _flightRepos = flightRepos;
            _logger = logger;
            _terminalService = terminal;
            _mapper = mapper;
        }

        [HttpPost]
        [Route("AddDepartureFlight")]
        public async Task<IActionResult> AddDepartureFlight([FromBody] FlightInDTO flightDto)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    var flight = _mapper.Map<Flight>(flightDto);
                    await _flightRepos.AddFlightAsync(flight);
                    await _terminalService.StartFlightAsync(flight, true);
                    _logger.LogError("Successssss");
                    return Ok();
                }
                else return new JsonResult("Something went wrong") { StatusCode = 500 };

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
        public async Task<IActionResult> AddLandingFlight([FromBody] FlightInDTO flightDto)
        {
            try
            {

                var flight = new Flight { Plain = flightDto.Plane, IsDeparture = true, };
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
