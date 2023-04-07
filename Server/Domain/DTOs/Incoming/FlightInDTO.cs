using Core.Entities.ForFlight;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DTOs.Incoming
{
    public class FlightInDTO
    {
        [Required]
        public Guid? Code { get; set; }
        //public bool IsDeparture { get; set; }
        [Required]
        public virtual Pilot? Pilot { get; set; }
        [Required]
        public virtual Plain? Plain { get; set; }
    }
}
