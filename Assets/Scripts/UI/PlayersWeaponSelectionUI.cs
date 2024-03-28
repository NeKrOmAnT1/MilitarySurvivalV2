using UnityEngine;
using Zenject;

public class PlayersWeaponSelectionUI : WeaponSelectionUI
{
    [Inject]
    protected void Construct(WeaponCharacteristics[] weaponCharacteristics, PlayersWeaponSelection weaponSelection)
    {
        _weaponSelection = weaponSelection;
        FillDropDown(weaponCharacteristics);
    }
}
