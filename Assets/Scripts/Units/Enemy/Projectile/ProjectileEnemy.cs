using System;
using UnityEngine;
using Zenject;

public class ProjectileEnemy : Enemy, ICanAttack
{
    [SerializeField] private Transform _firePoint;
    [SerializeField] private GameObject _projectilePrefab;
    private ProjectileFactory _projectileFactory; //crutch

    private bool _isAttack = false;
    private float _timer = 0;

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

    private void StartAttack()
    {
        _isAttack = false;
    }

    private void InitBullet()
    {
        Debug.Log(_projectileFactory);
        GameObject bullet = Instantiate(_projectilePrefab, transform.position, Quaternion.identity);
        var projectile = bullet.GetComponent<Projectile>();
        projectile.Init(_playerHealth, _damage, Target);
        projectile.Launch();

        //_projectileFactory.SpawnProjectile(this.transform.position, _playerHealth, _damage, Target)
        //    .Launch();
    }

    public void DetFactory(ProjectileFactory projectileFactory) =>//crutch
        _projectileFactory = projectileFactory;
}
