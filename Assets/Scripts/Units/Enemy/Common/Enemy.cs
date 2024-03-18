using UnityEngine;

[RequireComponent (typeof(EnemyHealth), typeof(EnemyMovement))]
[RequireComponent(typeof(Rigidbody))]
public class Enemy : MonoBehaviour, IDamagable
{
    [SerializeField] protected EnemyMovement _enemyMovement;
    [SerializeField] protected EnemyDeath _enemyDeath;
    [SerializeField] protected float _damage = 5;
    [SerializeField] protected float _attackDelay = 5;
    [SerializeField] protected int _xpForKilling = 5;
    public SideType SideType { get; set; }
    public Transform Target { get; protected set; }
    public int XpForKilling { get => _xpForKilling; }

    protected PlayerHealth _playerHealth;
    protected bool _isfound;//for test. Remove after fixing the spawner

    public void Init(PlayerHealth playerHealth, Transform target)
    {
        _playerHealth = playerHealth;
        Target = target;
    }

    protected void LateUpdate()//for test. Remove after fixing the spawner
    {
        if (!_isfound)
        {
            var player = FindObjectOfType<Player>();
            Target = player.transform;

            if (Target != null)
            {
                _playerHealth = player.PlayerHealth;
                _isfound = true;
            }
        }
    }
}
