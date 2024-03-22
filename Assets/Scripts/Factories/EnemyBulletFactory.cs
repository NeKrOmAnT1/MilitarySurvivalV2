using UnityEngine;

public class EnemyBulletFactory
{
    private EnemyPool<BaseProjectile> _pool;
    private readonly PlayerHealth _playerHealth;
    private readonly Transform _target;
    private readonly float _damage;

    public EnemyBulletFactory(BaseProjectile prefab, Transform container, int poolSize, 
        PlayerHealth playerHealth/*, Transform target*/, float damage)
    {
        _pool = new EnemyPool<BaseProjectile>(prefab, poolSize, container);
        _playerHealth = playerHealth;
        //_target = target;
        _damage = damage;
    }

    public BaseProjectile Spawn(Vector3 position, Vector3 target)
    {
        Debug.Log(_target.position);
        var proj = _pool.GetObject();
        proj.transform.position = position;
        proj.Initialize(_playerHealth, _damage, target,  this);
        return proj;
    }

    public void DeSpawn(BaseProjectile proj) => 
        _pool.ReturnObject(proj);
}
