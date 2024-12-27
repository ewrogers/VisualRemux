using Avalonia.Controls;

namespace VisualRemux.App.Services;

public class TopLevelResolver : ITopLevelResolver
{
    private TopLevel? _topLevel;

    public TopLevel? GetTopLevel() => _topLevel;
    public void SetTopLevel(TopLevel topLevel) => _topLevel = topLevel;
}