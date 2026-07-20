using Content.Shared.Preferences;
using Content.Shared.Roles;

// ᓚᘏᗢ <(This is intentional
namespace Content.Client.Lobby.UI;

/// <summary>
/// Integration layer between <see cref="HumanoidProfileEditor"/> and Starfall's redesigned loadout, role, and trait editors.
/// </summary>
public sealed partial class HumanoidProfileEditor
{
    /// <summary>
    /// Temporarily overrides the job used to render the character preview.
    /// This lets the Roles and Loadout editors preview job clothing without
    /// changing the character's actual saved job preferences.
    /// TODO: Will probably change with multi-char select
    /// </summary>
    public JobPrototype? JobOverride;

    /// <summary>
    /// Upstream code expects PreferenceUnavailableButton to belong directly to HumanoidProfileEditor.
    /// The actual control is in RolesEditor, so this property just forwards the reference.
    /// </summary>
    private Robust.Client.UserInterface.Controls.OptionButton PreferenceUnavailableButton => RolesEditor.PreferenceUnavailableButton;

    /// <summary>
    /// Initializes the Starfall editors and connects them to the upstream systems.
    /// This should only be called once during the construction of HumanoidProfileEditor.
    /// </summary>
    private void InitializeStarfallEditors()
    {
        // ᓚᘏᗢ <(I love blocks of code like this its so pretty
        TabContainer.SetTabTitle(0,Loc.GetString("humanoid-profile-editor-identity-tab"));
        TabContainer.SetTabTitle(1,Loc.GetString("humanoid-profile-editor-appearance-tab"));
        TabContainer.SetTabTitle(2,Loc.GetString("humanoid-profile-editor-loadout-tab"));
        TabContainer.SetTabTitle(3,Loc.GetString("humanoid-profile-editor-roles-tab"));
        TabContainer.SetTabTitle(4,Loc.GetString("humanoid-profile-editor-traits-tab"));

        LoadoutEditor.ProfileChanged += ApplyStarfallProfileChange;
        RolesEditor.ProfileChanged += ApplyStarfallProfileChange;
        TraitsEditor.ProfileChanged += ApplyStarfallProfileChange;

        LoadoutEditor.PreviewJobChanged += job =>
        {
            JobOverride = job;
            ReloadPreview();
        };

        RolesEditor.PreviewJobChanged += job =>
        {
            JobOverride = job;
            ReloadPreview();
        };

        // The Loadout button on a job card opens that job directly in the Loadout tab.
        RolesEditor.LoadoutRequested += job =>
        {
            LoadoutEditor.SyncProfile(Profile);
            LoadoutEditor.SelectJob(job.ID);
            TabContainer.CurrentTab = 2;
        };

        RolesEditor.OpenGuidebookRequested += pages =>
            OnOpenGuidebook?.Invoke(pages);
    }

    private void ApplyStarfallProfileChange(
        HumanoidCharacterProfile profile)
    {
        Profile = profile;

        LoadoutEditor.SyncProfile(profile);
        RolesEditor.SyncProfile(profile);
        TraitsEditor.SyncProfile(profile);

        SetDirty();
        ReloadPreview();
    }

    public void RefreshJobs()
    {
        RolesEditor.SyncProfile(Profile);
        RolesEditor.RefreshJobs();
    }

    public void RefreshAntags()
    {
        RolesEditor.SyncProfile(Profile);
        RolesEditor.RefreshAntags();
    }

    public void RefreshLoadouts()
    {
        LoadoutEditor.SetProfile(Profile);
    }

    public void RefreshTraits()
    {
        TraitsEditor.SetProfile(Profile);
    }
}
