using log4net;
using log4net.Config;

public static class OpenCartLog
{
    private static readonly ILog log = LogManager.GetLogger(typeof(OpenCartLog));

    static OpenCartLog()
    {
        var logConfig = new FileInfo("log4net.config");
        XmlConfigurator.Configure(logConfig);
    }

    public static void Info(string message) { if (log.IsInfoEnabled) log.Info(message); }
    public static void Debug(string message) { if (log.IsDebugEnabled) log.Debug(message); }
    public static void Warning(string message) { if (log.IsWarnEnabled) log.Warn(message); }
    public static void Error(string message, Exception ex = null) { if (log.IsErrorEnabled) log.Error(message, ex); }

}
