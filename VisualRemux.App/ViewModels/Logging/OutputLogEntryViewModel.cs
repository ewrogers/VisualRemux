using System;
using VisualRemux.App.Logging;

namespace VisualRemux.App.ViewModels.Logging;

public class OutputLogEntryViewModel : ViewModelBase
{
    private readonly object? _sender;
    private readonly LogEntry _logEntry;

    public string SenderName => _sender is ViewModelBase vm ? vm.DisplayName : _sender?.GetType().Name ?? "Unknown";

    public DateTime Timestamp => _logEntry.Timestamp;
    public LogLevel LogLevel => _logEntry.Level;
    
    public bool IsDebug => LogLevel == LogLevel.Debug;
    public bool IsInfo => LogLevel == LogLevel.Info;
    public bool IsWarn => LogLevel == LogLevel.Warn;
    public bool IsError => LogLevel == LogLevel.Error;
    
    public string Message => _logEntry.Message;

    public string? ExceptionType => _logEntry.Exception?.GetType().Name;
    public string? Exception => _logEntry.Exception?.Message;

    public OutputLogEntryViewModel(object? sender, LogEntry logEntry)
    {
        _sender = sender;
        _logEntry = logEntry;
    }
}