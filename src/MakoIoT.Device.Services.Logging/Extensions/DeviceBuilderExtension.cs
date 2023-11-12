using MakoIoT.Device.Services.Interface;
using MakoIoT.Device.Services.Logging.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;

namespace MakoIoT.Device.Services.Logging.Extensions
{
    public static class DeviceBuilderExtension
    {
        public static IDeviceBuilder AddLogging(this IDeviceBuilder builder)
        {
            return AddLogging(builder, new LoggerConfig(LogLevel.Debug));
        }

        public static IDeviceBuilder AddLogging(this IDeviceBuilder builder, LoggerConfig loggerConfig)
        {
            builder.Services.AddSingleton(typeof(LoggerConfig), loggerConfig);
            builder.Services.AddTransient(typeof(ILogger), typeof(MakoIoTLogger));
            MakoIoTLogger.InitLogging();
            return builder;
        }
    }
}
