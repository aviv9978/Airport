using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleSimulator.Dto
{
    public class PlaneDTO
    {
        public CompanyDTO? Company { get; set; }
        public string? Model { get; set; }
        public int PassangerCount { get; set; }
    }
}
