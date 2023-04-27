using Castle.Core.Logging;
using Core.EventHandlers.Interfaces.DAL;
using Core.Interfaces;
using Microsoft.Extensions.Logging;
using Core.Entities.Terminal;
using Core.EventHandlers.Enums;

namespace Airport.Infrastracture.Handlers.FlightHandlers
{
    public class UpdateFlightHandler : IFlightDalEventHandler
    {
        public DalTopic Topic = DalTopic.UpdateFlight;

        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<UpdateFlightHandler> _logger;

        public UpdateFlightHandler(IUnitOfWork unitOfWork, ILogger<UpdateFlightHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task NotifyAsync(Flight flight)
        {
            await _unitOfWork.Flight.UpdateAsync(flight);
            await _unitOfWork.CommitAsync();
            _logger.LogInformation($"Flight {flight} has been updated.");
        }
    }
}
