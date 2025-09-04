using Serilog;

namespace Challenge.Backend.Logs
{
    public class LogServices : ILogServices<LogServices>
    {
        public LogServices()
        {
        }

        public void LogCritical(string message)
        {
            Log.Fatal(message);
        }

        public void LogError(string message)
        {
            Log.Error(message);
        }

        public void LogInfo(string message)
        {
            Log.Information(message);
        }

        public void LogWarning(string message)
        {
            Log.Warning(message);
        }
    }
}
