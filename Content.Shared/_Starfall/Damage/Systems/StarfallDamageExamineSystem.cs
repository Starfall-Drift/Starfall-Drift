using Content.Shared.Damage.Components;
using Content.Shared.Damage.Events;
using Content.Shared.Examine;
using Robust.Shared.Utility;

namespace Content.Shared._Starfall.Damage.Systems;
// <summary>_Starfall: Show damage examine info directly in the default examine menu instead of a separate verb window.</summary>
public sealed class StarfallDamageExamineSystem : EntitySystem
{
    public override void Initialize()
    {
        base.Initialize();

        SubscribeLocalEvent<DamageExaminableComponent, ExaminedEvent>(OnExamined);
    }

    private void OnExamined(Entity<DamageExaminableComponent> ent, ref ExaminedEvent args)
    {
        if (!args.IsInDetailsRange)
            return;

        var ev = new DamageExamineEvent(new FormattedMessage(), args.Examiner);
        RaiseLocalEvent(ent, ref ev);

        if (!ev.Message.IsEmpty)
        {
            args.PushMessage(ev.Message);
        }
    }
}

