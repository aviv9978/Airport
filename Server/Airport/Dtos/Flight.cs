using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FlightSimulator.Models
{
    public class Flight
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string? Code { get; set; }
        public string? Name { get; set; }
        public bool IsDeparture { get; set; }
        [Required]
        public virtual Pilot? Pilot { get; set; }
        public virtual ICollection<ProcessLog> ProcessLogs { get; set; } = new List<ProcessLog>();
    }
}
