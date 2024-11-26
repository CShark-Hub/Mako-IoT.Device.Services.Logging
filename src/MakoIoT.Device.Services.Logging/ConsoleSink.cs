using System;
using MakoIoT.Device.Services.Interface;

namespace MakoIoT.Device.Services.Logging
{
    public sealed class ConsoleSink : ILogSink
    {
        public void Log(string message)
        {
            Console.WriteLine(message);
        }
    }
}