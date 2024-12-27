using System.Collections.ObjectModel;
using System.Linq;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace VisualRemux.App.ViewModels.Workflow;

public partial class TaskQueueViewModel : ToolViewModel
{
    [ObservableProperty] private ObservableCollection<TaskViewModel> _tasks = [];
    [ObservableProperty] private ObservableCollection<TaskViewModel> _selectedTasks = [];
    
    public TaskQueueViewModel()
    {
        DisplayName = "Queue";

        Tasks.CollectionChanged += (_, _) => { ClearCompletedCommand.NotifyCanExecuteChanged(); };
    }

    [RelayCommand(CanExecute = nameof(CanClearCompleted))]
    private void ClearCompleted()
    {
        for (var i = Tasks.Count - 1; i >= 0; i--)
        {
            if (Tasks[i].IsCompleted)
            {
                Tasks.RemoveAt(i);
            }
        }
    }

    private bool CanClearCompleted() => Tasks.Any(task => task.IsCompleted);
}