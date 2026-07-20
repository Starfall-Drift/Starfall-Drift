using Content.Client.Stylesheets;
using Content.Client.Stylesheets.Fonts;
using Content.Client.UserInterface.Controls;
using Robust.Client.Graphics;
using Robust.Client.UserInterface;
using Robust.Client.UserInterface.Controls;
using Robust.Client.UserInterface.CustomControls;
using static Content.Client.Stylesheets.StylesheetHelpers;
using Content.Client.UserInterface.Systems.Chat.Controls;

namespace Content.Client._Starfall.Stylesheets;

public sealed class StarfallThemeSheetlet : Sheetlet<PalettedStylesheet>
{
    // Main Colors
    private static readonly Color WindowBackground =
        Color.FromHex("#262626");

    private static readonly Color WindowBorder =
        Color.FromHex("#0b0d0f");

    private static readonly Color TitleBarBackground =
        Color.FromHex("#363636");

    private static readonly Color SectionBackground =
        Color.FromHex("#202428");

    private static readonly Color SectionBackgroundLight =
        Color.FromHex("#292e33");

    private static readonly Color SectionBackgroundDark =
        Color.FromHex("#1d2227");

    // Accent colors
    private static readonly Color AccentBlue =
        Color.FromHex("#477cab");

    private static readonly Color AccentBlueHover =
        Color.FromHex("#6394bb");

    private static readonly Color AccentBluePressed =
        Color.FromHex("#31516e");

    // Text colors
    private static readonly Color TextNormal =
        Color.FromHex("#E1E1E1");

    private static readonly Color TextMuted =
        Color.FromHex("#8997a3");

    private static readonly Color TextDisabled =
        Color.FromHex("#68727a");

    // Status colors
    private static readonly Color PositiveGreen =
        Color.FromHex("#1B9638");

    private static readonly Color NegativeRed =
        Color.FromHex("#BD2020");

