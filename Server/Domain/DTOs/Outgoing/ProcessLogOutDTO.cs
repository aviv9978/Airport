using Core.Entities.Terminal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DTOs.Outgoing
{
    public class ProcessLogOutDTO
    {
        public int Id { get; set; }
        public virtual FlightOutDTO? Flight { get; set; }
        public string? LegNumber { get; set; }
        public DateTime? EnterTime { get; set; }
        public DateTime? ExitTime { get; set; }
        public string? Message { get; set; }
    }
}
