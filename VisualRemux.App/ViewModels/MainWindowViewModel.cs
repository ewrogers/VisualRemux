using System.Collections.ObjectModel;
using System.Linq;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace VisualRemux.App.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    private readonly OutputLogToolViewModel _outputLogTool = new();

    [ObservableProperty] private ObservableCollection<ToolViewModel> _tools = [];

    [ObservableProperty] private ToolViewModel? _selectedTool = new LandingPageViewModel();

    public MainWindowViewModel(RemuxToolViewModel remuxToolViewModel)
    {
        Tools.Add(remuxToolViewModel);

        SelectTool(_tools.FirstOrDefault());
    }

    [RelayCommand]
    private void SelectTool(ToolViewModel? tool) => SelectedTool = tool;

    [RelayCommand]
    private void ShowOutputLog()
    {
        SelectTool(_outputLogTool);
    }
}