    public override StyleRule[] GetRules(PalettedStylesheet sheet, object config)
    {
        // Windows

        var window = new StyleBoxFlat
        {
            BackgroundColor = WindowBackground,
            BorderColor = WindowBorder,
            BorderThickness = new Thickness(2),
        };

        var titleBar = new StyleBoxFlat
        {
            BackgroundColor = TitleBarBackground,

            ContentMarginLeftOverride = 6,
            ContentMarginTopOverride = 3,
            ContentMarginRightOverride = 6,
            ContentMarginBottomOverride = 3,
        };

        var titleDivider = new StyleBoxFlat
        {
            BackgroundColor = AccentBlue,
        };

        var alertTitleBar = new StyleBoxFlat
        {
            BackgroundColor = Color.FromHex("#54282c"),
            BorderColor = NegativeRed,
            BorderThickness = new Thickness(0, 0, 0, 2),
        };

        // Panels

        var panel = MakePanel(
            SectionBackground,
            Color.FromHex("#30363b"));

        var panelLight = MakePanel(
            SectionBackgroundLight,
            Color.FromHex("#3a4249"));

        var panelDark = MakePanel(
            SectionBackgroundDark,
            WindowBorder);

        var divider = new StyleBoxFlat
        {
            BackgroundColor = AccentBlue,
        };

        // Inputs

        var input = new StyleBoxFlat
        {
            BackgroundColor = Color.FromHex("#11161a"),
            BorderColor = Color.FromHex("#486a86"),
            BorderThickness = new Thickness(1),

            ContentMarginLeftOverride = 5,
            ContentMarginTopOverride = 3,
            ContentMarginRightOverride = 5,
            ContentMarginBottomOverride = 3,
        };

        var inputReadOnly = new StyleBoxFlat
        {
            BackgroundColor = Color.FromHex("#181c1f"),
            BorderColor = Color.FromHex("#3b4248"),
            BorderThickness = new Thickness(1),

            ContentMarginLeftOverride = 5,
            ContentMarginTopOverride = 3,
            ContentMarginRightOverride = 5,
            ContentMarginBottomOverride = 3,
        };

        // Dropdowns

        var optionPopup = new StyleBoxFlat
        {
            BackgroundColor = SectionBackground,

            ContentMarginLeftOverride = 2,
            ContentMarginTopOverride = 2,
            ContentMarginRightOverride = 2,
            ContentMarginBottomOverride = 2,
        };

        // Tabs

        var tabPanel = new StyleBoxFlat
        {
            BackgroundColor = Color.FromHex("#20262b"),
            BorderColor = Color.FromHex("#39424a"),
            BorderThickness = new Thickness(1),
        };

        var tabActive = new StyleBoxFlat
        {
            BackgroundColor = AccentBluePressed,
            BorderColor = AccentBlue,
            BorderThickness = new Thickness(0, 0, 0, 2),

            ContentMarginLeftOverride = 8,
            ContentMarginTopOverride = 4,
            ContentMarginRightOverride = 8,
            ContentMarginBottomOverride = 4,
        };

        var tabInactive = new StyleBoxFlat
        {
            BackgroundColor = Color.FromHex("#252b30"),

            ContentMarginLeftOverride = 8,
            ContentMarginTopOverride = 4,
            ContentMarginRightOverride = 8,
            ContentMarginBottomOverride = 4,
        };

        // Headings

        var nanoHeading = new StyleBoxFlat
        {
            BackgroundColor = Color.FromHex("#171a1d"),
            BorderColor = AccentBlue,
            BorderThickness = new Thickness(0, 0, 0, 2),

            ContentMarginLeftOverride = 7,
            ContentMarginTopOverride = 3,
            ContentMarginRightOverride = 7,
            ContentMarginBottomOverride = 3,
        };

        // Progress bars

        var progressBackground = new StyleBoxFlat
        {
            BackgroundColor = Color.FromHex("#11161a"),
            BorderColor = Color.FromHex("#41484e"),
            BorderThickness = new Thickness(1),
            ContentMarginTopOverride = 12,
            ContentMarginBottomOverride = 12,
        };

        var progressForeground = new StyleBoxFlat
        {
            BackgroundColor = AccentBlue,
            ContentMarginTopOverride = 12,
            ContentMarginBottomOverride = 12,
        };

        // Lists

        var itemListBackground = new StyleBoxFlat
        {
            BackgroundColor = SectionBackgroundDark,
            BorderColor = Color.FromHex("#30363b"),
            BorderThickness = new Thickness(1),
        };

        var itemListItem = MakeListItem(Color.Transparent);

        var itemListSelected = MakeListItem(AccentBluePressed);

        itemListSelected.BorderColor = AccentBlue;
        itemListSelected.BorderThickness = new Thickness(1);

        var itemListDisabled = MakeListItem(Color.FromHex("#1a1d20"));

        // Scrollbars

        var scrollNormal = MakeScrollGrabber(Color.FromHex("#48545e"));

        var scrollHovered = MakeScrollGrabber(Color.FromHex("#60788d"));

        var scrollPressed = MakeScrollGrabber(AccentBlue);

        // Tooltips

        var tooltip = new StyleBoxFlat
        {
            BackgroundColor = Color.FromHex("#161a1eef"),
            BorderColor = AccentBlue,
            BorderThickness = new Thickness(1),

            ContentMarginLeftOverride = 6,
            ContentMarginTopOverride = 4,
            ContentMarginRightOverride = 6,
            ContentMarginBottomOverride = 4,
        };

        // Embedded output and chat surfaces

        var outputBackground = new StyleBoxFlat
        {
            BackgroundColor = Color.FromHex("#1d2227"),
            BorderColor = Color.FromHex("#303941"),
            BorderThickness = new Thickness(1),

            ContentMarginLeftOverride = 4,
            ContentMarginTopOverride = 4,
            ContentMarginRightOverride = 4,
            ContentMarginBottomOverride = 4,
        };

        var chatBackground = new StyleBoxFlat
        {
            BackgroundColor = Color.FromHex("#20262b"),
            BorderColor = Color.FromHex("#303941"),
            BorderThickness = new Thickness(1),

            ContentMarginLeftOverride = 3,
            ContentMarginTopOverride = 3,
            ContentMarginRightOverride = 3,
            ContentMarginBottomOverride = 3,
        };

        return
        [
            // Windows

            E()
                .Class(DefaultWindow.StyleClassWindowPanel)
                .Panel(window),

            E()
                .Class(DefaultWindow.StyleClassWindowHeader)
                .Panel(titleBar),

            E<PanelContainer>()
                .Class("StarfallWindowBackground")
                .Panel(window),

            E<PanelContainer>()
                .Class("StarfallWindowTitleBar")
                .Panel(titleBar),

            E<PanelContainer>()
                .Class("StarfallWindowTitleDivider")
                .Panel(titleDivider),

            E()
                .Class(StyleClass.BorderedWindowPanel)
                .Panel(window),

            E()
                .Class(StyleClass.AlertWindowHeader)
                .Panel(alertTitleBar),

            // Window Titles

            E<Label>()
                .Class(DefaultWindow.StyleClassWindowTitle)
                .Font(sheet.BaseFont.GetFont(14, FontKind.Bold))
                .FontColor(TextNormal),

            E<Label>()
                .Class("FancyWindowTitle")
                .Font(sheet.BaseFont.GetFont(14, FontKind.Bold))
                .FontColor(TextNormal),

            E<Label>()
                .Class("windowTitleAlert")
                .Font(sheet.BaseFont.GetFont(14, FontKind.Bold))
                .FontColor(Color.FromHex("#fff0f0")),

            E<Label>()
                .Class("WindowFooterText")
                .Font(sheet.BaseFont.GetFont(9))
                .FontColor(TextMuted),

            // Panels

            E()
                .Class(StyleClass.BackgroundPanel)
                .Panel(panel),

            E()
                .Class(StyleClass.BackgroundPanelDark)
                .Panel(panelDark),

            E()
                .Class(StyleClass.BackgroundPanelOpenLeft)
                .Panel(panel),

            E()
                .Class(StyleClass.BackgroundPanelOpenRight)
                .Panel(panel),

            E<PanelContainer>()
                .Class(StyleClass.PanelLight)
                .Panel(panelLight),

            E<PanelContainer>()
                .Class(StyleClass.PanelDark)
                .Panel(panelDark),

            E<PanelContainer>()
                .Class("BackgroundDark")
                .Panel(panelDark),

            // Embedded output and chat surfaces

            E<OutputPanel>()
                .Panel(outputBackground),

            E<PanelContainer>()
                .Class(ChatInputBox.StyleClassChatPanel)
                .Panel(chatBackground),

            E<PanelContainer>()
                .Class("ChatPanel")
                .Panel(chatBackground),

            E<PanelContainer>()
                .Class("StyleNano.StyleClassChatPanel")
                .Panel(chatBackground),

            // Dividers

            E<PanelContainer>()
                .Class(StyleClass.LowDivider)
                .Panel(divider),

            E<PanelContainer>()
                .Class(StyleClass.HighDivider)
                .Panel(divider),

            // Buttons

            E<ContainerButton>()
                .Class(ContainerButton.StyleClassButton)
                .PseudoNormal()
                .Modulate(Color.FromHex("#3e6189")),

            E<ContainerButton>()
                .Class(ContainerButton.StyleClassButton)
                .PseudoHovered()
                .Modulate(AccentBlueHover),

            E<ContainerButton>()
                .Class(ContainerButton.StyleClassButton)
                .PseudoPressed()
                .Modulate(AccentBluePressed),

            E<ContainerButton>()
                .Class(ContainerButton.StyleClassButton)
                .PseudoDisabled()
                .Modulate(Color.FromHex("#6a6d70")),

            /* Status buttons */

            // Positive buttons
            E<ContainerButton>()
                .Class(ContainerButton.StyleClassButton)
                .Class(StyleClass.Positive)
                .PseudoNormal()
                .Modulate(PositiveGreen),

            E<ContainerButton>()
                .Class(ContainerButton.StyleClassButton)
                .Class(StyleClass.Positive)
                .PseudoHovered()
                .Modulate(Color.FromHex("#4ca967")),

            E<ContainerButton>()
                .Class(ContainerButton.StyleClassButton)
                .Class(StyleClass.Positive)
                .PseudoPressed()
                .Modulate(Color.FromHex("#28683b")),

            E<ContainerButton>()
                .Class(ContainerButton.StyleClassButton)
                .Class(StyleClass.Positive)
                .PseudoDisabled()
                .Modulate(Color.FromHex("#53685a")),

            // Negative buttons

            E<ContainerButton>()
                .Class(ContainerButton.StyleClassButton)
                .Class(StyleClass.Negative)
                .PseudoNormal()
                .Modulate(NegativeRed),

            E<ContainerButton>()
                .Class(ContainerButton.StyleClassButton)
                .Class(StyleClass.Negative)
                .PseudoHovered()
                .Modulate(Color.FromHex("#c4525a")),

            E<ContainerButton>()
                .Class(ContainerButton.StyleClassButton)
                .Class(StyleClass.Negative)
                .PseudoPressed()
                .Modulate(Color.FromHex("#7d2930")),

            E<ContainerButton>()
                .Class(ContainerButton.StyleClassButton)
                .Class(StyleClass.Negative)
                .PseudoDisabled()
                .Modulate(Color.FromHex("#685154")),

            // Text colors for buttons

            E<ContainerButton>()
                .Class(ContainerButton.StyleClassButton)
                .ParentOf(E<Label>())
                .FontColor(Color.White),

            E<ContainerButton>()
                .Class(ContainerButton.StyleClassButton)
                .PseudoDisabled()
                .ParentOf(E<Label>())
                .FontColor(Color.FromHex("#dedede")),

            // Inputs

            E<LineEdit>()
                .Prop(LineEdit.StylePropertyStyleBox, input),

            E<LineEdit>()
                .Class(LineEdit.StyleClassLineEditNotEditable)
                .Prop(
                    LineEdit.StylePropertyStyleBox,
                    inputReadOnly),

            E<LineEdit>()
                .Class(LineEdit.StyleClassLineEditNotEditable)
                .FontColor(TextMuted),

            E<LineEdit>()
                .Pseudo(LineEdit.StylePseudoClassPlaceholder)
                .FontColor(TextMuted),

            E<TextEdit>()
                .Pseudo(TextEdit.StylePseudoClassPlaceholder)
                .FontColor(TextMuted),

            // Dropdowns

            E<PanelContainer>()
                .Class(OptionButton.StyleClassOptionsBackground)
                .Panel(optionPopup),

            E<Label>()
                .Class(OptionButton.StyleClassOptionButton)
                .FontColor(Color.White)
                .AlignMode(Label.AlignMode.Center),

            E<TextureRect>()
                .Class(OptionButton.StyleClassOptionTriangle)
                .Modulate(Color.FromHex("#e5f1fa")),

            // Tabs

            E<TabContainer>()
                .Prop(
                    TabContainer.StylePropertyPanelStyleBox,
                    tabPanel)
                .Prop(
                    TabContainer.StylePropertyTabStyleBox,
                    tabActive)
                .Prop(
                    TabContainer.StylePropertyTabStyleBoxInactive,
                    tabInactive),

            // Progress bars

            E<ProgressBar>()
                .Prop(
                    ProgressBar.StylePropertyBackground,
                    progressBackground)
                .Prop(
                    ProgressBar.StylePropertyForeground,
                    progressForeground),

            // Lists

            E<ItemList>()
                .Prop(
                    ItemList.StylePropertyBackground,
                    itemListBackground)
                .Prop(
                    ItemList.StylePropertyItemBackground,
                    itemListItem)
                .Prop(
                    ItemList.StylePropertySelectedItemBackground,
                    itemListSelected)
                .Prop(
                    ItemList.StylePropertyDisabledItemBackground,
                    itemListDisabled),

            // Scrollbars

            E<VScrollBar>()
                .Prop(
                    ScrollBar.StylePropertyGrabber,
                    scrollNormal),

            E<VScrollBar>()
                .PseudoHovered()
                .Prop(
                    ScrollBar.StylePropertyGrabber,
                    scrollHovered),

            E<VScrollBar>()
                .PseudoPressed()
                .Prop(
                    ScrollBar.StylePropertyGrabber,
                    scrollPressed),

            E<HScrollBar>()
                .Prop(
                    ScrollBar.StylePropertyGrabber,
                    scrollNormal),

            E<HScrollBar>()
                .PseudoHovered()
                .Prop(
                    ScrollBar.StylePropertyGrabber,
                    scrollHovered),

            E<HScrollBar>()
                .PseudoPressed()
                .Prop(
                    ScrollBar.StylePropertyGrabber,
                    scrollPressed),

            // Switch buttons

            E<SwitchButton>()
                .Prop(SwitchButton.StylePropertySeparation, 8),

            E<SwitchButton>()
                .ParentOf(
                    E<TextureRect>()
                        .Class(SwitchButton.StyleClassTrackFill))
                .Modulate(SectionBackgroundDark),

            E<SwitchButton>()
                .ParentOf(
                    E<TextureRect>()
                        .Class(SwitchButton.StyleClassTrackOutline))
                .Modulate(Color.FromHex("#59636b")),

            E<SwitchButton>()
                .ParentOf(
                    E<TextureRect>()
                        .Class(SwitchButton.StyleClassThumbFill))
                .Modulate(AccentBlue),

            E<SwitchButton>()
                .ParentOf(
                    E<TextureRect>()
                        .Class(SwitchButton.StyleClassThumbOutline))
                .Modulate(Color.FromHex("#94b4cf")),

            E<SwitchButton>()
                .PseudoPressed()
                .ParentOf(
                    E<TextureRect>()
                        .Class(SwitchButton.StyleClassTrackFill))
                .Modulate(PositiveGreen),

            // Text

            E<Label>()
                .Class(StyleClass.Highlight)
                .FontColor(Color.FromHex("#E1E1E1")),

            E<Label>()
                .Class(StyleClass.LabelHeading)
                .Font(sheet.BaseFont.GetFont(15, FontKind.Bold))
                .FontColor(TextNormal),

            E<Label>()
                .Class(StyleClass.LabelHeadingBigger)
                .Font(sheet.BaseFont.GetFont(18, FontKind.Bold))
                .FontColor(TextNormal),

            E<Label>()
                .Class(StyleClass.LabelKeyText)
                .Font(sheet.BaseFont.GetFont(12, FontKind.Bold))
                .FontColor(Color.FromHex("#E1E1E1")),

            E<Label>()
                .Class(StyleClass.LabelSubText)
                .FontColor(TextMuted),

            E<Label>()
                .Class(StyleClass.LabelWeak)
                .FontColor(TextMuted),

            // Tooltips

            E<Tooltip>()
                .Panel(tooltip),

            // Those highlights that piss me off

            E<HLine>()
                .Class(StyleClass.Highlight)
                .Panel(new StyleBoxFlat
                {
                    BackgroundColor = AccentBlue,
                }),

            E<PanelContainer>()
                .Class(StyleClass.HighDivider)
                .Panel(new StyleBoxFlat
                {
                    BackgroundColor = AccentBlue,
                }),

            E<PanelContainer>()
                .Class(StyleClass.LowDivider)
                .Panel(new StyleBoxFlat
                {
                    BackgroundColor = AccentBlue,
                }),

            E<Label>()
                .Class(StyleClass.Highlight)
                .FontColor(Color.FromHex("#a9c9e5")),

            E<NanoHeading>()
                .ParentOf(E<PanelContainer>())
                .Panel(nanoHeading),
        ];
    }

    private static StyleBoxFlat MakePanel(
        Color background,
        Color border)
    {
        return new StyleBoxFlat
        {
            BackgroundColor = background,
            BorderColor = border,
            BorderThickness = new Thickness(1),

            ContentMarginLeftOverride = 6,
            ContentMarginTopOverride = 6,
            ContentMarginRightOverride = 6,
            ContentMarginBottomOverride = 6,
        };
    }

    private static StyleBoxFlat MakeListItem(
        Color background)
    {
        return new StyleBoxFlat
        {
            BackgroundColor = background,

            ContentMarginLeftOverride = 5,
            ContentMarginTopOverride = 2,
            ContentMarginRightOverride = 5,
            ContentMarginBottomOverride = 2,
        };
    }

    private static StyleBoxFlat MakeScrollGrabber(
        Color background)
    {
        return new StyleBoxFlat
        {
            BackgroundColor = background,
            ContentMarginLeftOverride = 8,
            ContentMarginTopOverride = 8,
        };
    }
}
