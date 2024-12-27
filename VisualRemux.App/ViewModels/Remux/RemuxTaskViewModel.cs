using VisualRemux.App.Logging;
using VisualRemux.App.Models;
using VisualRemux.App.ViewModels.Workflow;

namespace VisualRemux.App.ViewModels.Remux;

public partial class RemuxTaskViewModel : TaskViewModel
{
    private readonly RemuxTaskParameters _parameters;
    private readonly ILogger? _logger;

    public RemuxTaskViewModel(RemuxTaskParameters parameters, ILogger? logger = null)
    {
        _parameters = parameters;
        _logger = logger;
    }
}