using CommunityToolkit.Mvvm.ComponentModel;

namespace VisualRemux.App.ViewModels;

public abstract partial class ToolViewModel : ViewModelBase
{
    [ObservableProperty] private bool _isSelected;
}