using Core.EventHandlers.Interfaces.DAL;
using Core.Interfaces;
using Microsoft.Extensions.Logging;
using Core.Entities.Terminal;
using Core.EventHandlers.Enums;

namespace Airport.Infrastracture.Handlers.LegHandlers
{
    public class UpdateLegHandler : ILegDalEventHandler
    {
        public DalTopic DalTopic { get; set; } = DalTopic.UpdateLeg;


        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<UpdateLegHandler> _logger;

        public UpdateLegHandler(IUnitOfWork unitOfWork, ILogger<UpdateLegHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }


        public async Task NotifyAsync(Leg leg)
        {
             _unitOfWork.Leg.Update(leg);
            _logger.LogInformation($"Flight {leg} has been updated.");
        }
    }
}
