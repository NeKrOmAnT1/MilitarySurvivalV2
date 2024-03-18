using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class ProjectileFactory 
{
    private readonly List<Projectile> _projectiles = new();

    [Inject] private readonly Projectile.Pool _projectilesPool;

    public Projectile SpawnProjectile(Transform transform, PlayerHealth playerHealth, float damage, Transform target)
    {
        var projectile = _projectilesPool.Spawn();
        _projectiles.Add(projectile);
        projectile.transform.position = transform.position;
        projectile.Init(playerHealth, damage, target);
        return projectile;
    }

    public void RemoveProjectile(Projectile projectile)
    {
        _projectilesPool.Despawn(projectile);
        _projectiles.Remove(projectile);
    }
}
