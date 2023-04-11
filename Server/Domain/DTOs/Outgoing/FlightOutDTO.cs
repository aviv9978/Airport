using Core.Entities.ForFlight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DTOs.Outgoing
{
    public class FlightOutDTO
    {
        public Guid? Code { get; set; }
        public bool IsDeparture { get; set; }
        public virtual Plane? Plane { get; set; }
        public virtual Pilot? Pilot { get; set; }
    }
}
