using System.ComponentModel.DataAnnotations.Schema;
using Core.Entities.ForFlight;

namespace Core.Entities.Terminal
{
    public class Flight : BaseEntity
    {
        public Guid? Code { get; set; }
        public bool IsDeparture { get; set; }
        public virtual Plane? Plane { get; set; }
        public virtual Pilot? Pilot { get; set; }
        [NotMapped]
        public virtual Leg? Leg { get; set; }
        public virtual List<ProcessLog>? ProcessLog { get; set; }
    }
}
