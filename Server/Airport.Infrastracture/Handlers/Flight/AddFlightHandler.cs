using Core.EventHandlers.Interfaces.DAL;
using Core.Interfaces;
using Core.Interfaces.Subject;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Airport.Infrastracture.Handlers.Flight
{
    public class AddFlightHandler : IFlightDalHandler
    {
        private readonly IISUbject _subject;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<AddFlightHandler> _logger;

        public AddFlightHandler(IISUbject subject,IUnitOfWork unitOfWork, ILogger<AddFlightHandler> logger)
        {
            _subject = subject;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }
        public async Task UpdateAsync(Core.Entities.Terminal.Flight flight)
        {
            await _unitOfWork.Flight.AddAsync(flight);
            _logger.LogInformation($"Flight {flight.Id} has been added");
        }
    }
}
