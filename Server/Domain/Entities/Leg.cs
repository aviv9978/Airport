using FlightSimulator.Models.Enums;


namespace Core.Entities
{
    public class Leg : BaseEntity
    {
        public virtual Flight? Flight { get; set; }
        public LegNumber CurrentLeg { get; set; }
        public LegNumber NextPosibbleLegs { get; set; }
    }
}
