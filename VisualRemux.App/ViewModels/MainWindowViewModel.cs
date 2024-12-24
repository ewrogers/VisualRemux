using System.Collections.ObjectModel;
using System.Linq;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace VisualRemux.App.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    [ObservableProperty] private ObservableCollection<ToolViewModel> _tools = [];
    [ObservableProperty] private ToolViewModel? _selectedTool = new LandingPageViewModel();
    
    [ObservableProperty] private OutputLogViewModel _outputLog;
    [ObservableProperty] private ApplicationSettingsViewModel _applicationSettings;

    public MainWindowViewModel(RemuxToolViewModel remuxToolViewModel, OutputLogViewModel outputLogViewModel,
        ApplicationSettingsViewModel applicationSettingsViewModel)
    {
        OutputLog = outputLogViewModel;
        ApplicationSettings = applicationSettingsViewModel;

        Tools.Add(remuxToolViewModel);

        SelectTool(_tools.FirstOrDefault());
    }

    [RelayCommand]
    private void SelectTool(ToolViewModel? tool)
    {
        foreach (var availableTool in Tools)
        {
            availableTool.IsSelected = false;
        }

        OutputLog.IsSelected = false;
        ApplicationSettings.IsSelected = false;

        SelectedTool = tool;

        if (tool is not null)
        {
            tool.IsSelected = true;
        }
    }

    [RelayCommand]
    private void ShowOutputLog()
    {
        SelectTool(OutputLog);
    }
    
    [RelayCommand]
    private void ShowApplicationSettings()
    {
        SelectTool(ApplicationSettings);
    }
}