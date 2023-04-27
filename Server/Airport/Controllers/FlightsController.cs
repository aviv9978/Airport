using Airport.Application.ILogicServices;
using Airport.Handlers;
using AutoMapper;
using Core.ApiHandlers;
using Core.DTOs.Incoming;
using Core.Entities.Terminal;
using Core.Interfaces.Subject;
using Microsoft.AspNetCore.Mvc;

namespace FlightSimulator.Controllers
{
    [Route("api/Flights")]
    [ApiController]
    public class FlightsController : ControllerBase
    {
        private readonly IFlightControllerHandler _flightControllerHandler;
        private readonly ILogger<FlightsController> _logger;
        private readonly IMapper _mapper;
        public FlightsController(IFlightControllerHandler flightControllerHandler, ILogger<FlightsController> logger,
                 IMapper mapper)
        {
            _flightControllerHandler = flightControllerHandler;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpPost]
        [Route("AddDepartureFlight")]
        public async Task<IActionResult> AddDepartureFlightAsync([FromBody] FlightInDTO flightDto) => await StartFlightAsync(flightDto, true);


        [HttpPost]
        [Route("AddLandingFlight")]
        public async Task<IActionResult> AddLandingFlightAsync([FromBody] FlightInDTO flightDto) => await StartFlightAsync(flightDto, false);
        [HttpGet]
        [Route("ResetLegs")]
        public async Task<IActionResult> ResetLegsAsync()
        {
            try
            {
               // await _terminalService.ResetLegsAsync();
                return Ok();

            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        private async Task<IActionResult> StartFlightAsync(FlightInDTO flightDto, bool isDeparture)
        {
            try
            {
                var flight = _mapper.Map<Flight>(flightDto);
                flight.IsDeparture = isDeparture;
               await _flightControllerHandler.AddFlightAsync(flight);
                return Ok();
            }

            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return StatusCode(500);
            }
        }
    }
}
