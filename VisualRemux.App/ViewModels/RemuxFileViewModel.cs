using System.IO;
using CommunityToolkit.Mvvm.ComponentModel;

namespace VisualRemux.App.ViewModels;

public partial class RemuxFileViewModel : ViewModelBase
{
    [ObservableProperty]
    private string _inputFilePath;

    public string InputFileName => Path.GetFileName(InputFilePath);

    public RemuxFileViewModel(string inputFilePath)
    {
        _inputFilePath = inputFilePath;
    }
}