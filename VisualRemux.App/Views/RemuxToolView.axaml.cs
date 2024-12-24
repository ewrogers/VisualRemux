using Avalonia.Controls;
using Microsoft.Extensions.DependencyInjection;
using VisualRemux.App.ViewModels.Remux;

namespace VisualRemux.App.Views;

public partial class RemuxToolView : UserControl
{
    public RemuxToolView()
    {
        if (Design.IsDesignMode)
        {
            var viewModel = App.Current?.Services.GetService<RemuxToolViewModel>();
            Design.SetDataContext(this, viewModel!);    
        }
        
        InitializeComponent();
    }
}