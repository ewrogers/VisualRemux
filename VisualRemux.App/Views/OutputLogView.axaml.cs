using Avalonia.Controls;
using Microsoft.Extensions.DependencyInjection;
using VisualRemux.App.ViewModels;

namespace VisualRemux.App.Views;

public partial class OutputLogView : UserControl
{
    public OutputLogView()
    {
        if (Design.IsDesignMode)
        {
            var viewModel = App.Current?.Services.GetService<OutputLogViewModel>();
            Design.SetDataContext(this, viewModel!);    
        }
        
        InitializeComponent();
    }
}