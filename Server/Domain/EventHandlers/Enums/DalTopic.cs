using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.EventHandlers.Enums
{
    public enum DalTopic
    {
        AddFlight,
        FlightInComing,
        UpdateFlight,
        UpdateLeg,
        FlightNextLegs,
        FlightFinishedLeg,
        FlightNextLegClear,
        FlightCompleted,
    }
}
