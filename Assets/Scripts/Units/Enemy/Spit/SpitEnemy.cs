using System.ComponentModel;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;
using UnityEngine.UIElements;

public class SpitEnemy : Enemy, ICanAttack
{
    [SerializeField] private Spit _spitPrefab;
    [SerializeField] private int _bulletsPoolSize = 20;

    private bool _isAttack = false;
    private float _timer = 0;
    private EnemyPool<Spit> _pool;

    //private EnemyBulletFactory _bulletFactory;

    private void Start() =>
        _pool = new EnemyPool<Spit>(_spitPrefab, _bulletsPoolSize, this.transform);
    //_bulletFactory = new(_testspitPrefab, this.transform, _bulletsPoolSize, _playerHealth/*, Target*/, _damage);
    
    private void SpittingAttack()
    {
        // Replace "SpitPrefab" with the actual prefab you want to instantiate
        //Use pulls and factories in the same way as a bullet
        //GameObject spitObject = Instantiate(_spitPrefab, transform.position, Quaternion.identity);
        //Spit spitComponent = spitObject.GetComponent<Spit>();

        //spitComponent.Initialize(_playerHealth, Target.position, _damage);

        //_bulletFactory.Spawn(transform.position, Target.position);
        var proj = _pool.GetObject();
        proj.transform.position = transform.position;
        proj.Initialize(_playerHealth, _damage, Target, _pool);
        _isAttack = false;
    }

    public void AttackProcess()
    {
        if (_enemyDeath.IsDead) return;

        _timer -= Time.deltaTime;
        if (_timer < 0f && _isAttack == false)
        {
            _isAttack = true;
            SpittingAttack();
            _timer = _attackDelay;
        }
    }
}
