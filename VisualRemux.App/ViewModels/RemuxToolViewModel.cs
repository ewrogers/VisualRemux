using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;
using VisualRemux.App.Services;

namespace VisualRemux.App.ViewModels;

public partial class RemuxToolViewModel : ToolViewModel
{
    [ObservableProperty]
    private string _outputFormat = "mp4";

    [ObservableProperty] private ObservableCollection<string> _availableOutputFormats = ["mp4", "mkv"];
    
    public RemuxToolViewModel()
    {
        DisplayName = "Remux";
    }

    public void OutputCurrentState()
    {
        Debug.WriteLine($"OutputFormat: {OutputFormat}");
    }

    [RelayCommand]
    public async Task ShowSelectInputFilesDialog()
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
    }
}