using Content.Client.Stylesheets;
using Content.Client.Stylesheets.Fonts;
using Robust.Client.Graphics;
using Robust.Client.UserInterface;
using Robust.Client.UserInterface.Controls;
using static Content.Client.Stylesheets.StylesheetHelpers;

namespace Content.Client._Starfall.Stylesheets.Lobby;

[CommonSheetlet]
public sealed class StarfallJobSheetlet : Sheetlet<PalettedStylesheet>
{
    public override StyleRule[] GetRules(PalettedStylesheet sheet, object config)
    {
        StyleBoxFlat Box(string background, string border, float margin = 8) => new()
        {
            BackgroundColor = Color.FromHex(background),
            BorderColor = Color.FromHex(border),
            BorderThickness = new Thickness(1),
            ContentMarginLeftOverride = margin,
            ContentMarginTopOverride = margin,
            ContentMarginRightOverride = margin,
            ContentMarginBottomOverride = margin,
        };

        var transparent = Box("#00000000", "#00000000", 3);
        var hover = Box("#27323a", "#00000000", 3);
        var pressed = Box("#304252", "#00000000", 3);

        return
        [
            E<PanelContainer>()
                .Class("StarfallJobCard")
                .Panel(Box("#20262b", "#39444d")),
            E<PanelContainer>()
                .Class("StarfallJobIcon")
                .Panel(Box("#181d21", "#303a42", 3)),
            E<PanelContainer>()
                .Class("StarfallJobDescriptionPanel")
                .Panel(Box("#181d21", "#303a42", 6)),
            E<Label>()
                .Class("StarfallJobName")
                .FontColor(Color.FromHex("#e4e9ed"))
                .Font(sheet.BaseFont.GetFont(15, FontKind.Bold)),
            E<ContainerButton>()
                .Class("StarfallJobTitleButton")
                .PseudoNormal()
                .Panel(transparent),
            E<ContainerButton>()
                .Class("StarfallJobTitleButton")
                .Pseudo(ContainerButton.StylePseudoClassHover)
                .Panel(hover),
            E<ContainerButton>()
                .Class("StarfallJobTitleButton")
                .Pseudo(ContainerButton.StylePseudoClassPressed)
                .Panel(pressed),
        ];
    }
}
