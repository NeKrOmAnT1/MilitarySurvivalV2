using System.Collections;
using UnityEngine;


public class ProjectileEnemy : Enemy, ICanAttack
{
    [SerializeField] private ProjectileOwner _owner => ProjectileOwner.ProjectileEnemy;

    private bool _isAttack = false;
    private float _timer = 0;

    //private ProjectileFactory _projectileFactory;

    private AmmoPool _pool;
    public void SetAmmoPool(AmmoPool ammoPool) => _pool = ammoPool;
    private EnemyPool<Projectile> _projectilePool;
    
    private void Start()
    {
        _projectilePool = _pool.GetProjectilePool(_owner);          // получаем конкретный пулл 
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

    //private void InitBullet()
    //{     
    //    Projectile projectile = _projectilePool.GetObject(); 
    
    //    projectile.transform.SetPositionAndRotation(transform.position, Quaternion.identity);



    //    projectile.Init(transform.rotation, _playerHealth, _damage, Target, this.transform.position);

    //    projectile.Launch();
    //}
    //public void AddFActory(ProjectileFactory projectileFactory) =>
    //    _projectileFactory = projectileFactory;


    private IEnumerator LaunchProjectileWithDelay(Projectile projectile)
    {
        projectile.Init(transform.rotation, _playerHealth, _damage, Target, this.transform.position);     
        yield return new WaitForSeconds(0.1f); // Задержка в секундах     
        projectile.Launch();
    }

    private void InitBullet()
    {
        Projectile projectile = _projectilePool.GetObject();
        projectile.transform.SetPositionAndRotation(transform.position, Quaternion.identity);

        StartCoroutine(LaunchProjectileWithDelay(projectile));
    }
}
