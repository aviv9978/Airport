using Core.Entities.Terminal;
using Core.Enums;
using Core.EventHandlers.Interfaces.DAL;
using Core.Interfaces.Subject;
using Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.EventHandlers.Enums;

namespace Airport.Infrastracture.Handlers.FlightHandlers
{
    public class FlightFinishedLegHandler : IFlightDalEventHandler
    {
        public DalTopic DalTopic { get; set; } = DalTopic.FlightFinishedLeg;

        private readonly IISUbject _subject;
        private readonly IUnitOfWork _unitOfWork;

        public FlightFinishedLegHandler(IISUbject subject, IUnitOfWork unitOfWork)
        {
            _subject = subject;
            _unitOfWork = unitOfWork;
        }
        public async Task NotifyAsync(Flight flight)
        {
            var flightNextPosLegs = flight.Leg.NextPosibbleLegs;
            var nextLegs = await _unitOfWork.Leg.FindListAsync(leg => flightNextPosLegs.HasFlag(leg.CurrentLeg));

            foreach (var nextLeg in nextLegs)
            {
                if (nextLeg.IsOccupied == false)
                {
                    _subject.NotifyFlightNextLegClear(flight, nextLeg);
                    return;
                }
            }
            foreach (var leg in nextLegs)
                _subject.AttatchFlightToLegQueue(flight, leg);
        }

    }
}
