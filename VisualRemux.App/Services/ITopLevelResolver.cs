using Avalonia.Controls;

namespace VisualRemux.App.Services;

public interface ITopLevelResolver
{
    TopLevel? GetTopLevel();
    void SetTopLevel(TopLevel topLevel);
}