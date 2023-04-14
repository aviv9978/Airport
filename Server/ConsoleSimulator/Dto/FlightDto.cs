using ConsoleSimulator.Dto;

namespace ConsoleSimulator.Models
{
    internal class FlightDto
    {
        public Guid Code { get; set; }
        public PilotDTO? Pilot { get; set; }
        public PlaneDTO? Plane { get; set; }
    }
}
