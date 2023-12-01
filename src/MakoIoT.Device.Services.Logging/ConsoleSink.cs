using System;
using MakoIoT.Device.Services.Interface;

namespace MakoIoT.Device.Services.Logging
{
    internal sealed class ConsoleSink : ILogSink
    {
        public void Log(string message)
        {
            Console.WriteLine(message);
        }
    }
}