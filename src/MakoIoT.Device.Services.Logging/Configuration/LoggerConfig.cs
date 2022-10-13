using System.Collections;
using Microsoft.Extensions.Logging;

namespace MakoIoT.Device.Services.Logging.Configuration
{
    public class LoggerConfig
    {
        public LogLevel DefaultMinLogLevel { get; }
        public Hashtable Loggers { get; }

        public LoggerConfig(LogLevel defaultMinLogLevel)
        {
            DefaultMinLogLevel = defaultMinLogLevel;
        }

        public LoggerConfig(LogLevel defaultMinLogLevel, Hashtable loggers) : this(defaultMinLogLevel)
        {
            Loggers = loggers;
        }

        public LoggerConfig() : this(LogLevel.Information)
        {

        }

        public LogLevel GetLogLevel(string name)
        {
            if (Loggers != null && Loggers.Contains(name))
                return (LogLevel)Loggers[name];
            return DefaultMinLogLevel;
        }
    }
}
