using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class AllysAttack : MonoBehaviour
{   

    public WeaponCharacteristics WeaponCharacteristics { get; private set; }
    public SideType SideType { get; set; }

    private readonly Dictionary<string, WeaponCharacteristics> _weapons = new();



    [Inject]
    private void Construct(PlayerCharacteristics playerCharacteristics, WeaponCharacteristics[] weaponCharacteristics,
       WeaponSelection weaponSelection)
    {
        foreach (var weapon in weaponCharacteristics)
        {
            _weapons.Add(weapon.Name, weapon);
        }

        WeaponCharacteristics = _weapons[weaponSelection.CurrentWeaponName];
    }

    private void Awake()
    {
        SideType = SideType.ALLY;
        Debug.Log(WeaponCharacteristics.Name);
    }



}
