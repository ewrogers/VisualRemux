using System.Collections.ObjectModel;
using System.Linq;
using Avalonia.Metadata;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using VisualRemux.App.Models;

namespace VisualRemux.App.ViewModels.Workflow;

public partial class TaskQueueViewModel : ToolViewModel
{
    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(IsRunning))]
    [NotifyPropertyChangedFor(nameof(IsPaused))]
    [NotifyPropertyChangedFor(nameof(IsStopped))]
    [NotifyPropertyChangedFor(nameof(IsCompleted))]
    [NotifyCanExecuteChangedFor(nameof(StartQueueCommand))]
    [NotifyCanExecuteChangedFor(nameof(PauseQueueCommand))]
    private TaskQueueStatus _status;
    
    [ObservableProperty] private ObservableCollection<TaskViewModel> _tasks = [];
    [ObservableProperty] private ObservableCollection<TaskViewModel> _selectedTasks = [];
    
    public bool IsIdle => Status == TaskQueueStatus.Idle;
    public bool IsRunning => Status == TaskQueueStatus.Running;
    public bool IsPaused => Status == TaskQueueStatus.Paused;
    public bool IsStopped => Status == TaskQueueStatus.Stopped;
    public bool IsCompleted => Status == TaskQueueStatus.Completed;

    public TaskQueueViewModel()
    {
        DisplayName = "Queue";

        Tasks.CollectionChanged += (_, _) => { ClearCompletedCommand.NotifyCanExecuteChanged(); };
    }

    [RelayCommand(CanExecute = nameof(CanStartQueue))]
    private void StartQueue()
    {
        if (IsRunning)
        {
            return;
        }

        Status = TaskQueueStatus.Running;
    }

    private bool CanStartQueue() => !IsRunning;

    [RelayCommand(CanExecute = nameof(CanPauseQueue))]
    private void PauseQueue()
    {
        if (!IsRunning)
        {
            return;
        }

        Status = TaskQueueStatus.Paused;
    }

    private bool CanPauseQueue() => IsRunning;

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