using Avalonia.Controls;
using Microsoft.Extensions.DependencyInjection;
using VisualRemux.App.ViewModels;

namespace VisualRemux.App.Views;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        if (Design.IsDesignMode)
        {
            var viewModel = App.Current?.Services.GetService<MainWindowViewModel>();
            Design.SetDataContext(this, viewModel!);    
        }
        InitializeComponent();
    }
}