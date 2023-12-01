using MakoIoT.Device.Services.Interface;
using MakoIoT.Device.Services.Logging.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MakoIoT.Device.Services.Logging.Extensions
{
    public static class DeviceBuilderExtension
    {
        public static IDeviceBuilder AddLogging(this IDeviceBuilder builder)
        {
            return AddLogging(builder, new LoggerConfig(LogEventLevel.Trace));
        }
        
        public static IDeviceBuilder AddLogging(this IDeviceBuilder builder, LoggerConfig loggerConfig)
        {
            return AddLogging(builder, loggerConfig, new ILogSink[] { new ConsoleSink() });
        }

        public static IDeviceBuilder AddLogging(this IDeviceBuilder builder, LoggerConfig loggerConfig, ILogSink[] sinks)
        {
            builder.Services.AddSingleton(typeof(LoggerConfig), loggerConfig);
            builder.Services.AddTransient(typeof(ILog), typeof(MakoIoTLogger));

            if (sinks != null)
            {
                foreach (var sink in sinks)
                {
                    builder.Services.AddSingleton(typeof(ILogSink), sink);
                }   
            }
            
            return builder;
        }
    }
}
