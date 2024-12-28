using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;
using VisualRemux.App.Logging;
using VisualRemux.App.Models;
using VisualRemux.App.Services;
using VisualRemux.App.ViewModels.Workflow;

namespace VisualRemux.App.ViewModels.Remux;

public partial class RemuxToolViewModel : ToolViewModel
{
    private readonly ILogger _logger;
    private readonly IFileService _fileService;
    private readonly TaskQueueViewModel _taskQueue;
    
    [ObservableProperty]
    private ObservableCollection<RemuxFileViewModel> _inputFiles = [];
    
    [ObservableProperty]
    private ObservableCollection<RemuxFileViewModel> _selectedFiles = [];

    [ObservableProperty] private string _outputFormat = "mp4";

    [ObservableProperty] private ObservableCollection<string> _availableOutputFormats = ["mp4", "mkv"];

    [ObservableProperty] private bool _useCustomOutputDirectory;

    [ObservableProperty]
    private string _outputDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyVideos);

    public RemuxToolViewModel(IServiceProvider serviceProvider)
    {
        _logger = serviceProvider.GetRequiredService<ILogger>().Child(this);
        _fileService = serviceProvider.GetRequiredService<IFileService>();
        _taskQueue = serviceProvider.GetRequiredService<TaskQueueViewModel>();

        DisplayName = "Remux";

        InputFiles.CollectionChanged += (_, _) => AddToQueueCommand.NotifyCanExecuteChanged();
        SelectedFiles.CollectionChanged += (_, _) => RemoveSelectedFilesCommand.NotifyCanExecuteChanged();
    }

    [RelayCommand]
    private async Task ShowSelectInputFilesDialog()
    {
        var files = await _fileService.OpenVideoFilesAsync("Select Files");
        if (files is null)
        {
            _logger.LogDebug("No files selected.");
            return;
        }

        foreach (var remuxFile in files)
        {
            var inputPath = remuxFile.Path.LocalPath;
            var inputFilename = Path.GetFileNameWithoutExtension(remuxFile.Path.LocalPath);

            var outputFilename = $"{inputFilename}.{OutputFormat}";
            var outputPath = Path.Join(OutputDirectory, outputFilename);

            var viewModel = new RemuxFileViewModel(inputPath, outputPath, OutputFormat);
            InputFiles.Add(viewModel);
        }
    }

    [RelayCommand(CanExecute = nameof(CanRemoveSelectedFiles))]
    private void RemoveSelectedFiles()
    {
        // Make a defensive copy to avoid modifying the collection while removing items
        foreach (var remuxFile in SelectedFiles.ToList())
        {
            InputFiles.Remove(remuxFile);
        }

        SelectedFiles.Clear();
    }

    private bool CanRemoveSelectedFiles() => SelectedFiles.Count > 0;

    [RelayCommand]
    private async Task ShowSelectOutputDirectoryDialog()
    {
        var folder = await _fileService.OpenFolderAsync("Select Output Directory");
        if (folder is null)
        {
            _logger.LogDebug("No folder selected.");
            return;
        }

        OutputDirectory = folder.Path.LocalPath;
    }

    [RelayCommand(CanExecute = nameof(CanAddToQueue))]
    private void AddToQueue()
    {
        foreach (var remuxFile in InputFiles)
        {
            var taskParameters = new RemuxTaskParameters
            {
                InputFile = remuxFile.InputFilePath,
                OutputFile = remuxFile.OutputFilePath,
                OutputFormat = remuxFile.OutputFormat
            };
            
            var taskViewModel = new RemuxTaskViewModel(taskParameters, _logger);
            _taskQueue.Tasks.Add(taskViewModel);
        }
        InputFiles.Clear();
    }
    
    private bool CanAddToQueue() => InputFiles.Count > 0;
}