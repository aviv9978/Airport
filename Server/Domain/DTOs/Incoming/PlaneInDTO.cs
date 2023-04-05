using Core.Entities.ForFlight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DTOs.Incoming
{
    public class PlaneInDTO
    {
        public virtual Company? Company { get; set; }
        public string? Model { get; set; }
        public int PassangerCount { get; set; }
    }
}
