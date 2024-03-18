using UnityEngine;
using Zenject;

public class BootstrapInstaller : MonoInstaller
{
    [SerializeField] private HUD _hudPrefab;
    [SerializeField] private Bullet _bulletPrefab;
    [SerializeField] private Player _playerPrefab;
    [SerializeField] private CameraFollow _cameraPrefab;
    [SerializeField] private EnemySpawnManager _enemySpawnPrefab;
    [SerializeField] private Projectile _projectilePrefab;

    public override void InstallBindings()
    {
        Container.Bind<BulletFactory>().AsSingle();
        Container.BindMemoryPool<Bullet, Bullet.Pool>().FromComponentInNewPrefab(_bulletPrefab);

        Container.Bind<ProjectileFactory>().FromNew().AsSingle();
        Container.BindMemoryPool<Projectile, Projectile.Pool>().FromComponentInNewPrefab(_projectilePrefab);

        var player = Container.InstantiatePrefabForComponent<Player>(_playerPrefab);
        Container.Bind<Player>().FromInstance(player).AsSingle();

        var hud = Container.InstantiatePrefabForComponent<HUD>(_hudPrefab);
        Container.Bind<HUD>().FromInstance(hud).AsSingle();

        var camera = Container.InstantiatePrefabForComponent<CameraFollow>(_cameraPrefab);
        camera.Follow(player.transform);

        Container.InstantiatePrefabForComponent<EnemySpawnManager>(_enemySpawnPrefab);
    }
}