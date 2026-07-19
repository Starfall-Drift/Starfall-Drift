using Content.Client.Stylesheets;
using Content.Client.Stylesheets.Fonts;
using Robust.Client.Graphics;
using Robust.Client.UserInterface;
using Robust.Client.UserInterface.Controls;
using static Content.Client.Stylesheets.StylesheetHelpers;

namespace Content.Client._Starfall.Stylesheets;

/// <summary>
/// Starfall character editor
/// </summary>
[CommonSheetlet]
public sealed class StarfallProfileEditorSheetlet : Sheetlet<PalettedStylesheet>
{
    public override StyleRule[] GetRules(
        PalettedStylesheet sheet,
        object config)
    {
        var root = new StyleBoxFlat
        {
            BackgroundColor = Color.FromHex("#1b1f23"),
            BorderColor = Color.FromHex("#39424a"),
            BorderThickness = new Thickness(1),
        };

        var card = new StyleBoxFlat
        {
            BackgroundColor = Color.FromHex("#24292e"),
            BorderColor = Color.FromHex("#39424a"),
            BorderThickness = new Thickness(1),

            ContentMarginLeftOverride = 10,
            ContentMarginTopOverride = 10,
            ContentMarginRightOverride = 10,
            ContentMarginBottomOverride = 10,
        };

        var sidebar = new StyleBoxFlat
        {
            BackgroundColor = Color.FromHex("#202428e8"),
            BorderColor = Color.FromHex("#39424a"),
            BorderThickness = new Thickness(1),

            ContentMarginLeftOverride = 8,
            ContentMarginTopOverride = 8,
            ContentMarginRightOverride = 8,
            ContentMarginBottomOverride = 8,
        };

        var preview = new StyleBoxFlat
        {
            BackgroundColor = Color.FromHex("#202428"),
            BorderColor = Color.FromHex("#39424a"),
            BorderThickness = new Thickness(1),

            ContentMarginLeftOverride = 10,
            ContentMarginTopOverride = 10,
            ContentMarginRightOverride = 10,
            ContentMarginBottomOverride = 10,
        };

        var header = new StyleBoxFlat
        {
            BackgroundColor = Color.FromHex("#2b3137"),
            BorderColor = Color.FromHex("#3e6189"),
            BorderThickness = new Thickness(0, 0, 0, 2),

            ContentMarginLeftOverride = 8,
            ContentMarginTopOverride = 5,
            ContentMarginRightOverride = 8,
            ContentMarginBottomOverride = 5,
        };

        return
        [
            E<PanelContainer>()
                .Class("StarfallProfileRoot")
                .Panel(root),

            E<PanelContainer>()
                .Class("StarfallProfileCard")
                .Panel(card),

            E<PanelContainer>()
                .Class("StarfallProfileSidebar")
                .Panel(sidebar),

            E<PanelContainer>()
                .Class("StarfallProfilePreview")
                .Panel(preview),

            E<PanelContainer>()
                .Class("StarfallProfileHeader")
                .Panel(header),

            E<Label>()
                .Class("StarfallProfileTitle")
                .Font(sheet.BaseFont.GetFont(15, FontKind.Bold))
                .FontColor(Color.FromHex("#e3e9ee")),

            E<ContainerButton>()
                .Class(ContainerButton.StyleClassButton)
                .Class("StarfallCharacterPicker")
                .PseudoNormal()
                .Modulate(Color.FromHex("#343D44")),

            E<ContainerButton>()
                .Class(ContainerButton.StyleClassButton)
                .Class("StarfallCharacterPicker")
                .PseudoHovered()
                .Modulate(Color.FromHex("#343D44")),

            E<ContainerButton>()
                .Class(ContainerButton.StyleClassButton)
                .Class("StarfallCharacterPicker")
                .PseudoPressed()
                .Modulate(Color.FromHex("#2A3035")),

            E<ContainerButton>()
                .Class(ContainerButton.StyleClassButton)
                .Class("StarfallCharacterPicker")
                .PseudoDisabled()
                .Modulate(Color.FromHex("#202428")),
        ];
    }
}
