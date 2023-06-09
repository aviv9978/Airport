﻿using Airport.Application;
using Airport.Application.EventHandlers.FlightHandlers;
using Airport.Application.Events;
using Airport.Application.Events.DalSubjects;
using Airport.Application.Events.EventHandlersSubjects;
using Airport.Application.ILogicServices;
using Airport.Application.Interfaces;
using Airport.Application.LogicServices;
using Airport.Application.Subjects.SubscribeSubject;
using Airport.Handlers;
using Airport.Handlers.Airport.Handlers;
using Airport.HangFire;
using Airport.Infrastracture.Handlers.FlightHandlers;
using Airport.Infrastracture.Handlers.FlightHandlers.FirstSteps;
using Airport.Infrastracture.Handlers.FlightLegHandlers;
using Airport.Infrastracture.Handlers.LegHandlers;
using Airport.Infrastracture.Repositories;
using Core.ApiHandlers;
using Core.Entities;
using Core.Entities.Terminal;
using Core.EventHandlers.Interfaces;
using Core.EventHandlers.Interfaces.DAL;
using Core.EventHandlers.Interfaces.FlightInterfaces;
using Core.EventHandlers.Interfaces.Subjects.DAL;
using Core.EventHandlers.Interfaces.Subjects.EventHandlersSubjects;
using Core.EventHandlers.Interfaces.Subjects.Subscribers;
using Core.Hubs;
using Core.Interfaces;
using Core.Interfaces.Events;
using Core.Interfaces.Repositories;
using Core.Interfaces.Subject;
using FlightSimulator.Errors;
using Hangfire;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace FlightSimulator.Extensions
{
    public static class ApplicationServicesExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IFlightRepository, FlightRepository>();
            services.AddScoped<IProcLogRepository, ProcLogRepository>();
            services.AddScoped<IPilotRepository, PilotRepository>();
            services.AddScoped<ILegRepostiroy, LegRepository>();
            //services.AddScoped<ITerminalService, TerminalService>();
            //services.AddScoped<ILegStatusService, LegStatusService>();
            //services.AddScoped<IProcLogService, ProcLogsService>();
            //services.AddSingleton<IHUB, TerminalHub>();
            //services.AddScoped<ISubject, Subject>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IFlightDalEventHandler, AddFlightHandler>();
            services.AddScoped<IFlightDalEventHandler, FlightIncomingHandler>();
            services.AddScoped<IFlightDalEventHandler, FlightCompletedHandler>();
            services.AddScoped<IFlightDalEventHandler, FlightFinishedLegHandler>();
            services.AddScoped<IFlightDalEventHandler, UpdateFlightHandler>();
            services.AddScoped<IFlightLegDalEventHandler, FlightNextLegClearHandler>();
            services.AddScoped<ILegDalEventHandler, UpdateLegHandler>();
            services.AddScoped<IFlightBasicEventHandler, FlightEnteredLegHandler>();
            services.AddScoped<IDalSubject, DalSubject>();
            services.AddScoped<IFlightDalSubject, FlightDalSubject>();
            services.AddScoped<IFlightLegDalSubject, FlightLegDalSubject>();
            services.AddScoped<ILegDalSubject, LegDalSubject>();
            services.AddScoped<IEventHandlerSubject, EventHandlerSubject>();
            services.AddScoped<IFlightEventHandlerSubject, FlightEventHandlersSubject>();
            services.AddScoped<IFlightControllerHandler, FlightControllerHandler>();
            //services.AddScoped<ISubscribeToHandlers, SubscribeToHandlers>();
            //services.AddScoped<ISubscribeSubject, SubscriberSubject>();
            services.AddScoped<IISUbject, SSubject>();

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
