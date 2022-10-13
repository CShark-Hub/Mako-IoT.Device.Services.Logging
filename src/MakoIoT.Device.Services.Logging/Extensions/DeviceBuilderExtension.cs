using System.Collections;
using MakoIoT.Device.Services.DependencyInjection;
using MakoIoT.Device.Services.Interface;
using MakoIoT.Device.Services.Logging.Configuration;
using Microsoft.Extensions.Logging;

namespace MakoIoT.Device.Services.Logging.Extensions
{
    public static class DeviceBuilderExtension
    {
        public static IDeviceBuilder AddLogging(this IDeviceBuilder builder)
        {
            return AddLogging(builder, new LoggerConfig(LogLevel.Debug, new Hashtable
            {
                { "DI", LogLevel.Debug }
            }));
        }

        public static IDeviceBuilder AddLogging(this IDeviceBuilder builder, LoggerConfig loggerConfig)
        {
            DI.Register(typeof(ILogger), typeof(MakoIoTLogger), Lifetime.Transient, containingClass =>
                MakoIoTLogger.Create(containingClass.Name, loggerConfig));

            MakoIoTLogger.InitLogging();
            DI.Logger = MakoIoTLogger.Create("DI", loggerConfig);

            return builder;
        }
    }
}
