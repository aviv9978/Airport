using Airport.Application.ILogicServices;
using AutoMapper;
using Core.DTOs.Incoming;
using Core.Entities.Terminal;
using Microsoft.AspNetCore.Mvc;

namespace FlightSimulator.Controllers
{
    [Route("api/Flights")]
    [ApiController]
    public class FlightsController : ControllerBase
    {

        private readonly ILogger<FlightsController> _logger;
        private readonly ITerminalService _terminalService;
        private readonly IMapper _mapper;
        public FlightsController(ITerminalService terminal, ILogger<FlightsController> logger,
                 IMapper mapper)
        {
            _logger = logger;
            _terminalService = terminal;
            _mapper = mapper;
        }

        [HttpPost]
        [Route("AddDepartureFlight")]
        public async Task<IActionResult> AddDepartureFlight([FromBody] FlightInDTO flightDto) => await StartFlightAsync(flightDto, true);


        [HttpPost]
        [Route("AddLandingFlight")]
        public async Task<IActionResult> AddLandingFlight([FromBody] FlightInDTO flightDto) => await StartFlightAsync(flightDto, false);

        private async Task<IActionResult> StartFlightAsync(FlightInDTO flightDto, bool isDeparture)
        {
            try
            {
                var flight = _mapper.Map<Flight>(flightDto);
                await _terminalService.StartFlightAsync(flight, isDeparture);
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
