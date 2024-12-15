using System.Collections.ObjectModel;
using System.Diagnostics;
using CommunityToolkit.Mvvm.ComponentModel;

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
}