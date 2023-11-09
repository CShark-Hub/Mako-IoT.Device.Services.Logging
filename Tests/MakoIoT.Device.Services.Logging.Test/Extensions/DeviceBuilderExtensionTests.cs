using MakoIoT.Device.Services.Logging.Mocks;
using Microsoft.Extensions.DependencyInjection;
using nanoFramework.TestFramework;
using Microsoft.Extensions.Logging;

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
            var imp = serviceProvider.GetService(typeof(ILogger));
            Assert.IsNotNull(imp);
        }
    }
}
