using System;

namespace VisualRemux.App.Logging;

public interface ILogger
{
    event EventHandler<LogEntry>? LogEntryAdded;

    ILogger Child<T>(T sender) where T : notnull;

    void LogDebug(string message) => Log(LogLevel.Debug, message);
    void LogInfo(string message) => Log(LogLevel.Info, message);
    void LogWarn(string message) => Log(LogLevel.Warn, message);
    void LogError(Exception exception, string message) => Log(LogLevel.Error, message, exception);

    void Log(LogLevel logLevel, string message, Exception? exception = null);
}