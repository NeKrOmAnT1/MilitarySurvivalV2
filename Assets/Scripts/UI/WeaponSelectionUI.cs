using TMPro;
using UnityEngine;

public class WeaponSelectionUI : MonoBehaviour
{
    [SerializeField] protected TMP_Dropdown _dropdown;

    protected WeaponSelection _weaponSelection;

    protected void FillDropDown(WeaponCharacteristics[] weaponCharacteristics)
    {
        _dropdown.ClearOptions();

        foreach (var item in weaponCharacteristics)
        {
            _dropdown.options.Add((new TMP_Dropdown.OptionData() { text = item.Name }));
            _weaponSelection.AddDic(item.Name, item);
        }
    }

    protected void Start() =>
        OnChoice();

    public void OnChoice() => 
        _weaponSelection.AddCurrentWeaponName(_dropdown.options[_dropdown.value].text);
}
