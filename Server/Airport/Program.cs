﻿using Airport.Infrastracture;
using FlightSimulator.Configures;
using FlightSimulator.Extensions;
using Microsoft.EntityFrameworkCore;
using Core.Hubs;
using Serilog;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.

builder.Services.AddDbContext<AirportDataContext>(options =>
options.UseSqlServer(builder.Configuration["ConnectionStrings:myAirport"]));
builder.Services.AddApplicationServices();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//To enable client to communicate with server
var configureService = new ConfigureService();
builder.Services.AddSignalR();
builder.Services.AddCors(options =>
{
    options.AddPolicy("EnableCORS", builder =>
    {
        builder.AllowAnyHeader()
               .AllowAnyMethod()
               .AllowCredentials()
               .WithOrigins(configureService.GetClient(), configureService.GetServer());
    });
});
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

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

app.MapHub<TerminalHub>("/terminalHub");

app.Run();
