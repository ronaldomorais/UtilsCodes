using Serilog;
using WebApiBackgroundTImer.cs.Options;
using WebApiBackgroundTImer.cs.Services;

namespace WebApiBackgroundTImer.cs.Extensions;

public static class ServiceCollectionsExtensions
{
    public static WebApplicationBuilder AddBackgroundTimerService(this WebApplicationBuilder builder)
    {
        builder.Services.AddSingleton<IBackgroundTimerService, BackgroundTimerService>();
        return builder;
    }

    public static WebApplicationBuilder AddSeriLogService(this WebApplicationBuilder builder)
    {
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
        return builder;
    }
}
