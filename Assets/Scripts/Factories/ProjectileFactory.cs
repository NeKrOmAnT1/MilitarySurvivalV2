using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class ProjectileFactory 
{
    //private readonly List<Projectile> _projectiles = new();

    //[Inject] private readonly Projectile.Pool _projectilesPool;
    //[Inject] private Player _player;

    //public Projectile SpawnProjectile(Transform spawnTransform, float damage)
    //{
    //    var projectile = _projectilesPool.Spawn();
    //    _projectiles.Add(projectile);
    //    projectile.transform.SetPositionAndRotation(spawnTransform.position, spawnTransform.rotation);

    //    projectile.Init(_player.PlayerHealth, damage, _player.transform, this);
    //    projectile.Launch();

    //    return projectile;
    //}

    //public void RemoveProjectile(Projectile projectile)
    //{
    //    _projectilesPool.Despawn(projectile);
    //    _projectiles.Remove(projectile);
    //}
}
