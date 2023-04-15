using Airport.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Airport.Controllers
{
    [Route("api/ProcessLogs")]
    [ApiController]
    public class ProcessLogsController : ControllerBase
    {
        private readonly IProcLogService _procLogService;

        public ProcessLogsController(IProcLogService procLogService)
        {
            _procLogService = procLogService;
        }
        // GET: api/<ProcessLogs>
        [HttpGet]
        [Route("GetAllProcessLogs")]
        public async Task<IActionResult> GetProcessLogs()
        {
            try
            {
                var allProcLogs = await _procLogService.GetProcessLogsAsync();
                return Ok(allProcLogs);
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }
    }
}
