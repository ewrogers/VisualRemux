using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace VisualRemux.App.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    private readonly OutputLogToolViewModel _outputLogTool = new();

    [ObservableProperty] private ObservableCollection<ToolViewModel> _tools =
    [
        new RemuxToolViewModel()
    ];

    [ObservableProperty] private ToolViewModel? _selectedTool = new LandingPageViewModel();

    public MainWindowViewModel()
    {
        SelectTool(_tools[0]);
    }

    [RelayCommand]
    private void SelectTool(ToolViewModel? tool) => SelectedTool = tool;

    [RelayCommand]
    private void ShowOutputLog()
    {
        SelectTool(_outputLogTool);
    }
}