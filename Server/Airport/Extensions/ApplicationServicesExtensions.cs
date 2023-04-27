using Airport.Application;
using Airport.Application.EventHandlers.FlightHandlers;
using Airport.Application.Events;
using Airport.Application.ILogicServices;
using Airport.Application.Interfaces;
using Airport.Application.LogicServices;
using Airport.Handlers;
using Airport.Infrastracture.Handlers.FlightHandlers;
using Airport.Infrastracture.Handlers.FlightHandlers.FirstSteps;
using Airport.Infrastracture.Handlers.FlightLegHandlers;
using Airport.Infrastracture.Handlers.LegHandlers;
using Airport.Infrastracture.Repositories;
using Core.ApiHandlers;
using Core.Entities;
using Core.Entities.Terminal;
using Core.EventHandlers.Interfaces.DAL;
using Core.EventHandlers.Interfaces.FlightInterfaces;
using Core.Hubs;
using Core.Interfaces;
using Core.Interfaces.Events;
using Core.Interfaces.Repositories;
using Core.Interfaces.Subject;
using FlightSimulator.Errors;
using Microsoft.AspNetCore.Mvc;

namespace FlightSimulator.Extensions
{
    public static class ApplicationServicesExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            // services.AddScoped<IFlightRepository, FlightRepository>();
            // services.AddScoped<IProcLogRepository, ProcLogRepository>();
            // services.AddScoped<IPilotRepository, PilotRepository>();
            //  services.AddScoped<ILegRepostiroy, LegRepository>();
            //services.AddScoped<ITerminalService, TerminalService>();
            //services.AddScoped<ILegStatusService, LegStatusService>();
            //services.AddScoped<IProcLogService, ProcLogsService>();
            //services.AddSingleton<IHUB, TerminalHub>();
            services.AddScoped<ISubject, Subject>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IISUbject, SSubject>();
            services.AddScoped<IFlightDalEventHandler, AddFlightHandler>();
            services.AddScoped<IFlightDalEventHandler, FlightIncomingHandler>();
            services.AddScoped<IFlightDalEventHandler, FlightCompletedHandler>();
            services.AddScoped<IFlightDalEventHandler, FlightFinishedLegHandler>();
            services.AddScoped<IFlightDalEventHandler, UpdateFlightHandler>();
            services.AddScoped<IFlightLegDalEventHandler, FlightNextLegClearHandler>();
            services.AddScoped<ILegDalEventHandler, UpdateLegHandler>();
            services.AddScoped<IFlightBasicEventHandler, FlightEnteredLegHandler>();
            services.AddScoped<IFlightControllerHandler, FlightControllerHandler>();

            services.Configure<ApiBehaviorOptions>(options => options.InvalidModelStateResponseFactory = ActionContext =>
            {
                var error = ActionContext.ModelState.Where(e => e.Value.Errors.Count > 0).SelectMany(e => e.Value.Errors).Select(e => e.ErrorMessage).ToArray();
                var errorresponce = new APIValidationErrorResponce
                {
                    Errors = error
                };
                return new BadRequestObjectResult(error);
            });
            return services;
        }
    }
}
