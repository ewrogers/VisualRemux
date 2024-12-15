using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using VisualRemux.App.Logging;

namespace VisualRemux.App.ViewModels;

public partial class OutputLogViewModel : ViewModelBase
{
    [ObservableProperty] private ObservableCollection<OutputLogEntryViewModel> _logEntries = [];

    public OutputLogViewModel(ILogger logger)
    {
        DisplayName = "Output Log";

        logger.LogEntryAdded += (sender, logEntry) =>
        {
            _logEntries.Add(new OutputLogEntryViewModel(sender, logEntry));
        };
    }
}