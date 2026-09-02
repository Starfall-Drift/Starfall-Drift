using Content.Client._AS.CanvasDesign.UI;
using Content.Client.Eui;
using Content.Shared._AS.CanvasDesign;
using Content.Shared.Eui;
using JetBrains.Annotations;

namespace Content.Client._AS.CanvasDesign;

[UsedImplicitly]
public sealed class CanvasDesignPreviewEui : BaseEui
{
    private CanvasDesignPreviewWindow? _window;

    public override void Opened()
    {
        base.Opened();
        _window = new CanvasDesignPreviewWindow();
        _window.OnClose += () => SendMessage(new CloseEuiMessage());
        _window.OpenCentered();
    }

    public override void HandleState(EuiStateBase state)
    {
        if (state is CanvasDesignPreviewEuiState preview)
            _window?.SetPreview(preview.PreviewId,
                preview.Width,
                preview.Height,
                preview.Background,
                preview.Pixels,
                preview.Name,
                preview.Description);
    }

    public override void Closed()
    {
        base.Closed();
        _window?.Close();
        _window = null;
    }
}
