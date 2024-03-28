using System.Collections;
using UnityEngine;
using Zenject;

public class Bullet : MonoBehaviour
{
    public class Pool : MonoMemoryPool<Bullet> { }

    [SerializeField] private float _destroyDelay = 5f;

    [SerializeField]private SideType _side;
    private Vector3 _direction; // Направление движения пули
    private BulletFactory _factory;

    private WeaponCharacteristics _characteristics;

    private bool _isActive;// in order not to return Bullet to the pool twice

    public void Init(BulletFactory factory, SideType side, WeaponCharacteristics weapon, Vector3 dir)
    {
        _isActive = true;

        _side = side;
        _characteristics = weapon;
        _direction = dir.normalized;
        _factory = factory;

        // Уничтожаем пулю через заданное время
        StartCoroutine(DestriyRoutine());
    }

    private void Update() =>
        // Двигаем пулю в заданном направлении
        transform.Translate(_characteristics.BulletSpeed.Value * Time.deltaTime * _direction, Space.World);

    private void OnCollisionEnter(Collision collision)
    {
        if (_isActive)
        {
            if (_side != collision.gameObject.GetComponent<IDamagable>().SideType)
            {
                collision.gameObject.GetComponent<EnemyHealth>()
                    .TakeDamage(_characteristics.DamageDealt.Value);
            }
            Despawn();
        }
    }

    private IEnumerator DestriyRoutine()
    {
        yield return new WaitForSeconds(_destroyDelay);
        Despawn();
    }

    private void Despawn()
    {
        _factory.RemoveBullet(this);
        _isActive = false;
    }
}

public enum SideType
{
    ENEMY,
    ALLY
}