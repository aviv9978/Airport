using Airport.Application.ILogicServices;
using Airport.Handlers;
using AutoMapper;
using Core.ApiHandlers;
using Core.DTOs.Incoming;
using Core.Entities.Terminal;
using Core.Interfaces.Subject;
using Hangfire;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting.Internal;
using System.Linq.Expressions;

namespace FlightSimulator.Controllers
{
    [Route("api/Flights")]
    [ApiController]
    public class FlightsController : ControllerBase
    {
        private readonly ILogger<FlightsController> _logger;
        private readonly IMapper _mapper;
        private readonly IBackgroundJobClient _backgroundJobClient;
        private readonly IServiceProvider _serviceProvider;
        private readonly IFlightControllerHandler _flightControllerHandler;
        public FlightsController(ILogger<FlightsController> logger,
            IMapper mapper,
            IBackgroundJobClient backgroundJobClient,
            IServiceProvider serviceProvider,
            IFlightControllerHandler flightControllerHandler)
        {
            _logger = logger;
            _mapper = mapper;
            _backgroundJobClient = backgroundJobClient;
            _serviceProvider = serviceProvider;
            _flightControllerHandler = flightControllerHandler;
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
                 _flightControllerHandler.AddFlight(flight);
                await Task.Delay(25000);
                // RecurringJob.AddOrUpdate<IFlightControllerHandler>($"AddFlight:{flight.Id}", (handler) => handler.AddFlight(flight), Cron.Hourly(0));
                //_backgroundJobClient.Enqueue<IFlightControllerHandler>(handler => handler.AddFlight(flight));
                //using (var scope = _serviceProvider.CreateScope())
                //{
                //_backgroundJobClient.Enqueue<IFlightControllerHandler>(handler => handler.AddFlight(flight));
                //}
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
