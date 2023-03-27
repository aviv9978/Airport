using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Enums
{
    public enum LegType
    {
        Departure = 0b00000001,
        Land = 0b00000010,
        Process = 0b00000100,
    }
}
