using System.IO;
using CommunityToolkit.Mvvm.ComponentModel;

namespace VisualRemux.App.ViewModels.Remux;

public partial class RemuxFileViewModel : ViewModelBase
{
    [ObservableProperty]
    private string _inputFilePath;

    public string InputFileName => Path.GetFileName(InputFilePath);

    public string InputFormat => Path.GetExtension(InputFilePath).Trim('.');
    
    [ObservableProperty]
    private string _outputFilePath;

    public string OutputFileName => Path.GetFileName(OutputFilePath);

    [ObservableProperty] private string _outputFormat;

    public RemuxFileViewModel(string inputFilePath, string outputFilePath, string outputFormat)
    {
        _inputFilePath = inputFilePath;
        _outputFilePath = outputFilePath;
        _outputFormat = outputFormat;
    }
}