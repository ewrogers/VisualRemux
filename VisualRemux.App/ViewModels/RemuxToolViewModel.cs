using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;
using VisualRemux.App.Services;

namespace VisualRemux.App.ViewModels;

public partial class RemuxToolViewModel : ToolViewModel
{
    [ObservableProperty] private ObservableCollection<RemuxFileViewModel> _inputFiles = [];
    [ObservableProperty] private ObservableCollection<RemuxFileViewModel> _selectedFiles = [];
    
    [ObservableProperty]
    private string _outputFormat = "mp4";

    [ObservableProperty] private ObservableCollection<string> _availableOutputFormats = ["mp4", "mkv"];
    
    public RemuxToolViewModel()
    {
        DisplayName = "Remux";
    }

    [RelayCommand]
    private async Task ShowSelectInputFilesDialog()
    {
        var fileService = App.Current?.Services.GetService<IFileService>();
        if (fileService is null)
        {
            throw new NullReferenceException("File service is missing");
        }

        var files = await fileService.OpenVideoFilesAsync("Select Files");
        if (files is null)
        {
            return;
        }

        foreach (var file in files)
        {
            var viewModel = new RemuxFileViewModel(file.Path.LocalPath);
            InputFiles.Add(viewModel);
        }
    }

    [RelayCommand]
    private void RemoveSelectedFiles()
    {
        // Make a defensive copy to avoid modifying the collection while removing items
        foreach (var file in SelectedFiles.ToList())
        {
            InputFiles.Remove(file);
        }

        SelectedFiles.Clear();
    }
}