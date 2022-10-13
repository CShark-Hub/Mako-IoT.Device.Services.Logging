using System;
using System.Diagnostics;
using System.Reflection;
using MakoIoT.Device.Services.Logging.Configuration;
using Microsoft.Extensions.Logging;

namespace MakoIoT.Device.Services.Logging
{
    public class MakoIoTLogger : ILogger
    {
        public string LoggerName { get; set; }

        public LogLevel MinLogLevel { get; set; }

        public static MakoIoTLogger Create(string name, LoggerConfig loggerConfig)
        {
            return new MakoIoTLogger
            {
                LoggerName = name,
                MinLogLevel = loggerConfig.GetLogLevel(name)
            };
        }

        protected MakoIoTLogger()
        {
            LoggerName = nameof(MakoIoTLogger);
            MinLogLevel = LogLevel.Debug;
        }

        protected virtual void Write(string message)
        {
            Debug.WriteLine(message);
        }

        public virtual void Log(LogLevel logLevel, EventId eventId, string state, Exception exception, MethodInfo format)
        {
            if (logLevel >= MinLogLevel)
            {
                string msg;
                if (format == null)
                {
                    msg = exception == null ? state : $"{state} {exception}";
                }
                else
                {
                    msg = (string)format.Invoke(null, new object[] { LoggerName, logLevel, eventId, state, exception });
                }

                Write(msg);
            }
        }

        public bool IsEnabled(LogLevel logLevel) => logLevel >= MinLogLevel;

        public static void InitLogging()
        {
            LoggerExtensions.MessageFormatter = typeof(MakoIoTLogger).GetMethod("Formatter");
        }

        public static string Formatter(string className, LogLevel logLevel, EventId eventId, string state,
            Exception exception)
        {
            string level = null;
            switch (logLevel)
            {
                case LogLevel.Trace: level = "Trace"; break;
                case LogLevel.Debug: level = "Debug"; break;
                case LogLevel.Information: level = "Info"; break;
                case LogLevel.Warning: level = "Warning"; break;
                case LogLevel.Error: level = "Error"; break;
                case LogLevel.Critical: level = "Critical"; break;
                case LogLevel.None: level = "None"; break;
            }

            return $"[{level}][{className}] {state} {exception?.Message}";
        }
    }
}
