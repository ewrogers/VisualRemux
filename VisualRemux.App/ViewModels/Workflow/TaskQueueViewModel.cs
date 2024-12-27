using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;

namespace VisualRemux.App.ViewModels.Workflow;

public partial class TaskQueueViewModel : ToolViewModel
{
    [ObservableProperty] private ObservableCollection<TaskViewModel> _tasks = [];
    
    public TaskQueueViewModel()
    {
        DisplayName = "Queue";
    }
}