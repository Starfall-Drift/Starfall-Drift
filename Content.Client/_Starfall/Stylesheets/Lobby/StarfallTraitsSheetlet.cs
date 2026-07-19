using Content.Client.Stylesheets;
using Content.Client.Stylesheets.Fonts;
using Robust.Client.Graphics;
using Robust.Client.UserInterface;
using Robust.Client.UserInterface.Controls;
using static Content.Client.Stylesheets.StylesheetHelpers;

namespace Content.Client._Starfall.Stylesheets.Lobby;

/// <summary>
/// Starfall trait-selection cards and cost badges
/// </summary>
[CommonSheetlet]
public sealed class StarfallTraitsSheetlet : Sheetlet<PalettedStylesheet>
{
    public override StyleRule[] GetRules(
        PalettedStylesheet sheet,
        object config)
    {
        var card = new StyleBoxFlat
        {
            BackgroundColor = Color.FromHex("#202428"),
            BorderColor = Color.FromHex("#303941"),
            BorderThickness = new Thickness(1),

            ContentMarginLeftOverride = 6,
            ContentMarginTopOverride = 4,
            ContentMarginRightOverride = 6,
            ContentMarginBottomOverride = 4,
        };

        var selectedCard = new StyleBoxFlat
        {
            BackgroundColor = Color.FromHex("#293641"),
            BorderColor = Color.FromHex("#3e6189"),
            BorderThickness = new Thickness(1),

            ContentMarginLeftOverride = 6,
            ContentMarginTopOverride = 4,
            ContentMarginRightOverride = 6,
            ContentMarginBottomOverride = 4,
        };

        var neutralCost = MakeCostBadge(
            Color.FromHex("#343b41"),
            Color.FromHex("#59636b"));

        var positiveCost = MakeCostBadge(
            Color.FromHex("#542f33"),
            Color.FromHex("#a63c44"));

        var negativeCost = MakeCostBadge(
            Color.FromHex("#294333"),
            Color.FromHex("#398c50"));

        return
        [
            E<PanelContainer>()
                .Class("StarfallTraitCard")
                .Panel(card),

            E<PanelContainer>()
                .Class("StarfallTraitCard")
                .Class("StarfallTraitSelected")
                .Panel(selectedCard),

            E<Label>()
                .Class("StarfallTraitDescription")
                .FontColor(Color.FromHex("#9aa6b0")),
        ];
    }

    private static StyleBoxFlat MakeCostBadge(
        Color background,
        Color border)
    {
        return new StyleBoxFlat
        {
            BackgroundColor = background,
            BorderColor = border,
            BorderThickness = new Thickness(1),

            ContentMarginLeftOverride = 6,
            ContentMarginTopOverride = 2,
            ContentMarginRightOverride = 6,
            ContentMarginBottomOverride = 2,
        };
    }
}
