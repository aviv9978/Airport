using Airport.Application.ILogicServices;
using FlightSimulator.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Airport.Controllers
{
    [Route("api/LegStatus")]
    [ApiController]
    public class LegStatusController : ControllerBase
    {
        private readonly ILegStatusService _legService;
        private readonly ILogger<LegStatusController> _logger;

        public LegStatusController(ILegStatusService legService, ILogger<LegStatusController> logger)
        {
            _logger = logger;
            _legService = legService;
        }

        [HttpGet]
        [Route("GetLegStatus")]
        public async Task<IActionResult> GetLegsStatusAsync()
        {
            try
            {
                var LegsStatus = await _legService.GetLegsStatusAsync();
                return Ok(LegsStatus);
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return StatusCode(500);
            }
        }
    }
}
