using Airport.Application.ILogicServices;
using Airport.Application.LogicServices;
using Airport.Infrastracture.Repositories;
using Core.Hubs;
using Core.Interfaces.Hub;
using Core.Interfaces.Repositories;
using FlightSimulator.Errors;
using Microsoft.AspNetCore.Mvc;

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
            services.AddScoped<ITerminalService, TerminalService>();
            services.AddScoped<IFlightHub, FlightHub>();
            services.Configure<ApiBehaviorOptions>(options => options.InvalidModelStateResponseFactory = ActionContext => {
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
