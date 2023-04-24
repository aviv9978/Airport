using Core.Entities.Terminal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Events
{
    public class LegEventArgs : EventArgs
    {
        public Leg NextLeg { get; set; }

        public LegEventArgs(Leg nextLeg)
        {
            NextLeg = nextLeg;
        }
    }
}
