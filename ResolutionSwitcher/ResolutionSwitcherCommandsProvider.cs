using Microsoft.CommandPalette.Extensions;
using Microsoft.CommandPalette.Extensions.Toolkit;
using ResolutionSwitcher.Commands;

namespace ResolutionSwitcher;

public partial class ResolutionSwitcherCommandsProvider : CommandProvider
{
    private readonly ICommandItem[] _commands;

    public ResolutionSwitcherCommandsProvider()
    {
        DisplayName = "Resolution Switcher";
        Icon = IconHelpers.FromRelativePath("Assets\\StoreLogo.png");
        _commands = [
            new CommandItem(new ChangeResolutionCommand("FHD", 1920, 1080)) { Title = "FHD (1920x1080)", Subtitle = "Switch to Full HD" },
            new CommandItem(new ChangeResolutionCommand("WQHD", 2560, 1440)) { Title = "WQHD (2560x1440)", Subtitle = "Switch to Quad HD" },
            new CommandItem(new ChangeResolutionCommand("4K", 3840, 2160)) { Title = "4K (3840x2160)", Subtitle = "Switch to Ultra HD" },
        ];
    }

    public override ICommandItem[] TopLevelCommands()
    {
        return _commands;
    }
}
