using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entities
{
    public class Flight : BaseEntity
    {
        public string? Code { get; set; }
        public string? Name { get; set; }
        public bool IsDeparture { get; set; }
        public virtual Pilot? Pilot { get; set; }
        [NotMapped]
        public virtual Leg? Leg { get; set; }
        public virtual ICollection<ProcessLog> ProcessLog { get; set; }
    }
}
