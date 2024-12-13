using System.Collections.ObjectModel;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;

namespace VisualRemux.App.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
    public ObservableCollection<ToolViewModel> Tools { get; } =
    [
        new RemuxToolViewModel()
    ];

    private ToolViewModel? _selectedTool = new LandingPageViewModel();

    public ToolViewModel? SelectedTool
    {
        get => _selectedTool;
        set => SetProperty(ref _selectedTool, value);
    }

    public ICommand SelectToolCommand => new RelayCommand<ToolViewModel>(SelectTool);

    private void SelectTool(ToolViewModel? tool) => SelectedTool = tool;
}