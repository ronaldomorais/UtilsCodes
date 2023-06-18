using System.Runtime.CompilerServices;
using WebApiBackgroundTImer.cs.Services;

namespace WebApiBackgroundTImer.cs.Applications
{
    public static class BackgroundTimerApp
    {
        public static void BackgroundTimerAppInitialize(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            scope.ServiceProvider.GetService<IBackgroundTimerService>()?.StartAsync();
        }
    }
}
