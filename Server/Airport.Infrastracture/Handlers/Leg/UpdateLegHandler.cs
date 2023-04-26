using Airport.Infrastracture.Handlers.Flight;
using Core.EventHandlers.Interfaces.DAL;
using Core.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airport.Infrastracture.Handlers.Leg
{
    public class UpdateLegHandler : ILegDalHandler
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<UpdateLegHandler> _logger;

        public UpdateLegHandler(IUnitOfWork unitOfWork, ILogger<UpdateLegHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task UpdateAsync(Core.Entities.Terminal.Leg leg)
        {
            await _unitOfWork.Leg.UpdateAsync(leg);
            _logger.LogInformation($"Flight {leg} has been updated.");
        }
    }
}
