using UnityEngine;
using Zenject;

public class Projectile : MonoBehaviour
{
    public class Pool : MonoMemoryPool<Projectile> { }

    [SerializeField] private GameObject _targetPrefab;
    [SerializeField] private GameObject _targetDamagePrefab;
    [Space]
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private SphereCollider _collider;
    [SerializeField] private Explosion _exp;
    [Space]
    [SerializeField] private float _h = 25;
    [SerializeField] private float _gravity = -18;
    [SerializeField] private float _damageArea = 10f;
    [SerializeField] private float _timeToDelete = 2f;

    private Transform _target;
    private PlayerHealth _playerHealth;
    private float _damage;
    private Vector3 _attackPos;

    public void Init(PlayerHealth playerHealth, float damage, Transform target)
    {
        _target = target;
        _playerHealth = playerHealth;
        _damage = damage;

        _rigidbody.useGravity = false;

        _attackPos = new Vector3(target.position.x, -0.95f, target.position.z);
        _exp.CreateSphere(_attackPos, _damageArea, _targetPrefab);
    }

    public void Launch()
    {
        Physics.gravity = Vector3.up * _gravity;
        _rigidbody.useGravity = true;
        _rigidbody.velocity = CalculateLaunchData().initialVelocity;
    }

    private LaunchData CalculateLaunchData()
    {
        float displacementY = _target.position.y - _rigidbody.position.y;
        Vector3 displacementXZ = new(_target.position.x - _rigidbody.position.x, 0, _target.position.z - _rigidbody.position.z);
        float time = Mathf.Sqrt(-2 * _h / _gravity) + Mathf.Sqrt(2 * (displacementY - _h) / _gravity);
        Vector3 velocityY = Vector3.up * Mathf.Sqrt(-2 * _gravity * _h);
        Vector3 velocityXZ = displacementXZ / time;

        return new LaunchData(velocityXZ + velocityY * -Mathf.Sign(_gravity), time);
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject.name);
        Damage();
        _exp.DeleteSphere();
        this.gameObject.SetActive(false);
        _exp.CreateDamageSphere(_attackPos, _damageArea, _targetDamagePrefab);
        Invoke("DeleteBullet", _timeToDelete);
    }

    private void Damage()
    {
        Collider[] player = Physics.OverlapSphere(_attackPos, _damageArea / 2);
        foreach (Collider collider in player)
        {
            if (collider.tag == "Player")
            {
                _playerHealth.HealthReduce(_damage);
            }
        }
    }

    readonly struct LaunchData
    {
        public readonly Vector3 initialVelocity;
        public readonly float timeToTarget;

        public LaunchData(Vector3 initialVelocity, float timeToTarget)
        {
            this.initialVelocity = initialVelocity;
            this.timeToTarget = timeToTarget;
        }

    }
}
