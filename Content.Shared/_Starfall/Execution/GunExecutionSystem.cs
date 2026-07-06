using System.Linq;
using System.Numerics;
using Content.Shared.Camera;
using Content.Shared.Chat;
using Content.Shared.CombatMode;
using Content.Shared.Damage;
using Content.Shared.Damage.Components;
using Content.Shared.Damage.Prototypes;
using Content.Shared.Database;
using Content.Shared.DoAfter;
using Content.Shared.Execution;
using Content.Shared.IdentityManagement;
using Content.Shared.Popups;
using Content.Shared.Projectiles;
using Content.Shared.Verbs;
using Content.Shared.Weapons.Hitscan.Components;
using Content.Shared.Weapons.Ranged;
using Content.Shared.Weapons.Ranged.Components;
using Content.Shared.Weapons.Ranged.Events;
using Content.Shared.Weapons.Ranged.Systems;
using Robust.Shared.Audio.Systems;
using Robust.Shared.GameObjects;
using Robust.Shared.Network;
using Robust.Shared.Player;
using Robust.Shared.Prototypes;
using Robust.Shared.Timing;

namespace Content.Shared._Starfall.Execution;

/// <summary>
/// Handles executions for guns. Guns that should not be able to execute should have <see cref="GunExecutionBlacklistComponent"/>.
/// </summary>
public sealed partial class GunExecutionSystem : EntitySystem
{
    [Dependency] private readonly SharedExecutionSystem _execution = default!;
    [Dependency] private readonly SharedDoAfterSystem _doAfter = default!;
    [Dependency] private readonly SharedPopupSystem _popup = default!;
    [Dependency] private readonly SharedAudioSystem _audio = default!;
    [Dependency] private readonly SharedSuicideSystem _suicide = default!;
    [Dependency] private readonly SharedCombatModeSystem _combat = default!;
    [Dependency] private readonly SharedCameraRecoilSystem _recoil = default!;
    [Dependency] private readonly SharedAppearanceSystem _appearance = default!;
    [Dependency] private readonly IPrototypeManager _proto = default!;
    [Dependency] private readonly IComponentFactory _compFactory = default!;
    [Dependency] private readonly IGameTiming _timing = default!;
    [Dependency] private readonly INetManager _net = default!;

    private static readonly ProtoId<DamageTypePrototype> FallbackDamageType = "Piercing";
    private static readonly TimeSpan DefaultExecutionTime = TimeSpan.FromSeconds(4);

    public override void Initialize()
    {
        base.Initialize();

        SubscribeLocalEvent<GunComponent, GetVerbsEvent<UtilityVerb>>(OnGetVerbs);
        SubscribeLocalEvent<GunComponent, GunExecutionDoAfterEvent>(OnDoAfter);
    }

    private void OnGetVerbs(Entity<GunComponent> ent, ref GetVerbsEvent<UtilityVerb> args)
    {
        if (args.Hands == null || args.Using == null || !args.CanAccess || !args.CanInteract)
            return;

        if (HasComp<GunExecutionBlacklistComponent>(ent))
            return;

        var attacker = args.User;
        var victim = args.Target;
        var weapon = ent.Owner;

        if (!_execution.CanBeExecuted(victim, attacker))
            return;

        if (ent.Comp.NextFire > _timing.CurTime)
            return;

        var executionTime = TryComp<GunExecutionComponent>(ent, out var cfg)
            ? cfg.ExecutionTime
            : DefaultExecutionTime;

        args.Verbs.Add(new UtilityVerb
        {
            Act = () => TryBeginExecution(weapon, victim, attacker, executionTime),
            Impact = LogImpact.High,
            Text = Loc.GetString("execution-verb-name"),
            Message = Loc.GetString("execution-verb-message"),
        });
    }

    private void TryBeginExecution(EntityUid weapon, EntityUid victim, EntityUid attacker, TimeSpan executionTime)
    {
        if (!_execution.CanBeExecuted(victim, attacker))
            return;

        if (attacker == victim)
        {
            ShowInternal("gun-execution-suicide-initial-self", attacker, victim, weapon);
            ShowExternal("gun-execution-suicide-initial-others", attacker, victim, weapon);
        }
        else
        {
            ShowInternal("gun-execution-initial-self", attacker, victim, weapon);
            ShowExternal("gun-execution-initial-others", attacker, victim, weapon);
        }

        _doAfter.TryStartDoAfter(new DoAfterArgs(
            EntityManager,
            attacker,
            executionTime,
            new GunExecutionDoAfterEvent(),
            weapon,
            target: victim,
            used: weapon)
        {
            BreakOnMove = true,
            BreakOnDamage = true,
            NeedHand = true,
        });
    }

