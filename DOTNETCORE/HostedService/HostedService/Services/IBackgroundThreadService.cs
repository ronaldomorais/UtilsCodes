namespace HostedService.Services
{
    public interface IBackgroundThreadService
    {
        Task StartServiceAsync();
        Task StopServiceAsync(); 
    }
}
