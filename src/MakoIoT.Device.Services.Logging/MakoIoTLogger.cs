using System;
using System.Reflection;
using MakoIoT.Device.Services.Logging.Configuration;
using Microsoft.Extensions.Logging;

namespace MakoIoT.Device.Services.Logging
{
    public class MakoIoTLogger : ILogger
    {
        public string LoggerName { get; }

        public LogLevel MinLogLevel { get; }

        public MakoIoTLogger(LoggerConfig loggerConfig)
        {
            LoggerName = nameof(MakoIoTLogger);
            MinLogLevel = loggerConfig.GetLogLevel(LoggerName); // TODO: logger config per name?
        }

        protected virtual void Write(string message)
        {
            Console.WriteLine(message);
        }

        public virtual void Log(LogLevel logLevel, EventId eventId, string state, Exception exception, MethodInfo format)
        {
            if (!IsEnabled(logLevel))
            {
                return;
            }

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

        public bool IsEnabled(LogLevel logLevel) => logLevel >= MinLogLevel;

        public static void InitLogging()
        {
            LoggerExtensions.MessageFormatter = typeof(MakoIoTLogger).GetMethod(nameof(Formatter));
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
