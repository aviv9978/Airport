using System.ComponentModel.DataAnnotations;

namespace FlightSimulator.Models
{
    public class Flight
    {
        public int Id { get; set; }
        [Required]
        public string? Code { get; set; }
        public string? Name { get; set; }
        public bool IsDeparture { get; set; }
        [Required]
        public virtual Pilot? Pilot { get; set; }
        public virtual Leg? CurrentLeg { get; set; }
    }
}
