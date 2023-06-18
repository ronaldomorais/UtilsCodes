namespace WebApiBackgroundTImer.cs.Services;

public class BackgroundTimerService : IBackgroundTimerService, IDisposable
{
    private readonly ILogger<BackgroundTimerService> _logger;
    private Timer? _timer = null;
    private int executionCount = 0;

    public BackgroundTimerService(ILoggerFactory loggerFactory)
    {
        _logger = loggerFactory.CreateLogger<BackgroundTimerService>();
    }

    public int CounterValue => executionCount;

    public Task StartAsync()
    {
        _logger.LogInformation("Starting Hosted Service...");
        _timer = new Timer(DoWork, null, TimeSpan.Zero, TimeSpan.FromSeconds(5));
        return Task.CompletedTask;
    }

    public Task StopAsync()
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
