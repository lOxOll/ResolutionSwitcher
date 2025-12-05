using Microsoft.CommandPalette.Extensions;
using Microsoft.CommandPalette.Extensions.Toolkit;

namespace ResolutionSwitcher.Commands;

internal sealed partial class ChangeResolutionCommand : InvokableCommand
{
    private readonly int _width;
    private readonly int _height;
    private readonly string _name;

    public ChangeResolutionCommand(string name, int width, int height)
    {
        _name = name;
        _width = width;
        _height = height;
        Name = $"Change to {name}";
    }

    public override CommandResult Invoke()
    {
        var (success, message) = DisplayManager.ChangeResolution(_width, _height);

        if (success)
        {
            return CommandResult.Dismiss();
        }
        else
        {
            return CommandResult.ShowToast(message);
        }
    }
}
