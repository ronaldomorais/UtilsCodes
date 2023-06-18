namespace WebApiBackgroundTImer.cs.Services
{
    public interface IBackgroundTimerService
    {
        int CounterValue { get; }
        Task StartAsync();
        Task StopAsync();
    }
}
