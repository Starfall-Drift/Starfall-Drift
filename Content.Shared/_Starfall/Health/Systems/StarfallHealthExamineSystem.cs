using Content.Shared.Damage.Components;
using Content.Shared.Examine;
using Content.Shared.HealthExaminable;

namespace Content.Shared._Starfall.Health.Systems;
// <summary>_Starfall: Show damage examine info directly in the default examine menu instead of a separate verb window.</summary>
public sealed class StarfallHealthExamineSystem : EntitySystem
{
    [Dependency] private readonly HealthExaminableSystem _healthExaminable = default!;

    public override void Initialize()
    {
        base.Initialize();

        SubscribeLocalEvent<HealthExaminableComponent, ExaminedEvent>(OnExamined);
    }

    private void OnExamined(Entity<HealthExaminableComponent> ent, ref ExaminedEvent args)
    {
        if (!args.IsInDetailsRange)
            return;

        if (!TryComp<DamageableComponent>(ent, out var damage))
            return;

        var markup = _healthExaminable.CreateMarkup(ent, ent.Comp, damage);

        if (!markup.IsEmpty)
        {
            args.PushMessage(markup);
        }
    }
}



