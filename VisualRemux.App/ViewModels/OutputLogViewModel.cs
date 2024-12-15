using System.Collections.ObjectModel;
using System.Linq;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using VisualRemux.App.Logging;

namespace VisualRemux.App.ViewModels;

public partial class OutputLogViewModel : ViewModelBase
{
    [ObservableProperty] private ObservableCollection<OutputLogEntryViewModel> _logEntries = [];
    [ObservableProperty] private ObservableCollection<OutputLogEntryViewModel> _selectedLogEntries = [];
    
    public OutputLogViewModel(ILogger logger)
    {
        DisplayName = "Output Log";

        logger.LogEntryAdded += (sender, logEntry) =>
        {
            _logEntries.Add(new OutputLogEntryViewModel(sender, logEntry));
        };
        
        LogEntries.CollectionChanged += (_, _) => ClearAllLogsCommand.NotifyCanExecuteChanged();
        SelectedLogEntries.CollectionChanged += (_, _) => ClearSelectedLogsCommand.NotifyCanExecuteChanged();
    }
    
    [RelayCommand(CanExecute = nameof(CanRemoveSelectedLogs))]
    private void ClearSelectedLogs()
    {
        // Make a defensive copy to avoid modifying the collection while removing items
        foreach (var file in SelectedLogEntries.ToList())
        {
            LogEntries.Remove(file);
        }

        SelectedLogEntries.Clear();
    }
    
    private bool CanRemoveSelectedLogs() => SelectedLogEntries.Count > 0;

    [RelayCommand(CanExecute = nameof(CanRemoveAllLogs))]
    private void ClearAllLogs()
    {
        LogEntries.Clear();
    }
    
    private bool CanRemoveAllLogs() => LogEntries.Count > 0;
}