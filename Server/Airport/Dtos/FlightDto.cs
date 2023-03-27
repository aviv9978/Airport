using Core.Entities;
using System.ComponentModel.DataAnnotations;

namespace FlightSimulator.Models
{
    public class FlightDto
    {
        public string? Code { get; set; }
        public string? Name { get; set; }
        public bool IsDeparture { get; set; }
        public virtual Pilot? Pilot { get; set; }
    }
}
