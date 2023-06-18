using HostedService.Options;
using HostedService.Services;
using Serilog;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var configuration = builder.Configuration;
ServiceConfigOption serviceConfigOption = new ServiceConfigOption();

configuration.GetSection(ServiceConfigOption.SESSION_NAME).Bind(serviceConfigOption);
string? logPath = "";

if (serviceConfigOption?.LogPath?.EndsWith("\\") == false)
    logPath = $"{serviceConfigOption?.LogPath}\\.log";
else
    logPath = $"{serviceConfigOption?.LogPath}.log";

Log.Logger = new LoggerConfiguration()
     .WriteTo.Console(Serilog.Events.LogEventLevel.Debug)
     .WriteTo.File(logPath, rollingInterval: RollingInterval.Hour, outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff} [{Level}] {Message}{NewLine}{Exception}")
     .WriteTo.EventLog(source: "HostedService", logName: "Application", manageEventSource: false)
     .MinimumLevel.Debug()
     .Enrich.FromLogContext()
     .CreateLogger();

builder.Services.AddSingleton<IBackgroundThreadService, BackgroundThreadService>();

var app = builder.Build();

using var scope = app.Services.CreateScope();
scope.ServiceProvider.GetService<IBackgroundThreadService>()?.StartServiceAsync();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.Run();
