using System.Collections;
using MakoIoT.Device.Services.Interface;

namespace MakoIoT.Device.Services.Logging.Configuration
{
    public class LoggerConfig
    {
        public LogEventLevel DefaultMinLogLevel { get; }
        public Hashtable Loggers { get; }

        public LoggerConfig(LogEventLevel defaultMinLogLevel)
        {
            DefaultMinLogLevel = defaultMinLogLevel;
        }

        public LoggerConfig(LogEventLevel defaultMinLogLevel, Hashtable loggers) : this(defaultMinLogLevel)
        {
            Loggers = loggers;
        }

        public LoggerConfig() : this(LogEventLevel.Information)
        {

        }

        public LogEventLevel GetLogLevel(string name)
        {
            if (Loggers != null && Loggers.Contains(name))
                return (LogEventLevel)Loggers[name];

            return DefaultMinLogLevel;
        }
    }
}
