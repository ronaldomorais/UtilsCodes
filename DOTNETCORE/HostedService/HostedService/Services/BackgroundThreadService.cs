namespace HostedService.Services;

public class BackgroundThreadService : IBackgroundThreadService, IDisposable
{
    private readonly ILogger<BackgroundThreadService> _logger;
    private int executionCount = 0;
    private Timer? _timer = null;

    public BackgroundThreadService(ILoggerFactory loggerFactory)
    {
        _logger = loggerFactory.CreateLogger<BackgroundThreadService>();
    }

    public Task StartServiceAsync()
    {
        _logger.LogInformation("Starting Hosted Service...");
        _timer = new Timer(DoWork, null, TimeSpan.Zero, TimeSpan.FromSeconds(5));
        return Task.CompletedTask;
    }

    public Task StopServiceAsync()
    {
        _logger.LogInformation("Stoping Hosted Service...");
        _timer?.Change(Timeout.Infinite, 0);
        return Task.CompletedTask;

    }

    private void DoWork(object? state)
    {
        var count = Interlocked.Increment(ref executionCount);
        _logger.LogInformation($"Hosted Service running {count}");
    }

    public void Dispose()
    {
        _timer?.Dispose();
    }
}
