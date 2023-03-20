using FlightSimulator.Models.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FlightSimulator.Models
{
    public class Leg
    {
        public int Id { get; set; }
        public virtual Flight? Flight { get; set; }
        public LegNumber CurrentLeg { get; set; }
        public LegNumber NextPosibbleLegs { get; set; }
    }
}
