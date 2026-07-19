using Robust.Client.UserInterface.Controls;
using Robust.Shared.Utility;

namespace Content.Client.Lobby.UI;

public sealed partial class HumanoidProfileEditor
{
    private bool _allowFlavorText;

    private FlavorText.FlavorText? _flavorText;
    private TextEdit? _flavorTextEdit;

    /// <summary>
    /// Refreshes the flavor text editor status.
    /// </summary>
    public void RefreshFlavorText()
    {
        if (_allowFlavorText)
        {
            FlavorTextSection.Visible = true;

            if (_flavorText != null)
                return;

            _flavorText = new FlavorText.FlavorText();
            // _Starfall: Add the flavor text control to the container instead of making a new tab
            // TabContainer.AddChild(_flavorText);
            // TabContainer.SetTabTitle(TabContainer.ChildCount - 1, Loc.GetString("humanoid-profile-editor-flavortext-tab"));
            // _flavorTextEdit = _flavorText.CFlavorTextInput;
            FlavorTextContainer.AddChild(_flavorText);

            _flavorTextEdit = _flavorText.CFlavorTextInput;
            _flavorText.OnFlavorTextChanged += OnFlavorTextChange;

            UpdateFlavorTextEdit();
        }
        else
        {
            FlavorTextSection.Visible = false;

            if (_flavorText == null)
                return;

            _flavorText.OnFlavorTextChanged -= OnFlavorTextChange;
            FlavorTextContainer.RemoveChild(_flavorText);

            _flavorText.Dispose();
            _flavorText = null;
            _flavorTextEdit = null;
        }
    }

    private void OnFlavorTextChange(string content)
    {
        if (Profile is null)
            return;

        Profile = Profile.WithFlavorText(content);
        SetDirty();
    }

    private void UpdateFlavorTextEdit()
    {
        if (_flavorTextEdit != null)
        {
            _flavorTextEdit.TextRope = new Rope.Leaf(Profile?.FlavorText ?? "");
        }
    }
}
