
namespace ConsoleSimulator.Models
{
    internal class FlightDto
    {
        public string? Code { get; set; }
        public virtual PilotDto? Pilot { get; set; }
        public FlightDto() => Code = Guid.NewGuid().ToString();
    }
}
