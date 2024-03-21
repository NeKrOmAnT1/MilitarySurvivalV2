using System;
using UnityEngine;
using Zenject;

public class ProjectileEnemy : Enemy, ICanAttack
{
    [SerializeField] private Transform _firePoint;
    [SerializeField] private GameObject _projectilePrefab;

    [Inject]  private ProjectileFactory _projectileFactory; //crutch

    private bool _isAttack = false;
    private float _timer = 0;

    //[Inject]
    //public void Construct(ProjectileFactory projectileFactory)
    //{
    //    _projectileFactory = projectileFactory;
    //    Debug.Log(_projectileFactory);
    //}

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
        GameObject bullet = Instantiate(_projectilePrefab, transform.position, Quaternion.identity);
        var projectile = bullet.GetComponent<Projectile>();
        projectile.Init(_playerHealth, _damage, Target);
        projectile.Launch();

        //Debug.Log(_projectileFactory);
        //Debug.Log(_playerHealth);
        //Debug.Log(_damage);
        //Debug.Log(Target);

        //_projectileFactory.SpawnProjectile(this.transform.position, _playerHealth, _damage)
        //    .Launch();
    }

    public void SetFactory(ProjectileFactory projectileFactory) =>//crutch
        _projectileFactory = projectileFactory;
}
