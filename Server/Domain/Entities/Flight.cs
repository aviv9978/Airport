using System.ComponentModel.DataAnnotations;

namespace Core.Entities
{
    public class Flight : BaseEntity
    {
        [Required]
        public string? Code { get; set; }
        public string? Name { get; set; }
        public bool IsDeparture { get; set; }
        [Required]
        public virtual Pilot? Pilot { get; set; }
        public virtual Leg? Leg { get; set; }
        public virtual ICollection<ProcessLog> ProcessLogs { get; set; } = new List<ProcessLog>();
    }
}
