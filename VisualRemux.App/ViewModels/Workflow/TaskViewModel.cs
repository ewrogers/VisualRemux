using CommunityToolkit.Mvvm.ComponentModel;

namespace VisualRemux.App.ViewModels.Workflow;

public abstract partial class TaskViewModel : ViewModelBase
{
    [ObservableProperty] private bool _isCompleted;
}