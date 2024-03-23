using UnityEngine;

public class ProjectileEnemy : Enemy, ICanAttack
{
    [SerializeField] private Transform _firePoint;
    [SerializeField] private GameObject _projectilePrefab;
    [SerializeField] private int _bulletsPoolSize = 20;


    private bool _isAttack = false;
    private float _timer = 0;

    //private ProjectileFactory _projectileFactory;

    private EnemyPool<Projectile> _pool;

    private void Start() =>
        _pool = new EnemyPool<Projectile>(_projectilePrefab.GetComponent<Projectile>(), _bulletsPoolSize, this.transform);

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
        //projectile.Launch();

        //_projectileFactory.SpawnProjectile(transform, _damage);

        Projectile projectile = _pool.GetObject();
        projectile.transform.SetPositionAndRotation(transform.position, transform.rotation);
        projectile.Init(_playerHealth, _damage, Target);
        projectile.Launch();
    }


    //public void AddFActory(ProjectileFactory projectileFactory) =>
    //    _projectileFactory = projectileFactory;
}
