using AutoMapper;
using Core.DTOs.Incoming;
using Core.DTOs.Outcoming;
using Core.Entities.ForFlight;
using Core.Interfaces.Repositories;
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
        private readonly IMapper _mapper;

        public PilotsController(IPilotRepository pilotRepos, ILogger<FlightsController> logger,
            IMapper mapper)
        {
            _pilotRepos = pilotRepos;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpPost("AddPilot")]
        public async Task<IActionResult> AddPilot([FromBody] PilotInDTO pilotInDTO)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var _pilot = _mapper.Map<Pilot>(pilotInDTO);
                    await _pilotRepos.AddPilotAsync(_pilot);
                    _logger.LogInformation("Successssss adding pilot");
                    var newPilot = _mapper.Map<PilotOutDTO>(_pilot);


                    return Ok(newPilot);
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

        [HttpGet("Pilots")]
        public async Task<IActionResult> GetAllPilots()
        {
            var allPilots = await _pilotRepos.GetAllPilotsAsync();

            var _pilots = _mapper.Map<IEnumerable<PilotOutDTO>>(allPilots);
            return Ok(_pilots);
        }
    }
}
