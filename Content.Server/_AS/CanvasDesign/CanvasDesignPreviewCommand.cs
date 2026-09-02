using Content.Server.EUI;
using Content.Server.Administration;
using Content.Shared.Administration;
using Content.Server._AS.CanvasDesign;
using Robust.Shared.Console;

namespace Content.Server._AS.CanvasDesign;

[AdminCommand(AdminFlags.Logs)]
public sealed partial class CanvasDesignPreviewCommand : IConsoleCommand
{
    [Dependency] private EuiManager _eui = null!;
    [Dependency] private IEntityManager _entities = null!;

    public string Command => "canvas_preview";
    public string Description => "Opens a preview of a stored canvas.";
    public string Help => "canvas_preview <id>";

    public void Execute(IConsoleShell shell, string argStr, string[] args)
    {
        if (shell.Player is not { } player)
        {
            shell.WriteError(Loc.GetString("shell-cannot-run-command-from-server"));
            return;
        }

        if (args.Length != 1 || !int.TryParse(args[0], out var id) || id <= 0)
        {
            shell.WriteError(Help);
            return;
        }

        var system = _entities.System<CanvasDesignSystem>();
        if (!system.TryGetPreview(id, out var preview))
        {
            shell.WriteError($"Canvas preview {id} was not found or has expired.");
            return;
        }

        _eui.OpenEui(new CanvasDesignPreviewEui(id, preview), player);
    }
}
