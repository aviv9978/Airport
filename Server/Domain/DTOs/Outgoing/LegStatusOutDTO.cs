using Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DTOs.Outgoing
{
    public class LegStatusOutDTO
    {
        public int LegNumber { get; set; }
        public bool IsOccupied { get; set; }
        public virtual FlightOutDTO? Flight { get; set; }
    }
}
