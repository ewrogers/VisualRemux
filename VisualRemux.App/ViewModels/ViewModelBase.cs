using CommunityToolkit.Mvvm.ComponentModel;

namespace VisualRemux.App.ViewModels;

public abstract class ViewModelBase : ObservableObject
{
    public string DisplayName { get; set; }

    protected ViewModelBase()
    {
        DisplayName = GetType().Name;
    }

    public override string ToString() => DisplayName;
}