using Content.Client.Stylesheets;
using Robust.Client.Graphics;
using Robust.Client.UserInterface;
using Robust.Client.UserInterface.Controls;
using static Content.Client.Stylesheets.StylesheetHelpers;

namespace Content.Client._Starfall.Stylesheets.Lobby;

[CommonSheetlet]
public sealed class StarfallLoadoutSheetlet :
    Sheetlet<PalettedStylesheet>
{
    public override StyleRule[] GetRules(
        PalettedStylesheet sheet,
        object config)
    {
        StyleBoxFlat Card(
            string background,
            string border)
        {
            return new StyleBoxFlat
            {
                BackgroundColor =
                    Color.FromHex(background),

                BorderColor =
                    Color.FromHex(border),

                BorderThickness =
                    new Thickness(1),

                ContentMarginLeftOverride = 4,
                ContentMarginTopOverride = 4,
                ContentMarginRightOverride = 4,
                ContentMarginBottomOverride = 4,
            };
        }

        var normal = Card("#20262b", "#39444d");
        var hovered = Card("#27323a", "#4a5964");
        var selected = Card("#304252", "#3e6189");
        var disabled = Card("#1b2024", "#30383f");

        return
        [
            E<PanelContainer>()
                .Class("StarfallLoadoutCard")
                .Panel(normal),

            E<ContainerButton>()
                .Class("StarfallLoadoutItemCard")
                .PseudoHovered()
                .ParentOf(
                    E<PanelContainer>()
                        .Class("StarfallLoadoutCard"))
                .Panel(hovered),

            E<ContainerButton>()
                .Class("StarfallLoadoutItemCard")
                .PseudoPressed()
                .ParentOf(
                    E<PanelContainer>()
                        .Class("StarfallLoadoutCard"))
                .Panel(selected),

            E<ContainerButton>()
                .Class("StarfallLoadoutItemCard")
                .PseudoDisabled()
                .ParentOf(
                    E<PanelContainer>()
                        .Class("StarfallLoadoutCard"))
                .Panel(disabled),
        ];
    }
}
