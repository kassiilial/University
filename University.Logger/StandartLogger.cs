using System;
using NLog;
using MS =  Microsoft.Extensions.Logging;

namespace University.Logger
{
    public class StandartLogger<T>:MS.ILogger<T>
    {

        private readonly NLog.Logger _nlog = LogManager.GetCurrentClassLogger();
        public void Log<TState>(MS.LogLevel logLevel, MS.EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            
            var ll = logLevel == MS.LogLevel.Debug ? LogLevel.Debug
                : logLevel == MS.LogLevel.Trace ? LogLevel.Trace
                : logLevel == MS.LogLevel.Critical ? LogLevel.Fatal
                : logLevel == MS.LogLevel.Error ? LogLevel.Error
                : logLevel == MS.LogLevel.Information ? LogLevel.Info
                : logLevel == MS.LogLevel.Warning ? LogLevel.Warn
                : LogLevel.Off;
            _nlog.Log(ll, formatter(state, exception));
        }
        public bool IsEnabled(MS.LogLevel logLevel)
        {
            return true;
        }
        public IDisposable BeginScope<TState>(TState state)
        {
            return default;
        }
    }
}