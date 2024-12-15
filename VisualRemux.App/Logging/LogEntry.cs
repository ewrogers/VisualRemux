using System;

namespace VisualRemux.App.Logging;

public record LogEntry(LogLevel Level, string Message, Exception? Exception = null)
{
    public DateTime Timestamp { get; } = DateTime.Now;
}