using Avalonia.Controls;
using Microsoft.Extensions.DependencyInjection;
using VisualRemux.App.Logging;
using VisualRemux.App.ViewModels;

namespace VisualRemux.App.Views;

public partial class OutputLogView : UserControl
{
    public OutputLogView()
    {
        if (Design.IsDesignMode)
        {
            var viewModel = App.Current?.Services.GetService<OutputLogViewModel>();
            AddMockData(viewModel!);
            
            Design.SetDataContext(this, viewModel!);    
        }
        
        InitializeComponent();
    }

    private void AddMockData(OutputLogViewModel viewModel)
    {
        viewModel.LogEntries.Add(new OutputLogEntryViewModel(this,
            new LogEntry(LogLevel.Debug, "How much wood would a woodchuck chuck if a woodchuck could chuck wood?")));

        viewModel.LogEntries.Add(new OutputLogEntryViewModel(this,
            new LogEntry(LogLevel.Info, "The quick brown fox jumps over the lazy dog.")));

        viewModel.LogEntries.Add(new OutputLogEntryViewModel(this,
            new LogEntry(LogLevel.Warn, "Danger Will Robinson!")));

        viewModel.LogEntries.Add(new OutputLogEntryViewModel(this,
            new LogEntry(LogLevel.Error, "Uh-oh! Something went wrong.")));
    }
}