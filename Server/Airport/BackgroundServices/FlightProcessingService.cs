using Core.Entities.Terminal;
using Core.EventHandlers.Enums;
using Core.EventHandlers.Interfaces.DAL;
using Core.EventHandlers.Interfaces.FlightInterfaces;
using Core.Interfaces.Subject;

namespace Airport.BackgroundServices
{
    public class FlightProcessingService : BackgroundService
    {
        public static Queue<Flight> FlightQueue = new Queue<Flight>();
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public FlightProcessingService(IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                Flight flight;
                FlightQueue.TryDequeue(out flight);
                if (flight != null)
                {
                    using (var scope = _serviceScopeFactory.CreateScope())
                    {
                        IEnumerable<IFlightDalEventHandler> _flightDalEventHandlers;
                        ILegDalEventHandler _legDalEventHandler;
                        IFlightLegDalEventHandler _flightLegDalEventHandler;
                        IFlightBasicEventHandler _flightBasicEventHandler;
                        _flightDalEventHandlers = scope.ServiceProvider.GetServices<IFlightDalEventHandler>();
                        _legDalEventHandler = scope.ServiceProvider.GetService<ILegDalEventHandler>();
                        _flightLegDalEventHandler = scope.ServiceProvider.GetService<IFlightLegDalEventHandler>();
                        _flightBasicEventHandler = scope.ServiceProvider.GetService<IFlightBasicEventHandler>();

                        var subject = scope.ServiceProvider.GetRequiredService<IISUbject>();
                        SubscribeToBasicDalHandler(_flightDalEventHandlers, _legDalEventHandler, _flightLegDalEventHandler, subject);
                        SubscribeToFlightBasicEventHandler(_flightBasicEventHandler, subject);

                        await subject.NotifyFlightToDalAsync(DalTopic.AddFlight, flight);
                    }
                }
                else
                {
                    await Task.Delay(1000, stoppingToken); // wait for 1 second before checking the queue again
                }
            }
        }

        private static void SubscribeToFlightBasicEventHandler(IFlightBasicEventHandler _flightBasicEventHandler, IISUbject subject)
        {
            subject.AttachFlightHandlerToEventType(_flightBasicEventHandler.FlightTopic, _flightBasicEventHandler);
        }

        private static void SubscribeToBasicDalHandler(IEnumerable<IFlightDalEventHandler> _flightDalEventHandlers, ILegDalEventHandler _legDalEventHandler, IFlightLegDalEventHandler _flightLegDalEventHandler, IISUbject subject)
        {
            foreach (var flightDalEventHandler in _flightDalEventHandlers)
            {
                subject.AttachFlightDalHandlerToEventType(flightDalEventHandler.DalTopic, flightDalEventHandler);
            }
            subject.AttachFlightLegDalHandlerToEventType(_flightLegDalEventHandler.DalTopic, _flightLegDalEventHandler);
            subject.AttachLegDalHandlerToEventType(_legDalEventHandler.DalTopic, _legDalEventHandler);
        }
    }
}
