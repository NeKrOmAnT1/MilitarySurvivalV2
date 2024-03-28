using UnityEngine;

public class AllysAttack : MonoBehaviour
{  
    private WeaponCharacteristics _weaponCharacteristics; 
    private BulletFactory _bulletFactory;
    private Transform _bulletSpawnPoint;
    private Transform _meleeAttackPoint;
    private LayerMask _enemyMask;
    private float _nextFire = 0.0f;
    private SideType _sideType;
    private Transform _allyTransform;
      

    public void Init(BulletFactory bulletFactory, Transform bulletSpawnPoint, Transform meleeAttackPoint,
                     LayerMask enemyMask, WeaponCharacteristics weaponCharacteristics, SideType sideType, Transform allyTransform)
    {   
        _bulletFactory = bulletFactory;
        _bulletSpawnPoint = bulletSpawnPoint;
        _meleeAttackPoint = meleeAttackPoint;  
        _enemyMask = enemyMask;
        _weaponCharacteristics = weaponCharacteristics;
        _sideType = sideType;
        _allyTransform = allyTransform;
    }    
    private void Update()
    {
        if (Time.time > _nextFire)
        {
            Shoot();
            _nextFire = Time.time + _weaponCharacteristics.CoolDown.Value;
        }
    }

    private void Shoot()
    {
        switch (_weaponCharacteristics.WeaponType)
        {
            case WeaponType.Ranged:
                if (_weaponCharacteristics.BulletsNumber.Value > 1)
                {
                    float offsetStep = _weaponCharacteristics.SpreadAngle.Value / _weaponCharacteristics.BulletsNumber.Value;
                    float allysRotationY = _allyTransform.rotation.eulerAngles.y;
                    float centeringOffset = (_weaponCharacteristics.SpreadAngle.Value / 2) - (offsetStep / 2);

                    for (int i = 0; i < _weaponCharacteristics.BulletsNumber.Value; i++)
                    {

                        float currentAngle = offsetStep * i;
                        Quaternion rot = Quaternion.Euler(new Vector3(0, allysRotationY + currentAngle - centeringOffset, 0));

                        Vector3 shootDirection = rot * Vector3.forward;

                        _bulletFactory.SpawnBullet(_bulletSpawnPoint, _sideType, _weaponCharacteristics, shootDirection);
                    }
                }
                else
                {
                    float allysRotationY = _allyTransform.rotation.eulerAngles.y;
                    Vector3 shootDirection = Quaternion.Euler(0, allysRotationY, 0) * Vector3.forward;
                    _bulletFactory.SpawnBullet(_bulletSpawnPoint, _sideType, _weaponCharacteristics, shootDirection);
                }
                break;
            case WeaponType.Melee:

                Collider[] hitEnemies = Physics.OverlapSphere(_meleeAttackPoint.position,
                    _weaponCharacteristics.DamageArea.Value, _enemyMask);

                foreach (Collider collider in hitEnemies)
                {
                    if (collider.GetComponent<IDamagable>() != null && collider.GetComponent<IDamagable>().SideType != _sideType)
                    {
                        collider.GetComponent<EnemyHealth>().TakeDamage(_weaponCharacteristics.DamageDealt.Value);
                        // TODO: Spawn weapon effect
                    }
                }

                break;
            default:
                break;
        }
    }




}
