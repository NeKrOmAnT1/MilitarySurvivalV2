using System;
using System.Collections.Generic;

public class WeaponSelection
{
    public WeaponCharacteristics CurrentWeaponCharacteristics { get; private set; }
    public string CurrentWeaponName { get; private set; }

    private Dictionary<string, WeaponCharacteristics> _weaponCharDic = new();

    public event Action<WeaponCharacteristics> CangeWeapon;

    public void AddCurrentWeaponName(string currentWeaponName)
    {
        CurrentWeaponName = currentWeaponName;
        CurrentWeaponCharacteristics = _weaponCharDic[CurrentWeaponName];
        CangeWeapon?.Invoke(CurrentWeaponCharacteristics);
    }

    public void AddDic(string name, WeaponCharacteristics weaponCharacteristics) =>
        _weaponCharDic.Add(name, weaponCharacteristics);
}
