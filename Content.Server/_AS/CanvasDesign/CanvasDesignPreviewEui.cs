using Content.Server.EUI;
using Content.Shared._AS.CanvasDesign;
using Content.Shared.Eui;

namespace Content.Server._AS.CanvasDesign;

public sealed class CanvasDesignPreviewEui(int previewId, CanvasDesignPreview preview) : BaseEui
{
    public override void Opened()
    {
        base.Opened();
        StateDirty();
    }

    public override EuiStateBase GetNewState()
    {
        return new CanvasDesignPreviewEuiState(
            previewId,
            preview.Width,
            preview.Height,
            preview.Background,
            (uint[]) preview.Pixels.Clone(),
            preview.Name,
            preview.Description);
    }
}
