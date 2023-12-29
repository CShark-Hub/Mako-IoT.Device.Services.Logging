using System;
using System.Reflection;
using MakoIoT.Device.Services.Interface;
using MakoIoT.Device.Services.Logging.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MakoIoT.Device.Services.Logging
{
    internal sealed class MakoIoTLogger : ILog
    {
        private readonly ILogSink[] _sinks;
        private LogEventLevel MinLogEventLevel { get; }

        public MakoIoTLogger(IServiceProvider serviceProvider)
        {
            var sinks = serviceProvider.GetServices(typeof(ILogSink));
            var sinksArray = new ILogSink[sinks.Length];
            for (int i = 0; i < sinks.Length; i++)
            {
                sinksArray[i] = (ILogSink)sinks[i];
            }
            _sinks = sinksArray;

            var loggerConfig = serviceProvider.GetService(typeof(LoggerConfig)) as LoggerConfig ?? new LoggerConfig();
            MinLogEventLevel = loggerConfig.GetLogLevel(nameof(MakoIoTLogger));
        }

        public void Trace(Exception exception, string message, MethodInfo format)
        {
            Log(LogEventLevel.Trace, message, exception, format);
        }

        public void Trace(string message)
        {
            Log(LogEventLevel.Trace, message, null, null);
        }

        public void Trace(Exception exception)
        {
            Log(LogEventLevel.Trace, null, exception, null);
        }

        public void Trace(Exception exception, string message)
        {
            Log(LogEventLevel.Trace, message, null, null);
        }

        public void Information(Exception exception, string message, MethodInfo format)
        {
            Log(LogEventLevel.Information, message, exception, format);
        }

        public void Information(string message)
        {
            Log(LogEventLevel.Information, message, null, null);
        }

        public void Information(Exception exception)
        {
            Log(LogEventLevel.Information, null, exception, null);
        }

        public void Information(Exception exception, string message)
        {
            Log(LogEventLevel.Information, message, null, null);
        }

        public void Warning(Exception exception, string message, MethodInfo format)
        {
            Log(LogEventLevel.Warning, message, exception, format);
        }

        public void Warning(string message)
        {
            Log(LogEventLevel.Warning, message, null, null);
        }

        public void Warning(Exception exception)
        {
            Log(LogEventLevel.Warning, null, exception, null);
        }

        public void Warning(Exception exception, string message)
        {
            Log(LogEventLevel.Warning, message, null, null);
        }

        public void Error(Exception exception, string message, MethodInfo format)
        {
            Log(LogEventLevel.Error, message, exception, format);
        }

        public void Error(string message)
        {
            Log(LogEventLevel.Error, message, null, null);
        }

        public void Error(Exception exception)
        {
            Log(LogEventLevel.Error, null, exception, null);
        }

        public void Error(string message, Exception exception)
        {
            Log(LogEventLevel.Error, message, null, null);
        }


        public void Critical(Exception exception, string message, MethodInfo format)
        {
            Log(LogEventLevel.Critical, message, exception, format);
        }

        public void Critical(string message)
        {
            Log(LogEventLevel.Critical, message, null, null);
        }

        public void Critical(Exception exception)
        {
            Log(LogEventLevel.Critical, null, exception, null);
        }

        public void Critical(Exception exception, string message)
        {
            Log(LogEventLevel.Critical, message, null, null);
        }

        private bool IsEnabled(LogEventLevel logLevel)
        {
            if (logLevel >= MinLogEventLevel)
            {
                return true;
            }

            return false;
        }

        private void Log(LogEventLevel logLevel, string state, Exception exception, MethodInfo format)
        {
            if (!IsEnabled(logLevel))
            {
                return;
            }

            if (_sinks.Length == 0)
            {
                Console.WriteLine("[Critical] No logger sinks provided.");
                return;
            }

            var msg = format == null
                ? Formatter(logLevel, state, exception)
                : (string)format.Invoke(null, new object[] { logLevel, state, exception });

            foreach (var logger in _sinks)
            {
                try
                {
                    logger.Log(msg);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("[Critical] Error when calling logger.");
                    Console.WriteLine(ex.ToString());
                }
            }
        }
        
        private static string Formatter(LogEventLevel logLevel, string state, Exception exception)
        {
            var level = logLevel switch
            {
                LogEventLevel.Trace => "Trace",
                LogEventLevel.Information => "Information",
                LogEventLevel.Warning => "Warning",
                LogEventLevel.Error => "Error",
                LogEventLevel.Critical => "Critical",
                _ => throw new ArgumentOutOfRangeException(nameof(logLevel))
            };

            return $"[{level}][{DateTime.UtcNow}] {state} {exception?.Message}";
        }
    }
}
