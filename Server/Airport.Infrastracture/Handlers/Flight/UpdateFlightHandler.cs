

using Castle.Core.Logging;
using Core.EventHandlers.Interfaces.DAL;
using Core.Interfaces;
using Microsoft.Extensions.Logging;

namespace Airport.Infrastracture.Handlers.Flight
{
    public class UpdateFlightHandler : IFlightDalHandler
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<UpdateFlightHandler> _logger;

        public UpdateFlightHandler(IUnitOfWork unitOfWork, ILogger<UpdateFlightHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task UpdateAsync(Core.Entities.Terminal.Flight flight)
        {
            await _unitOfWork.Flight.UpdateAsync(flight);
            _logger.LogInformation($"Flight {flight} has been updated.");
        }
    }
}
