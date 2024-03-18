using UnityEngine;
using Zenject;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private Transform _bulletspawnPoint;

    private float _nextFire = 0.0f;
    private SideType _side;

    [Inject]
    private readonly BulletFactory _bulletFactory;

    private void Start() => 
        _side = _player.SideType;

    private void Update()
    {
        if (Time.time > _nextFire)
        {
            Shoot();
            _nextFire = Time.time + 1 / _player.WeaponCharacteristics.CoolDown.Value;
        }
    }

    private void Shoot()
    {
        float playerRotationY = _player.transform.rotation.eulerAngles.y;
        Vector3 shootDirection = Quaternion.Euler(0, playerRotationY, 0) * Vector3.forward;

        _bulletFactory.SpawnProjectile(_bulletspawnPoint, _side, _player.WeaponCharacteristics, shootDirection);
    }
}