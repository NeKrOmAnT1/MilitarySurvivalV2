using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class BulletFactory
{
    private readonly List<Bullet> _bullet = new();

    [Inject] private readonly Bullet.Pool _bulletsPool;

    public Bullet SpawnBullet(Transform transform, SideType side, WeaponCharacteristics weapon, Vector3 direction)
    {
        var bullet = _bulletsPool.Spawn();
        _bullet.Add(bullet);
        bullet.transform.SetPositionAndRotation(transform.position, transform.rotation);
        bullet.Init(this, side, weapon, direction);
        return bullet;
    }

    public void RemoveBullet(Bullet bullet)
    {
        _bulletsPool.Despawn(bullet);
        _bullet.Remove(bullet);
    }
}