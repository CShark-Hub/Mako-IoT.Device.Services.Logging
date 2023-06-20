#  Mako-IoT.Device.Services.Logging
Configurable logger implementation based on Microsoft.Extensions.Logging.ILogger interface. Outputs log into console. Filters messages based on configured log level.

## Usage
```c#
using Microsoft.Extensions.Logging;

public class MyService
{
    private readonly ILogger _logger;
   
    public MyService(ILogger logger)
    {
        _logger = logger;
    }

    public void DoSomething()
    {
        try
        {
            _logger.LogDebug("Starting...");
           //   
        }
        catch(Exception ex)
        {
          _logger.LogError("Error occured", ex);
        }
    }
}
```
Register logger in [_DeviceBuilder_](https://github.com/CShark-Hub/Mako-IoT.Device)
```c#
DeviceBuilder.Create()
  .AddLogging(new LoggerConfig(LogLevel.Debug))
  .Build()
  .Start();
```
