using Core.EventHandlers.Interfaces.DAL;
using Core.Interfaces;
using Core.Interfaces.Subject;
using Microsoft.Extensions.Logging;
using Core.Entities.Terminal;


namespace Airport.Infrastracture.Handlers.FlightHandlers.FirstSteps
{
    public class AddFlightHandler : IFlightDalHandler
    {
        private readonly IISUbject _subject;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<AddFlightHandler> _logger;

        public AddFlightHandler(IISUbject subject, IUnitOfWork unitOfWork, ILogger<AddFlightHandler> logger)
        {
            _subject = subject;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task NotifyAsync(Flight flight)
        {
            await _unitOfWork.Flight.AddAsync(flight);
            await _unitOfWork.CommitAsync();
            _logger.LogInformation($"Flight {flight.Id} has been added");
            _subject.NotifyIncomingFlight(flight);
        }
    }
}
