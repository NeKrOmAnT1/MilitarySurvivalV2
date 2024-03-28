using System.Collections.Generic;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(Animator))]
public class Allys : MonoBehaviour
{
    
    [SerializeField] private Transform _meleeAttackPoint;
    [SerializeField] private Transform _bulletspawnPoint;   
    [SerializeField] private SideType _sideType;     
    [SerializeField] private LayerMask _enemyMask;
    
    private static int _countAllys = 0;
    private BulletFactory _bulletFactory;
    private WeaponCharacteristics _weaponCharacteristics;
    private readonly Dictionary<string, WeaponCharacteristics> _weapons = new();
    public int NumberAlly { get { return _numberAlly; } }
    private int _numberAlly;

    [Inject]
    private void Construct(WeaponCharacteristics[] weaponCharacteristics, WeaponSelection weaponSelection, BulletFactory bulletFactory)
    {
        _bulletFactory = bulletFactory;
        foreach (var weapon in weaponCharacteristics)
        {
            _weapons.Add(weapon.Name, weapon);
        }

        _weaponCharacteristics = _weapons[weaponSelection.CurrentWeaponName];
    }
    private void Awake()
    {
        _numberAlly = ++_countAllys;

        gameObject.AddComponent<AllysMovement>();

        gameObject.AddComponent<AllysAttack>()
                  .Init(_bulletFactory, _bulletspawnPoint, _meleeAttackPoint, _enemyMask,
                   _weaponCharacteristics, _sideType, transform);
    }
}