using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class ProjectileFactory 
{
    private readonly List<Projectile> _projectiles = new();

    [Inject] private readonly Projectile.Pool _projectilesPool;

    public Projectile SpawnProjectile(Vector3 position, PlayerHealth playerHealth, float damage)
    {
        var projectile = _projectilesPool.Spawn();
        _projectiles.Add(projectile);
        projectile.transform.position = position;
        //projectile.Init(damage);
        return projectile;
    }

    public void RemoveProjectile(Projectile projectile)
    {
        _projectilesPool.Despawn(projectile);
        _projectiles.Remove(projectile);
    }
}
