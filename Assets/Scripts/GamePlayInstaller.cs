using UnityEngine;
using Zenject;

public class GamePlayInstaller : MonoInstaller
{
    [SerializeField] private HUD _hudPrefab;
    [SerializeField] private Bullet _bulletPrefab;
    [SerializeField] private Player _playerPrefab;
    [SerializeField] private CameraFollow _cameraPrefab;
    [SerializeField] private EnemySpawnManager _enemySpawnPrefab;
    [SerializeField] private PlayerSO _playerSO;
    [SerializeField] private Transform _playerSpawnPoint;


    private CameraFollow _camera;

    public override void InstallBindings()
    {
        InstallFactories();

        InstallPlayerCharacteristics();
        
        InstallCamera();

        InstallPlayer();

        InstallHud();


        InstallEnemySpawnManager();

        Container.Bind<XpSystem>().FromNew().AsSingle().NonLazy();
    }


    private void InstallEnemySpawnManager()
    {
        var spawner = Container.InstantiatePrefabForComponent<EnemySpawnManager>(_enemySpawnPrefab);
        Container.Bind<EnemySpawnManager>().FromInstance(spawner).AsSingle().NonLazy();
    }

    private void InstallCamera()
    {
        _camera = Container.InstantiatePrefabForComponent<CameraFollow>(_cameraPrefab);
        Container.Bind<CameraFollow>().FromInstance(_camera).AsSingle().NonLazy();
        
    }

    private void InstallHud()
    {
        var hud = Container.InstantiatePrefabForComponent<HUD>(_hudPrefab);
        Container.Bind<HUD>().FromInstance(hud).AsSingle();
    }

    private void InstallPlayer()
    {
        var player = Container.InstantiatePrefabForComponent<Player>(_playerPrefab);
        Container.Bind<Player>().FromInstance(player).AsSingle().NonLazy();
        player.gameObject.transform.position = _playerSpawnPoint.position;
        _camera.Follow(player.transform);
    }

    private void InstallFactories()
    {
        Container.Bind<BulletFactory>().AsSingle();
        Container.BindMemoryPool<Bullet, Bullet.Pool>().FromComponentInNewPrefab(_bulletPrefab);
    }

    private void InstallPlayerCharacteristics() => 
        Container.Bind<PlayerCharacteristics>().FromNew().AsSingle().WithArguments(_playerSO).NonLazy();
}
