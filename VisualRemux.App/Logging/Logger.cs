using System;
using System.Diagnostics;

namespace VisualRemux.App.Logging;

public class Logger : ILogger
{
    private readonly object? _sender;
    private readonly Logger? _parent;

    public event EventHandler<LogEntry>? LogEntryAdded;

    public Logger()
    {
        AttachToConsoleOutput();
    }

    private Logger(object sender, Logger parent)
    {
        _sender = sender;
        _parent = parent;
    }

    public ILogger Child<T>(T sender) where T : notnull
        => new Logger(sender, this);

    public void Log(LogLevel logLevel, string message, Exception? exception = null)
    {
        var entry = new LogEntry(logLevel, message, exception);
        NotifyLogEntryAdded(_sender ?? this, entry);
    }

    private void NotifyLogEntryAdded(object sender, LogEntry logEntry)
    {
        LogEntryAdded?.Invoke(sender, logEntry);
        _parent?.NotifyLogEntryAdded(sender, logEntry);
    }

    [Conditional("DEBUG")]
    private void AttachToConsoleOutput()
    {
        LogEntryAdded += (sender, logEntry) =>
        {
            var senderName = sender?.GetType().Name ?? string.Empty;

            var logLine =
                $"[{logEntry.Timestamp:HH:mm:ss.fff}] {logEntry.Level.ToString().ToUpperInvariant()} {{{senderName}}}: {logEntry.Message}";

            Console.WriteLine(logLine);

            if (logEntry.Exception is null)
            {
                return;
            }

            var exceptionLine = $"{logEntry.Exception.GetType().Name}: {logEntry.Exception.Message}";
            Console.WriteLine(exceptionLine);
        };
    }
}