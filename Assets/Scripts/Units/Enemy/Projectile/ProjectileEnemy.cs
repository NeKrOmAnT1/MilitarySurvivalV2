using System;
using UnityEngine;
using Zenject;

public class ProjectileEnemy : Enemy, ICanAttack
{
    [SerializeField] private Transform _firePoint;
    [SerializeField] private Projectile _projectilePrefab;
    [SerializeField] private int _bulletsPoolSize = 20;

    private bool _isAttack = false;
    private float _timer = 0;
    private EnemyBulletFactory _bulletFactory;

    private void Start() =>
        _bulletFactory = new(_projectilePrefab, this.transform, _bulletsPoolSize, _playerHealth/*, Target*/, _damage);

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
        //projectile.Initialize(_playerHealth, _damage, Target, _bulletFactory);
        //projectile.Launch();
        Debug.Log("InitBullet");
        Debug.Log(_bulletFactory);

        var projectile = _bulletFactory.Spawn(transform.position, Target.position);
        Debug.Log(Target.position);
        projectile.GetComponent<Projectile>().Launch();
    }
}
