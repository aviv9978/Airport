using Airport.Infrastracture;
using FlightSimulator.Configures;
using FlightSimulator.Extensions;
using Microsoft.EntityFrameworkCore;
using Core.Hubs;
using Serilog;
using System.Text.Json.Serialization;
using Hangfire;
using Hangfire.SqlServer;

var builder = WebApplication.CreateBuilder(args);
//var serviceProvider = builder.Services.BuildServiceProvider();
var connectionString = builder.Configuration["ConnectionStrings:myAirport"];
// Add services to the container.

builder.Services.AddDbContext<AirportDataContext>(options => options
.UseSqlServer(connectionString), ServiceLifetime.Scoped);
builder.Services.AddApplicationServices();
//GlobalConfiguration.Configuration.UseActivator(new FlightJobActivator(serviceProvider));

//builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory())
//    .ConfigureContainer<ContainerBuilder>(builder =>
//{
//    builder.RegisterModule(new AutofacBusinessModule());
//}); 

//builder.RegisterType<Database>()
//    .InstancePerBackgroundJob()
//    .InstancePerHttpRequest();

builder.Services.AddControllers().AddJsonOptions(o => o.JsonSerializerOptions
                        .ReferenceHandler = ReferenceHandler.IgnoreCycles);


builder.Services.AddHangfire(configuration => configuration
        .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
        .UseSimpleAssemblyNameTypeSerializer()
        .UseRecommendedSerializerSettings()
        .UseSqlServerStorage(connectionString, new SqlServerStorageOptions
        {
            CommandBatchMaxTimeout = TimeSpan.FromMinutes(5),
            SlidingInvisibilityTimeout = TimeSpan.FromMinutes(5),
            QueuePollInterval = TimeSpan.Zero,
            UseRecommendedIsolationLevel = true,
            DisableGlobalLocks = true
        }));

builder.Services.AddHangfireServer();


builder.Services.AddSwaggerGen();


//To enable client to communicate with server
var configureService = new ConfigureService();
builder.Services.AddSignalR(options =>
{
    options.EnableDetailedErrors = true;
});

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

var logger = new LoggerConfiguration()
  .ReadFrom.Configuration(builder.Configuration)
  .Enrich.WithThreadId()
  .Enrich.FromLogContext()
  .CreateLogger();
builder.Logging.ClearProviders();
builder.Logging.AddSerilog(logger);

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());


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

var options = new BackgroundJobServerOptions { WorkerCount = 1 };
app.UseHangfireServer(options);
app.UseHangfireDashboard();
app.MapHangfireDashboard();
app.Run();
