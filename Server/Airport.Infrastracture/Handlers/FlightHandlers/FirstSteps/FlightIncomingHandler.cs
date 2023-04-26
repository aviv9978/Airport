using Core.EventHandlers.Interfaces.DAL;
using Core.Entities.Terminal;
using Core.Interfaces.Subject;
using Core.Interfaces;
using Microsoft.Extensions.Logging;
using Core.Enums;

namespace Airport.Infrastracture.Handlers.FlightHandlers.FirstSteps
{
    public class FlightIncomingHandler : IFlightDalHandler
    {
        private readonly IISUbject _subject;
        private readonly IUnitOfWork _unitOfWork;

        public FlightIncomingHandler(IISUbject subject, IUnitOfWork unitOfWork)
        {
            _subject = subject;
            _unitOfWork = unitOfWork;
        }
        public async Task NotifyAsync(Flight flight)
        {
            IEnumerable<Leg> flightFirstLegs = await FindFlightNextLegsAsync(flight);
            foreach (var enteringLeg in flightFirstLegs)
            {
                if (enteringLeg.IsOccupied == false)
                {
                    _subject.NotifyFlightNextLegClear(flight, enteringLeg);
                    return;
                }
            }
            foreach (var leg in flightFirstLegs)
                _subject.AttatchFlightToLegQueue(flight, leg);
        }

        private async Task<IEnumerable<Leg>> FindFlightNextLegsAsync(Flight flight)
        {
            var legType = flight.IsDeparture ? LegType.StartForDeparture : LegType.StartForLand;
            var flightNextLegs = await _unitOfWork.Leg.FindListAsync(leg => leg.LegType == legType);
            return flightNextLegs;
        }
    }
}
