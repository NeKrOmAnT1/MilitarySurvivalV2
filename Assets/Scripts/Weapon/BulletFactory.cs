using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class BulletFactory
{
    private readonly List<Bullet> _projectiles = new();

    [Inject] private readonly Bullet.Pool _projectilesPool;

    public Bullet SpawnProjectile(Transform transform, SideType side, WeaponCharacteristics weapon, Vector3 direction)
    {
        var projectile = _projectilesPool.Spawn();
        _projectiles.Add(projectile);
        projectile.transform.SetPositionAndRotation(transform.position, transform.rotation);
        projectile.Init(this, side, weapon, direction);
        return projectile;
    }

    public void RemoveProjectile(Bullet projectile)
    {
        _projectilesPool.Despawn(projectile);
        _projectiles.Remove(projectile);
    }
}