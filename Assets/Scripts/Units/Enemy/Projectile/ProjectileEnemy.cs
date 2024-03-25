using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using Zenject;

public class ProjectileEnemy : Enemy, ICanAttack
{
    [SerializeField] private Transform _firePoint;
    [SerializeField] private GameObject _projectilePrefab;
    [SerializeField] private int _bulletsPoolSize = 20;


    [SerializeField] private int _bulletsPoolSize = 20;
    [SerializeField] private ProjectileOwner _owner => ProjectileOwner.ProjectileEnemy;


    private bool _isAttack = false;
    private float _timer = 0;


    //private ProjectileFactory _projectileFactory;

    private AmmoPool _pool;
    private ObjectPool<Projectile> _projectilePool;

    
    //private void Start() =>
    //  _projectilePool = new EnemyPool<Projectile>(_projectilePrefab.GetComponent<Projectile>(), _bulletsPoolSize, this.transform);

    [Inject]
    private void Construct(AmmoPool ammo)
    {
        _pool = ammo;    
       
    }

    private void Start()
    {

        //_enemyPool = _pool.GetComponent<EnemyPool<Projectile>>();
        _projectilePool = _pool.GetPool(_owner, _owner);
       Debug.Log(_pool);
    }



    public void AttackProcess()
    {
        if (_enemyDeath.IsDead) return;

        _timer -= Time.deltaTime;
        if (_timer < 0f && _isAttack == false)
        {
            _isAttack = true;
            StartAttack();
            InitBullet();
            _timer = _attackDelay;         
        }
    }

    private void StartAttack() =>
        _isAttack = false;

    private void InitBullet()
    {

        //GameObject bullet = Instantiate(_projectilePrefab, transform.position, Quaternion.identity);
        //var projectile = bullet.GetComponent<Projectile>();
        //projectile.Init(_playerHealth, _damage, Target);

        //GameObject bullet = Instantiate(_projectilePrefab, transform.position, Quaternion.identity);
        //var projectile = bullet.GetComponent<Projectile>();
        //projectile.Init(_playerHealth, _damage, Target);
        //projectile.Launch();

        //_projectileFactory.SpawnProjectile(transform, _damage);

        _projectilePool = _pool.GetPool(_owner, _owner);

        Projectile projectile = _projectilePool.Get(); //_enemyPool.GetObject();
    
        projectile.transform.SetPositionAndRotation(transform.position, Quaternion.identity);

        projectile.Init(transform.rotation, _playerHealth, _damage, Target, this.transform.position);

        projectile.Launch();


    }


    //public void AddFActory(ProjectileFactory projectileFactory) =>
    //    _projectileFactory = projectileFactory;
}
