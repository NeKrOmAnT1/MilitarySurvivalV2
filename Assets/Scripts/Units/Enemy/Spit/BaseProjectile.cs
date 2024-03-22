using UnityEngine;

public abstract class BaseProjectile : MonoBehaviour
{
    public abstract void Initialize(PlayerHealth playerHealth, float damage, Vector3 target, 
        EnemyBulletFactory factory);
}