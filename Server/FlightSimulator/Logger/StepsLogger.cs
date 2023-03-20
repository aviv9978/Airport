using FlightSimulator.Models;

namespace FlightSimulator.Logger
{
    public class StepsLogger
    {
        public int Id { get; set; }
        public virtual Flight? Flight { get; set; }
        public DateTime? In { get; set; }
        public DateTime? Out { get; set; }
        public string? Message { get; set; }
    }
}
