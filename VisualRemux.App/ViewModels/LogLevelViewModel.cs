using CommunityToolkit.Mvvm.ComponentModel;
using VisualRemux.App.Logging;

namespace VisualRemux.App.ViewModels;

public partial class LogLevelViewModel : ViewModelBase
{
    public LogLevel LogLevel { get; }
    
    [ObservableProperty] private bool _isEnabled;
    
    public bool IsDebug => LogLevel == LogLevel.Debug;
    public bool IsInfo => LogLevel == LogLevel.Info;
    public bool IsWarn => LogLevel == LogLevel.Warn;
    public bool IsError => LogLevel == LogLevel.Error;

    public LogLevelViewModel(LogLevel logLevel)
    {
        LogLevel = logLevel;
    }
}