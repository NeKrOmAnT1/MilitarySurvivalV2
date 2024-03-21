using UnityEngine;
using Zenject;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private Transform _bulletspawnPoint;
    [SerializeField] private LayerMask _enemyMask;

    [Space, SerializeField] private GameObject _testObject;

    private float _nextFire = 0.0f;
    private SideType _side;
    
    private BulletFactory _bulletFactory;

    [Inject]
    private void Construct(BulletFactory bulletFactory) => 
        _bulletFactory = bulletFactory;

    private void Start() => 
        _side = _player.SideType;

    private void Update()
    {
        if (Time.time > _nextFire)
        {
            Shoot();
            _nextFire = Time.time + _player.WeaponCharacteristics.CoolDown.Value;
        }
    }

    private void Shoot()
    {
        switch (_player.WeaponCharacteristics.WeaponType)
        {
            case WeaponType.Ranged:
                if(_player.WeaponCharacteristics.BulletsNumber.Value > 1)
                {
                    float offsetStep = _player.WeaponCharacteristics.SpreadAngle.Value / _player.WeaponCharacteristics.BulletsNumber.Value;
                    float playerRotationY = _player.transform.rotation.eulerAngles.y;
                    float centeringOffset = (_player.WeaponCharacteristics.SpreadAngle.Value / 2) - (offsetStep / 2);

                    for (int i = 0; i < _player.WeaponCharacteristics.BulletsNumber.Value; i++)
                    {
                        
                        float currentAngle = offsetStep * i;
                        Quaternion rot = Quaternion.Euler(new Vector3(0, playerRotationY + currentAngle - centeringOffset, 0));

                        Vector3 shootDirection = rot * Vector3.forward;

                        _bulletFactory.SpawnProjectile(_bulletspawnPoint, _side, _player.WeaponCharacteristics, shootDirection);
                    }
                }
                else
                {
                    float playerRotationY = _player.transform.rotation.eulerAngles.y;
                    Vector3 shootDirection = Quaternion.Euler(0, playerRotationY, 0) * Vector3.forward;
                    _bulletFactory.SpawnProjectile(_bulletspawnPoint, _side, _player.WeaponCharacteristics, shootDirection);
                }
                break;
            case WeaponType.Melee:

                Collider[] hitEnemies = Physics.OverlapSphere(transform.forward * _player.WeaponCharacteristics.Distance.Value, 
                    _player.WeaponCharacteristics.DamageArea.Value, _enemyMask);

                // for test
                var r = _player.WeaponCharacteristics.DamageArea.Value;
                _testObject.transform.localScale = new Vector3(r,r,r);
                _testObject.transform.position = transform.forward * _player.WeaponCharacteristics.Distance.Value;
                //for test end

                foreach (Collider collider in hitEnemies)
                {
                    if(collider.GetComponent<IDamagable>() != null && collider.GetComponent<IDamagable>().SideType != _player.SideType)
                    {
                        collider.GetComponent<EnemyHealth>().TakeDamage(_player.WeaponCharacteristics.DamageDealt.Value);
                        // TODO: Spawn weapon effect
                    }
                }

                break;
            default:
                break;
        } 
    }
}