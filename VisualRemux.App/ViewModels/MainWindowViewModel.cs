using System.Collections.ObjectModel;
using System.Linq;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace VisualRemux.App.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    [ObservableProperty] private ObservableCollection<ToolViewModel> _userTools = [];
    [ObservableProperty] private ToolViewModel? _selectedTool = new LandingPageViewModel();
    
    [ObservableProperty] private ObservableCollection<ToolViewModel> _systemTools = [];
    [ObservableProperty] private OutputLogViewModel _outputLog;
    [ObservableProperty] private ApplicationSettingsViewModel _applicationSettings;

    public MainWindowViewModel(RemuxToolViewModel remuxToolViewModel, OutputLogViewModel outputLogViewModel,
        ApplicationSettingsViewModel applicationSettingsViewModel)
    {
        SystemTools.Add(outputLogViewModel);
        SystemTools.Add(applicationSettingsViewModel);

        UserTools.Add(remuxToolViewModel);

        SelectTool(UserTools.FirstOrDefault());
    }

    [RelayCommand]
    private void SelectTool(ToolViewModel? tool)
    {
        foreach (var availableTool in UserTools)
        {
            availableTool.IsSelected = false;
        }

        foreach (var availableTool in SystemTools)
        {
            availableTool.IsSelected = false;
        }

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