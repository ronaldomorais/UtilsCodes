namespace WebApiBackgroundTImer.cs.Options
{
    public class ServiceConfigOption
    {
        public const string SESSION_NAME = "ServiceConfig";
        public string? LogPath { get; set; }
        public int PoolingIntervalInSec { get; set; }
    }
}
