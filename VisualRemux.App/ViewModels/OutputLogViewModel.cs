using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using VisualRemux.App.Logging;

namespace VisualRemux.App.ViewModels;

public partial class OutputLogViewModel : ViewModelBase
{
    private readonly List<OutputLogEntryViewModel> _logEntries = [];
    
    [ObservableProperty] private ObservableCollection<OutputLogEntryViewModel> _filteredLogEntries = [];
    [ObservableProperty] private ObservableCollection<OutputLogEntryViewModel> _selectedLogEntries = [];

    [ObservableProperty] private ObservableCollection<LogLevelViewModel> _logLevels =
    [
        new(LogLevel.Info) { IsEnabled = true },
        new(LogLevel.Warn) { IsEnabled = true },
        new(LogLevel.Error) { IsEnabled = true },
        new(LogLevel.Debug) { IsEnabled = false }
    ];
    
    public OutputLogViewModel(ILogger logger)
    {
        DisplayName = "Output Log";

        logger.LogEntryAdded += AddLogEntry;
        
        FilteredLogEntries.CollectionChanged += (_, _) => ClearAllLogsCommand.NotifyCanExecuteChanged();
        SelectedLogEntries.CollectionChanged += (_, _) => ClearSelectedLogsCommand.NotifyCanExecuteChanged();

        foreach (var logLevel in LogLevels)
        {
            logLevel.PropertyChanged += (_, e) =>
            {
                if (e.PropertyName == nameof(LogLevelViewModel.IsEnabled))
                {
                    ApplyFilter();
                }
            };
        }
    }

    public void AddLogEntry(object? sender, LogEntry logEntry)
    {
        var newLogEntry = new OutputLogEntryViewModel(sender, logEntry);
        _logEntries.Add(newLogEntry);

        if (IsLogLevelEnabled(logEntry.Level))
        {
            FilteredLogEntries.Add(newLogEntry);
        }
    }

    [RelayCommand(CanExecute = nameof(CanRemoveSelectedLogs))]
    private void ClearSelectedLogs()
    {
        // Make a defensive copy to avoid modifying the collection while removing items
        foreach (var file in SelectedLogEntries.ToList())
        {
            FilteredLogEntries.Remove(file);
            _logEntries.Remove(file);
        }

        SelectedLogEntries.Clear();
    }
    
    private bool CanRemoveSelectedLogs() => SelectedLogEntries.Count > 0;

    [RelayCommand(CanExecute = nameof(CanRemoveAllLogs))]
    private void ClearAllLogs()
    {
        FilteredLogEntries.Clear();
        _logEntries.Clear();
    }
    
    private bool CanRemoveAllLogs() => _logEntries.Count > 0;

    private void ApplyFilter()
    {
        FilteredLogEntries.Clear();

        foreach (var logEntry in _logEntries.Where(logEntry => IsLogLevelEnabled(logEntry.LogLevel)))
        {
            FilteredLogEntries.Add(logEntry);
        }
    }

    private bool IsLogLevelEnabled(LogLevel logLevel) =>
        LogLevels.Any(level => level.LogLevel == logLevel && level.IsEnabled);
}