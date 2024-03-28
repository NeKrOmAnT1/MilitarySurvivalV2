using UnityEngine;
using Zenject;

public class AllysWeaponSelectionUI : WeaponSelectionUI
{
    [Inject]
    protected void Construct(WeaponCharacteristics[] weaponCharacteristics, AllysWeaponSelection weaponSelection)
    {
        _weaponSelection = weaponSelection;
        FillDropDown(weaponCharacteristics);
    }
}
