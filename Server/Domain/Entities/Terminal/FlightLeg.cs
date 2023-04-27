using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities.Terminal
{
    public class FlightLeg : BaseEntity
    {
        public Flight? Flight { get; set; }
        public Leg? Leg { get; set; }
        public FlightLeg(Flight? flight, Leg? leg)
        {
            Flight = flight;
            Leg = leg;
        }
    }
}
