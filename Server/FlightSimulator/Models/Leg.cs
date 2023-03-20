using FlightSimulator.Models.Enums;

namespace FlightSimulator.Models
{
    public class Leg
    {
        public int Id { get; set; }
        public LegNumber CurrentLeg { get; set; }
        public LegNumber NextPosibbleLegs { get; set; }
    }
}
