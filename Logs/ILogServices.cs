namespace Challenge.Backend.Logs
{
    public interface ILogServices<T>
    {
        public void LogInfo(string message);
        public void LogError(string message);
        public void LogWarning(string message);
        public void LogCritical(string message);
    }
}
