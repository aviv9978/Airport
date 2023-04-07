using Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DTOs.Outcoming
{
    public class PilotOutDTO
    {

        public string? FullName { get; set; }
        public int Age { get; set; }
        public Rank Rank { get; set; }
    }
}
