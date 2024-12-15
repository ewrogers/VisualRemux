using VisualRemux.App.Logging;

namespace VisualRemux.App.ViewModels;

public class OutputLogEntryViewModel : ViewModelBase
{
    private readonly object? _sender;
    private readonly LogEntry _logEntry;

    public OutputLogEntryViewModel(object? sender, LogEntry logEntry)
    {
        _sender = sender;
        _logEntry = logEntry;
    }
}