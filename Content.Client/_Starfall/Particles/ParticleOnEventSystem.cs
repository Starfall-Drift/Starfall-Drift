using Content.Shared._Starfall.Particles;
using Content.Shared.Interaction;
using Content.Shared.Interaction.Events;
using Content.Shared.Projectiles;
using Content.Shared.Throwing;
using Content.Shared.Weapons.Melee.Events;
using Content.Shared.Weapons.Ranged.Events;
using Robust.Shared.Prototypes;

namespace Content.Client._Starfall.Particles;

/// <summary>
/// Client-side system that listens for events and spawns particles based on <see cref="ParticleOnEventComponent"/>
/// </summary>
public sealed class ParticleOnEventSystem : EntitySystem
{
    [Dependency] private readonly ClientParticleSystem _particles = default!;
    [Dependency] private readonly SharedTransformSystem _transform = default!;
    [Dependency] private readonly IPrototypeManager _proto = default!;

    public override void Initialize()
    {
        base.Initialize();

        SubscribeLocalEvent<ParticleOnEventComponent, UseInHandEvent>(OnUseInHand);
        SubscribeLocalEvent<ParticleOnEventComponent, AfterInteractEvent>(OnUseInWorld);
        SubscribeLocalEvent<ParticleOnEventComponent, MeleeHitEvent>(OnMeleeAttack);
        SubscribeLocalEvent<ParticleOnEventComponent, AttackedEvent>(OnMeleeHit);
        SubscribeLocalEvent<ParticleOnEventComponent, ThrownEvent>(OnThrown);
        SubscribeLocalEvent<ParticleOnEventComponent, LandEvent>(OnLanded);
        SubscribeLocalEvent<ParticleOnEventComponent, AmmoShotEvent>(OnGunShot);
        SubscribeLocalEvent<ParticleOnEventComponent, ProjectileHitEvent>(OnProjectileHit);
    }

    private void OnUseInHand(Entity<ParticleOnEventComponent> ent, ref UseInHandEvent args)
    {
        if (!ent.Comp.OnUse || args.Handled)
            return;
        SpawnParticles(ent);
    }

    private void OnUseInWorld(Entity<ParticleOnEventComponent> ent, ref AfterInteractEvent args)
    {
        if (!ent.Comp.OnUseInWorld || !args.CanReach)
            return;
        SpawnParticles(ent);
    }

    private void OnMeleeAttack(Entity<ParticleOnEventComponent> ent, ref MeleeHitEvent args)
    {
        if (ent.Comp.OnMeleeAttack)
            SpawnParticles(ent);

        // Spawn particles on each entity that was hit
        if (ent.Comp.OnMeleeAttackOther)
        {
            foreach (var victim in args.HitEntities)
            {
                SpawnParticlesAt(ent.Comp, victim);
            }
        }
    }

    private void OnMeleeHit(Entity<ParticleOnEventComponent> ent, ref AttackedEvent args)
    {
        if (!ent.Comp.OnMeleeHit)
            return;
        SpawnParticles(ent);
    }

    private void OnThrown(Entity<ParticleOnEventComponent> ent, ref ThrownEvent args)
    {
        if (!ent.Comp.OnThrown)
            return;
        SpawnParticles(ent);
    }

    private void OnLanded(Entity<ParticleOnEventComponent> ent, ref LandEvent args)
    {
        if (!ent.Comp.OnLanded)
            return;
        SpawnParticles(ent);
    }

    private void OnGunShot(Entity<ParticleOnEventComponent> ent, ref AmmoShotEvent args)
    {
        if (ent.Comp.OnGunShot)
            SpawnParticles(ent);

        // Spawn particles on each projectile fired
        // Exception: projectile trails are allowed to be infinite duration because they get
        // destroyed when the projectile despawns, preventing the infinite-emitter issue.
        if (ent.Comp.OnGunShotProjectile)
        {
            foreach (var projectile in args.FiredProjectiles)
            {
                SpawnParticlesAt(ent.Comp, projectile, allowInfiniteDuration: true);
            }
        }
    }

    private void OnProjectileHit(Entity<ParticleOnEventComponent> ent, ref ProjectileHitEvent args)
    {
        if (ent.Comp.OnProjectileHit)
            SpawnParticles(ent);

        // Spawn particles on the entity that got hit
        if (ent.Comp.OnProjectileHitOther)
            SpawnParticlesAt(ent.Comp, args.Target);
    }

    private void SpawnParticles(Entity<ParticleOnEventComponent> ent)
    {
        SpawnParticlesAt(ent.Comp, ent.Owner);
    }

    private void SpawnParticlesAt(ParticleOnEventComponent comp, EntityUid target, bool allowInfiniteDuration = false)
    {
        // Failsafe: refuse to spawn infinite-duration effects (duration == 0) on event triggers,
        // EXCEPT for projectile trails which auto-cleanup when the projectile is destroyed.
        if (!_proto.TryIndex(comp.Effect, out var proto))
        {
            Log.Error($"ParticleOnEvent references unknown effect '{comp.Effect}'");
            return;
        }

        if (!allowInfiniteDuration && proto.Duration == TimeSpan.Zero)
        {
            Log.Warning($"ParticleOnEvent tried to spawn infinite-duration effect '{comp.Effect}'. " +
                        "Infinite effects cannot be used with ParticleOnEvent, they never stop emitting and will destroy performance. " +
                        "Set a duration on the particle or use a different particle.");
            return;
        }

        var coords = _transform.GetMapCoordinates(target);
        _particles.SpawnEffect(comp.Effect, coords, target, comp.ColorOverride);
    }
}

