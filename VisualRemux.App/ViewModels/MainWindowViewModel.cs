using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;

namespace VisualRemux.App.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    [ObservableProperty]
    private ObservableCollection<ToolViewModel> _tools =
    [
        new RemuxToolViewModel()
    ];

    [ObservableProperty]
    private ToolViewModel? _selectedTool = new LandingPageViewModel();

    public MainWindowViewModel()
    {
        SelectTool(_tools[0]);
    }
    
    public void SelectTool(ToolViewModel? tool) => SelectedTool = tool;
}