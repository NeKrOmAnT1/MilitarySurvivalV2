using UnityEngine;

public class ProjectileEnemy : Enemy, ICanAttack
{
    [SerializeField] private Transform _firePoint;
    [SerializeField] private Projectile _projectilePrefab;
    [SerializeField] private int _bulletsPoolSize = 20;

    private bool _isAttack = false;
    private float _timer = 0;

    private EnemyPool<Projectile> _pool;

    private void Start()
    {
        _pool = new(_projectilePrefab, _bulletsPoolSize, this.transform);
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
        //projectile.Launch();

        var bullet = _pool.GetObject();
        bullet.transform.parent = null;
        var projectile = bullet.GetComponent<Projectile>();
        projectile.Init(_playerHealth, _damage, Target);
        projectile.Launch();
    }
}
