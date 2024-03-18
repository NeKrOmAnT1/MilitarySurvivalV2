using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private Enemy enemy;
    [SerializeField] private EnemyDeath _enemyDeath;
    [Space]
    [SerializeField] private float movementSpeed = 2;
    [SerializeField] private float attackDistance = 2;
    [SerializeField] private float rotationSpeed = 7;

    private ICanAttack attack;

    private void Start() => 
        attack = enemy.GetComponent<ICanAttack>();//for some reason it's not open in the inspector with SerializeField

    void Update()
    {
        if (enemy.Target != null && !_enemyDeath.IsDead)
        {
            Movement();
        }
    }

    private void Movement()
    {
        Vector3 direction = enemy.Target.position - transform.position;
        Quaternion toRotation = Quaternion.LookRotation(new Vector3(direction.x, 0f, direction.z), Vector3.up);
        transform.rotation = Quaternion.Lerp(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);

        if (!PlayerIsClose())
        {
            transform.Translate(0f, 0f, movementSpeed * Time.deltaTime);
        }
        else
        {
            try
            {
                attack.AttackProcess();
            }
            catch
            {
                return;
            }
        }
    }

    private bool PlayerIsClose()
    {
        float distance = Vector3.Distance(transform.position, enemy.Target.position);

        return distance <= attackDistance;
    }
}