    private void OnDoAfter(Entity<GunComponent> ent, ref GunExecutionDoAfterEvent args)
    {
        if (args.Handled || args.Cancelled || args.Used == null || args.Target == null)
            return;

        if (HasComp<GunExecutionBlacklistComponent>(ent))
            return;

        var attacker = args.User;
        var victim = args.Target.Value;
        var weapon = args.Used.Value;

        if (!_execution.CanBeExecuted(victim, attacker))
            return;

        if (!TryComp<DamageableComponent>(victim, out var damageable))
            return;

        // Capture direction for muzzle flash and recoil
        var targetPos = Transform(victim).WorldPosition - Transform(attacker).WorldPosition;

        var shootDir = targetPos != Vector2.Zero ? targetPos.Normalized() : Vector2.Zero;
        var recoilDir = -shootDir;

        // Consume one round.
        var takeAmmo = new TakeAmmoEvent(
            1,
            new List<(EntityUid? Entity, IShootable Shootable)>(),
            Transform(attacker).Coordinates,
            attacker);

        RaiseLocalEvent(weapon, takeAmmo);

        if (takeAmmo.Ammo.Count == 0)
        {
            _audio.PlayPredicted(ent.Comp.SoundEmpty, weapon, attacker);
            ShowInternal("gun-execution-empty-self", attacker, victim, weapon);
            ShowExternal("gun-execution-empty-others", attacker, victim, weapon);
            return;
        }

        var (ammoEnt, shootable) = takeAmmo.Ammo[0];
        string? damageType = null;

        switch (shootable)
        {
            case CartridgeAmmoComponent cartridge:
            {
                if (_proto.TryIndex<EntityPrototype>(cartridge.Prototype, out var proto)
                    && proto.TryGetComponent<ProjectileComponent>(out var projComp, _compFactory))
                {
                    damageType = DominantDamageType(projComp.Damage);
                }

                // Mark casing spent
                cartridge.Spent = true;
                _appearance.SetData(ammoEnt!.Value, AmmoVisuals.Spent, true);
                Dirty(ammoEnt.Value, cartridge);
                break;
            }
            case HitscanAmmoComponent:
                damageType = "Heat"; // most hitscan are energy weapons sooooooooooo
                CleanupSpawnedAmmo(ammoEnt);
                break;
            case AmmoComponent:
            {
                if (ammoEnt != null && TryComp<ProjectileComponent>(ammoEnt.Value, out var projComp))
                    damageType = DominantDamageType(projComp.Damage);

                CleanupSpawnedAmmo(ammoEnt);
                break;
            }
        }

        // Muzzle flash
        if (shootable is AmmoComponent ammoForFlash && ammoForFlash.MuzzleFlash is { } muzzleProto)
        {
            var attemptEv = new GunMuzzleFlashAttemptEvent();
            RaiseLocalEvent(weapon, ref attemptEv);

            if (!attemptEv.Cancelled)
            {
                var flashEv = new MuzzleFlashEvent(GetNetEntity(weapon), muzzleProto, shootDir.ToAngle());

                RaiseLocalEvent(flashEv);

                if (_net.IsServer)
                {
                    var filter = Filter.Pvs(weapon, entityManager: EntityManager)
                        .RemovePlayerByAttachedEntity(attacker);
                    RaiseNetworkEvent(flashEv, filter);
                }
            }
        }

        var prevCombat = _combat.IsInCombatMode(attacker);
        _combat.SetInCombatMode(attacker, true);

        // Recoil
        if (_net.IsClient && recoilDir != Vector2.Zero && _timing.IsFirstTimePredicted)
            _recoil.KickCamera(attacker, recoilDir * 0.5f * ent.Comp.CameraRecoilScalarModified);

        _audio.PlayPredicted(ent.Comp.SoundGunshotModified ?? ent.Comp.SoundGunshot, weapon, attacker);

        if (attacker == victim)
        {
            ShowInternal("gun-execution-suicide-complete-self", attacker, victim, weapon);
            ShowExternal("gun-execution-suicide-complete-others", attacker, victim, weapon);
        }
        else
        {
            ShowInternal("gun-execution-complete-self", attacker, victim, weapon);
            ShowExternal("gun-execution-complete-others", attacker, victim, weapon);
        }

        _suicide.ApplyLethalDamage(
            (victim, damageable),
            damageType != null ? new ProtoId<DamageTypePrototype>(damageType) : FallbackDamageType);

        _combat.SetInCombatMode(attacker, prevCombat);
        args.Handled = true;
    }

    /// <summary>
    /// Deletes an ammo entity that only exists so we could consume it.
    /// </summary>
    private void CleanupSpawnedAmmo(EntityUid? ammoEnt)
    {
        if (ammoEnt == null)
            return;

        if (IsClientSide(ammoEnt.Value))
            Del(ammoEnt.Value);
        else if (_net.IsServer)
            Del(ammoEnt.Value);
    }

    /// <summary>
    /// Returns the damage type key with the highest value
    /// </summary>
    private static string? DominantDamageType(DamageSpecifier damage)
    {
        return damage.DamageDict
            .Where(kv => !string.Equals(kv.Key, "Structural", StringComparison.OrdinalIgnoreCase))
            .OrderByDescending(kv => kv.Value)
            .Select(kv => kv.Key)
            .FirstOrDefault();
    }

    private void ShowInternal(LocId key, EntityUid attacker, EntityUid victim, EntityUid weapon)
    {
        _popup.PopupClient(
            Loc.GetString(key,
                ("attacker", Identity.Entity(attacker, EntityManager)),
                ("victim", Identity.Entity(victim, EntityManager)),
                ("weapon", weapon)),
            attacker,
            attacker,
            PopupType.MediumCaution);
    }

    private void ShowExternal(LocId key, EntityUid attacker, EntityUid victim, EntityUid weapon)
    {
        _popup.PopupEntity(
            Loc.GetString(key,
                ("attacker", Identity.Entity(attacker, EntityManager)),
                ("victim", Identity.Entity(victim, EntityManager)),
                ("weapon", weapon)),
            attacker,
            Filter.PvsExcept(attacker),
            true,
            PopupType.MediumCaution);
    }
}

