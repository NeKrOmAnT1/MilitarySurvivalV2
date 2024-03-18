using UnityEngine;

public class ProjectileEnemy : Enemy, ICanAttack
{
    //[SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private Transform _firePoint;
    [SerializeField] private GameObject _testBulletPrefab;

    private Vector3 _attackPos;
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
            Trajectory();
            _timer = _attackDelay;
        }
    }

    private void StartAttack()
    {
        _attackPos = new Vector3(Target.position.x, -1f, Target.position.z);
        _isAttack = false;
    }

    private void Trajectory()
    {
        //GameObject newBullet = Instantiate(_bulletPrefab, _firePoint.position, Quaternion.identity);
        //newBullet.GetComponent<EnemyBullet>().Initialize(_playerHealth, _damage, _attackPos);

        GameObject bullet = Instantiate(_testBulletPrefab, transform.position, Quaternion.identity);
        var test = bullet.GetComponent<TestBullet>();
            test.Init(_playerHealth, _damage, Target);
        test.Launch();

    }
}
