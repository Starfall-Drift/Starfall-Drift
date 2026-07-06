using Robust.Shared.GameStates;

namespace Content.Shared._Starfall.Execution;

/// <summary>
/// Optionally add to a gun to override its execution doafter duration.
/// </summary>
[RegisterComponent, NetworkedComponent, AutoGenerateComponentState]
public sealed partial class GunExecutionComponent : Component
{
    /// <summary>
    /// Default: 4 seconds.
    /// How long the execution doafter lasts.
    /// </summary>
    [DataField, AutoNetworkedField]
    public TimeSpan ExecutionTime = TimeSpan.FromSeconds(4);
}

/// <summary>
/// Add to a gun to prevent it from being used for executions.
/// By default all guns support execution.
/// </summary>
[RegisterComponent, NetworkedComponent]
public sealed partial class GunExecutionBlacklistComponent : Component
{
}
