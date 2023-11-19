using MakoIoT.Device.Services.Interface;
using MakoIoT.Device.Services.Logging.Mocks;
using Microsoft.Extensions.DependencyInjection;
using nanoFramework.TestFramework;

namespace MakoIoT.Device.Services.Logging.Extensions
{
    [TestClass]
    public class DeviceBuilderExtensionTests
    {
        [TestMethod]
        public void AddLogging_Should_InitializeLogger()
        {
            var mockBuilder = new DeviceBuilderMock();

            DeviceBuilderExtension.AddLogging(mockBuilder);

            var serviceProvider = mockBuilder.Services.BuildServiceProvider();
            var imp = serviceProvider.GetService(typeof(ILog));
            Assert.IsNotNull(imp);
        }
    }
}
