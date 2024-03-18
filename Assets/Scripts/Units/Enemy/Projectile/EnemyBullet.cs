using System.Collections;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    [SerializeField] private ProjectileEnemy _enemy;
    [SerializeField] private Explosion _exp;
    [SerializeField] private GameObject _explosionPrefab;
    [SerializeField] private GameObject _explosionDamagePrefab;
    [Space]
    [SerializeField] private float _timeToDelete = 2f;
    [SerializeField] private float _damageArea = 10f;
    [SerializeField] private AnimationCurve _curve;
    [SerializeField] private float _desiredFlightTime = 2f;

    private PlayerHealth playerHealth;
    private float damage;
    private Vector3 attackPos;

    private float bulletSpeed;
    public float ballHeigh = 10f;

    IEnumerator coroutine = null;

    public void Initialize(PlayerHealth playerHealth, float damage, Vector3 attackPos)
    {
        this.playerHealth = playerHealth;
        this.damage = damage;
        this.attackPos = attackPos;
        _exp.CreateSphere(attackPos, _damageArea, _explosionPrefab);

        // –ассчитываем скорость снар€да на основе желаемого времени полЄта и рассто€ни€ до цели
        bulletSpeed = Vector3.Distance(transform.position, attackPos) / _desiredFlightTime;

    }

    private void Update()
    {
        Trajectory();
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.gameObject.name);
        Damage();
        _exp.DeleteSphere();
        this.gameObject.SetActive(false);
        _exp.CreateDamageSphere(attackPos, _damageArea, _explosionDamagePrefab);
        Invoke(nameof(DeleteBullet), _timeToDelete);
    }

    void DeleteBullet()
    {
        _exp.DeleteDamageSphere();
        Destroy(this.gameObject);
    }

    void Damage()
    {
        Collider[] player = Physics.OverlapSphere(attackPos, _damageArea / 2);
        foreach (Collider collider in player)
        {
            if (collider.tag == "Player")
            {
                playerHealth.HealthReduce(damage);
            }
        }
    }

    private void Trajectory()
    {
        if (coroutine == null)
        {
            coroutine = FollowCurve();
            StartCoroutine(coroutine);
        }
    }

    private IEnumerator FollowCurve()
    {
        Vector3 pathVector = attackPos - transform.position;
        float totalDistance = pathVector.magnitude;
        Vector3 normalizedDirection = pathVector.normalized; // Ќормализованный вектор направлени€

        float distanceTravelled = 0f;
        float ballRadius = transform.localScale.y / 2f ;

        Vector3 newPosition = transform.position;

        while (distanceTravelled <= totalDistance)
        {
            Vector3 deltaPath = normalizedDirection * (bulletSpeed * Time.deltaTime);
            newPosition += deltaPath;
            distanceTravelled += deltaPath.magnitude;

            float normalizedTime = distanceTravelled / totalDistance;
            newPosition.y = ballRadius + (ballHeigh * _curve.Evaluate(normalizedTime));

            transform.position = newPosition;

            yield return null;
        }

        coroutine = null;
    }
}

