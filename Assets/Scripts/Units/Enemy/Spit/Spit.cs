using UnityEngine;

public class Spit : MonoBehaviour
{
    [SerializeField] private float _lifetime = 20;
    
    private PlayerHealth PlayerHealth;
    private Vector3 Target;
    private EnemyPool<Spit> _pool;
    private Quaternion TargetRotation;

    public float speed = 5f;
    public float Damage = 5;
    public float timer = 5f;

    public void Initialize(PlayerHealth playerHealth, float damage, Transform target, EnemyPool<Spit> pool)//, EnemyBulletFactory factory)
    {
        this.PlayerHealth = playerHealth;
        this.Damage = damage;
        this.Target = target.position;
        _pool = pool;
        // _factory = factory;

        timer = _lifetime;
    }

    //public void Initialize(PlayerHealth playerHealth, Vector3 Target, float damage, EnemyBulletFactory factory)
    //{
    //    this.PlayerHealth = playerHealth;
    //    this.Damage = damage;
    //    this.Target = Target;
    //    _factory = factory;

    //    timer = _lifetime;
    //}

    private void Start()
    {
        transform.LookAt(Target);
    }

    void Update()
    {
        // Move towards the target
        transform.Translate(Vector3.forward * speed * Time.deltaTime);

        timer -= Time.deltaTime;
        if (timer < 0f)
        {
            Destroy();
            Debug.Log("timer Destroy");
        }
    }

    void OnTriggerEnter(Collider other)
    {
        // Check if reached the target
        if (other.CompareTag("Player"))
        {
            // Damage the player
            DamagePlayer();

            // Destroy the spit object
            Destroy();
        }
    }

    private void Destroy()
    {
        //Destroy(gameObject);
        _pool.ReturnObject(this);
    }

    void DamagePlayer()
    {
        // Implement logic to damage the player
        
        if (PlayerHealth != null)
        {
            PlayerHealth.HealthReduce(Damage);
        }
    }
}
