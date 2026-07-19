using Content.Client.Stylesheets;
using Content.Client.Stylesheets.Fonts;
using Robust.Client.Graphics;
using Robust.Client.UserInterface;
using Robust.Client.UserInterface.Controls;
using static Content.Client.Stylesheets.StylesheetHelpers;

namespace Content.Client._Starfall.Stylesheets.Lobby;

[CommonSheetlet]
public sealed class StarfallAntagSheetlet : Sheetlet<PalettedStylesheet>
{
    public override StyleRule[] GetRules(PalettedStylesheet sheet, object config)
    {
        var card = new StyleBoxFlat
        {
            BackgroundColor = Color.FromHex("#20262b"),
            BorderColor = Color.FromHex("#39444d"),
            BorderThickness = new Thickness(1),
            ContentMarginLeftOverride = 8,
            ContentMarginTopOverride = 8,
            ContentMarginRightOverride = 8,
            ContentMarginBottomOverride = 8,
        };

        var inset = new StyleBoxFlat
        {
            BackgroundColor = Color.FromHex("#181d21"),
            BorderColor = Color.FromHex("#303a42"),
            BorderThickness = new Thickness(1),
            ContentMarginLeftOverride = 6,
            ContentMarginTopOverride = 6,
            ContentMarginRightOverride = 6,
            ContentMarginBottomOverride = 6,
        };

        var titleNormal = new StyleBoxFlat
        {
            BackgroundColor = Color.Transparent,

            ContentMarginLeftOverride = 5,
            ContentMarginTopOverride = 3,
            ContentMarginRightOverride = 5,
            ContentMarginBottomOverride = 3,
        };

        var titleHover = new StyleBoxFlat
        {
            BackgroundColor = Color.FromHex("#27323a"),

            ContentMarginLeftOverride = 5,
            ContentMarginTopOverride = 3,
            ContentMarginRightOverride = 5,
            ContentMarginBottomOverride = 3,
        };

        var titlePressed = new StyleBoxFlat
        {
            BackgroundColor = Color.FromHex("#304252"),

            ContentMarginLeftOverride = 5,
            ContentMarginTopOverride = 3,
            ContentMarginRightOverride = 5,
            ContentMarginBottomOverride = 3,
        };

        return
        [
            E<PanelContainer>().Class("StarfallAntagCard").Panel(card),
            E<PanelContainer>().Class("StarfallAntagIcon").Panel(inset),
            E<PanelContainer>().Class("StarfallAntagDescription").Panel(inset),
            E<Label>()
                .Class("StarfallAntagName")
                .FontColor(Color.FromHex("#e4e9ed"))
                .Font(sheet.BaseFont.GetFont(16, FontKind.Bold)),
            E<ContainerButton>()
                .Class("StarfallAntagTitleButton")
                .PseudoNormal()
                .Panel(titleNormal),

            E<ContainerButton>()
                .Class("StarfallAntagTitleButton")
                .Pseudo(ContainerButton.StylePseudoClassHover)
                .Panel(titleHover),

            E<ContainerButton>()
                .Class("StarfallAntagTitleButton")
                .Pseudo(ContainerButton.StylePseudoClassPressed)
                .Panel(titlePressed),
        ];
    }
}
