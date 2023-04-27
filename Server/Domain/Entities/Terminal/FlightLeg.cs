using Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities.Terminal
{
    public class FlightAndNextLeg : IBaseEntity
    {
        public Flight? Flight { get; set; }
        public Leg? NextLeg { get; set; }
        public FlightAndNextLeg(Flight? flight, Leg? nextLeg)
        {
            Flight = flight;
            NextLeg = nextLeg;
        }
    }
}
