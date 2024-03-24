using System;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSelection
{
    public WeaponCharacteristics CurrentWeaponCharacteristics { get; private set; }
    public string CurrentWeaponName { get; private set; }

    private readonly Dictionary<string, WeaponCharacteristics> _weaponCharDic = new();

    public event Action<WeaponCharacteristics> CangeWeapon;

    public void AddCurrentWeaponName(string currentWeaponName)
    {
        CurrentWeaponName = currentWeaponName;
        CurrentWeaponCharacteristics = _weaponCharDic[CurrentWeaponName];
        CangeWeapon?.Invoke(CurrentWeaponCharacteristics);

        Debug.Log("CurrentWeapon " + CurrentWeaponCharacteristics.Name);
        //Debug.Log("CurrentWeapon " + CurrentWeaponCharacteristics.DamageDealt.Value);
        //Debug.Log("CurrentWeapon " + CurrentWeaponCharacteristics.CoolDown.Value);
        //Debug.Log("CurrentWeapon " + CurrentWeaponCharacteristics.BulletSpeed.Value);
    }

    public void AddDic(string name, WeaponCharacteristics weaponCharacteristics) => 
        _weaponCharDic.Add(name, weaponCharacteristics);
}
