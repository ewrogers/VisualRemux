using VisualRemux.App.Logging;

namespace VisualRemux.App.ViewModels;

public class OutputLogEntryViewModel : ViewModelBase
{
    private readonly LogEntry _logEntry;

    public OutputLogEntryViewModel(LogEntry logEntry)
    {
        _logEntry = logEntry;
    }
}