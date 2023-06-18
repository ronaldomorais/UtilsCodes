using WebApiBackgroundTImer.cs.Services;
using Microsoft.AspNetCore.Http;

namespace WebApiBackgroundTImer.cs.Applications
{
    public static class EndpointsApp
    {
        public static void BackgroundTimerServiceApi(this WebApplication app)
        {
            app.MapGet("/", () => $"Welcome API Background Timer {DateTime.Now}");

            app.MapGet("/counter", async (IBackgroundTimerService backgroundTimerService) => 
            {
                int counterValue = backgroundTimerService.CounterValue;

                return Results.Ok(counterValue);
            });
        }
    }
}
