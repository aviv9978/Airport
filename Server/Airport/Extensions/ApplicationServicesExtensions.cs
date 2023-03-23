using Airport.Infrastracture.Repositories;
using Core.Interfaces;
using FlightSimulator.Errors;
using Microsoft.AspNetCore.Mvc;

namespace FlightSimulator.Extensions
{
    public static class ApplicationServicesExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {

            services.AddScoped<IFlightRepository, FlightRepository>();
            services.AddScoped<ILoggerRepository, LoggerRepository>();
            services.AddScoped<IPilotRepository, PilotRepository>();
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
