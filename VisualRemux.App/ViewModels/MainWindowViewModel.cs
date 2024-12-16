using System.Collections.ObjectModel;
using System.Linq;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace VisualRemux.App.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    [ObservableProperty] private ObservableCollection<ToolViewModel> _tools = [];
    [ObservableProperty] private ViewModelBase? _selectedTool = new LandingPageViewModel();


    [ObservableProperty] private OutputLogViewModel _outputLog;

    public MainWindowViewModel(RemuxToolViewModel remuxToolViewModel, OutputLogViewModel outputLogViewModel)
    {
        OutputLog = outputLogViewModel;

        Tools.Add(remuxToolViewModel);

        SelectTool(_tools.FirstOrDefault());
    }

    [RelayCommand]
    private void SelectTool(ViewModelBase? tool) => SelectedTool = tool;

    [RelayCommand]
    private void ShowOutputLog()
    {
        SelectTool(OutputLog);
    }
}