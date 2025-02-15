using Serilog;

namespace Opencart.ua.Tools.LogsHelpers
{
    public static class OpenCartSeriLog
    {
        private static readonly string _logTemplate = "[{Timestamp:MM/dd/yyyy HH:mm:ss} {Level:u3}] {Message:lj}{NewLine}{Exception}";

        static OpenCartSeriLog()
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.Debug(outputTemplate: _logTemplate)
                .WriteTo.Console(outputTemplate: _logTemplate)
                .WriteTo.File("Logs/log.txt", rollingInterval: RollingInterval.Day,
                                outputTemplate: _logTemplate)
                .CreateLogger();
        }

        public static void Info(string message) => Log.Information(message);
        public static void Debug(string message) => Log.Debug(message);
        public static void Warning(string message) => Log.Warning(message);

        public static void Error(string message, Exception ex = null)
        {
            if (ex == null)
                Log.Error(message);
            else
                Log.Error(ex, message);
        }
    }
}
