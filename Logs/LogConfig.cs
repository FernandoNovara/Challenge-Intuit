using Serilog;

namespace Challenge.Backend.Logs
{
    public class LogConfig
    {
        public static void ConfigureLogs()
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Override("Microsoft",Serilog.Events.LogEventLevel.Warning)
                .MinimumLevel.Override("System",Serilog.Events.LogEventLevel.Warning)
                .MinimumLevel.Debug()
                .Enrich.FromLogContext()
                .WriteTo.Console()
                .WriteTo.File("../LogsFiles/Log-.txt", rollingInterval: RollingInterval.Day)
                .CreateLogger();
        }
    }
}
