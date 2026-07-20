using Content.Client.Stylesheets;
using Content.Client.Stylesheets.Fonts;
using Robust.Client.Graphics;
using Robust.Client.UserInterface;
using Robust.Client.UserInterface.Controls;
using static Content.Client.Stylesheets.StylesheetHelpers;
using Content.Client.UserInterface.Systems.Chat.Controls;
using Content.Client.UserInterface.Systems.Chat.Widgets;

namespace Content.Client._Starfall.Stylesheets;

/// <summary>
/// Starfall lobby/dashboard
/// </summary>
[CommonSheetlet]
public sealed class StarfallLobbySheetlet : Sheetlet<PalettedStylesheet>
{
    public override StyleRule[] GetRules(
        PalettedStylesheet sheet,
        object config)
    {
        var root = new StyleBoxFlat
        {
            BackgroundColor = Color.FromHex("#14191ddd"),
            BorderColor = Color.FromHex("#39424a"),
            BorderThickness = new Thickness(1),

            ContentMarginLeftOverride = 0,
            ContentMarginTopOverride = 0,
            ContentMarginRightOverride = 0,
            ContentMarginBottomOverride = 0,
        };

        var topBar = new StyleBoxFlat
        {
            BackgroundColor = Color.FromHex("#2d3135"),
            BorderColor = Color.FromHex("#3e6189"),
            BorderThickness = new Thickness(0, 0, 0, 2),

            ContentMarginLeftOverride = 10,
            ContentMarginTopOverride = 5,
            ContentMarginRightOverride = 8,
            ContentMarginBottomOverride = 5,
        };

        var toolbar = new StyleBoxFlat
        {
            BackgroundColor = Color.FromHex("#181c20"),
            BorderColor = Color.FromHex("#303941"),
            BorderThickness = new Thickness(0, 0, 0, 1),

            ContentMarginLeftOverride = 6,
            ContentMarginTopOverride = 5,
            ContentMarginRightOverride = 6,
            ContentMarginBottomOverride = 5,
        };

        var card = new StyleBoxFlat
        {
            BackgroundColor = Color.FromHex("#24292e"),
            BorderColor = Color.FromHex("#39424a"),
            BorderThickness = new Thickness(1),

            ContentMarginLeftOverride = 8,
            ContentMarginTopOverride = 8,
            ContentMarginRightOverride = 8,
            ContentMarginBottomOverride = 8,
        };

        var cardHeader = new StyleBoxFlat
        {
            BackgroundColor = Color.FromHex("#2b3137"),
            BorderColor = Color.FromHex("#3e6189"),
            BorderThickness = new Thickness(0, 0, 0, 2),

            ContentMarginLeftOverride = 8,
            ContentMarginTopOverride = 5,
            ContentMarginRightOverride = 8,
            ContentMarginBottomOverride = 5,
        };

        var chat = new StyleBoxFlat
        {
            BackgroundColor = Color.FromHex("#1d2227"),
            BorderColor = Color.FromHex("#39424a"),
            BorderThickness = new Thickness(1),

            ContentMarginLeftOverride = 3,
            ContentMarginTopOverride = 3,
            ContentMarginRightOverride = 3,
            ContentMarginBottomOverride = 3,
        };

        var floatingPanel = new StyleBoxFlat
        {
            BackgroundColor = Color.FromHex("#1b1f23ed"),
            BorderColor = Color.FromHex("#39424a"),
            BorderThickness = new Thickness(1),

            ContentMarginLeftOverride = 7,
            ContentMarginTopOverride = 7,
            ContentMarginRightOverride = 7,
            ContentMarginBottomOverride = 7,
        };

        var lobbyChatInner = new StyleBoxFlat
        {
            BackgroundColor = Color.FromHex("#1d2227"),
            BorderThickness = new Thickness(0),

            ContentMarginLeftOverride = 0,
            ContentMarginTopOverride = 0,
            ContentMarginRightOverride = 0,
            ContentMarginBottomOverride = 0,
        };

        return
        [
            E<PanelContainer>()
                .Class("StarfallLobbyRoot")
                .Panel(root),

            E<PanelContainer>()
                .Class("StarfallLobbyTopBar")
                .Panel(topBar),

            E<PanelContainer>()
                .Class("StarfallLobbyToolbar")
                .Panel(toolbar),

            E<PanelContainer>()
                .Class("StarfallLobbyCard")
                .Panel(card),

            E<PanelContainer>()
                .Class("StarfallLobbyCardHeader")
                .Panel(cardHeader),

            E<PanelContainer>()
                .Class("StarfallLobbyChat")
                .Panel(chat),

            E<PanelContainer>()
                .Class("StarfallLobbyFloating")
                .Panel(floatingPanel),

            E<Label>()
                .Class("StarfallLobbyTitle")
                .Font(sheet.BaseFont.GetFont(16, FontKind.Bold))
                .FontColor(Color.FromHex("#e3e9ee")),

            E<Label>()
                .Class("StarfallLobbyCardTitle")
                .Font(sheet.BaseFont.GetFont(14, FontKind.Bold))
                .FontColor(Color.FromHex("#e3e9ee")),

            E<Label>()
                .Class("StarfallLobbyMuted")
                .FontColor(Color.FromHex("#95a1aa")),

            E<ChatBox>()
                .ParentOf(
                    E<PanelContainer>()
                        .Class(ChatInputBox.StyleClassChatPanel))
                .Panel(lobbyChatInner),

            E<ChatBox>()
                .ParentOf(
                    E<PanelContainer>()
                        .Class("StyleNano.StyleClassChatPanel"))
                .Panel(lobbyChatInner),
        ];
    }
}
