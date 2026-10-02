using Content.Client.Light.Components;
using Content.Shared._Starfall.Particles;
using Content.Shared._Starfall.Particles.Visuals;
using Robust.Client.GameObjects;
using Robust.Shared.Prototypes;
using Robust.Shared.Spawners;

namespace Content.Client._Starfall.Particles.Visuals;

/// <summary>
/// Renders machine spark bursts authorized by the server.
/// </summary>
public sealed partial class SparksOnDamageSystem : EntitySystem
{
    [Dependency] private ParticleSystem _particles = null!;
    [Dependency] private SharedPointLightSystem _lights = null!;

    public override void Initialize()
    {
        base.Initialize();
        SubscribeNetworkEvent<SparksOnDamageEvent>(OnSparks);
    }

    private void OnSparks(SparksOnDamageEvent args)
    {
        _particles.SpawnEffect(new ProtoId<ParticleEffectPrototype>(args.Effect), args.Coordinates);

        var lightEntity = Spawn(null, args.Coordinates);
        var light = _lights.EnsureLight(lightEntity);
        light.NetSyncEnabled = false;
        _lights.SetCastShadows(lightEntity, false, light);
        _lights.SetColor(lightEntity, args.LightColor, light);
        _lights.SetRadius(lightEntity, args.LightRadius, light);
        _lights.SetEnergy(lightEntity, args.LightEnergy, light);

        AddComp(lightEntity, new TimedDespawnComponent
        {
            Lifetime = args.LightDuration,
        });
        AddComp(lightEntity, new LightFadeComponent
        {
            Duration = args.LightDuration,
        });
    }
}
