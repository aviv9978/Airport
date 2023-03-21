using FlightSimulator.Configures;
using FlightSimulator.Dal;
using FlightSimulator.Dal.Repositories.Flights;
using FlightSimulator.Dal.Repositories.Logger;
using FlightSimulator.Dal.Repositories.Pilots;
using Microsoft.EntityFrameworkCore;
using Serilog;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.

builder.Services.AddScoped<IFlightRepository, FlightRepository>();
builder.Services.AddScoped<ILoggerRepository, LoggerRepository>();
builder.Services.AddScoped<IPilotRepository, PilotRepository>();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<AirportDataContext>(options =>
options.UseSqlServer(builder.Configuration["ConnectionStrings:myAirport"]));

//To enable client to communicate with server
var configureService = new ConfigureService();
builder.Services.AddCors(options =>
{
    options.AddPolicy("EnableCORS", builder =>
    {
        builder.AllowAnyHeader()
               .AllowAnyMethod()
               .WithOrigins(configureService.GetClient(), configureService.GetServer());
    });
});


var logger = new LoggerConfiguration()
  .ReadFrom.Configuration(builder.Configuration)
  .Enrich.WithThreadId()
  .Enrich.FromLogContext()
  .CreateLogger();
builder.Logging.ClearProviders();
builder.Logging.AddSerilog(logger);



var app = builder.Build();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("EnableCORS");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
