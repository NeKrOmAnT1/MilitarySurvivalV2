using TMPro;
using UnityEngine;
using Zenject;

public class WeaponSelectionUI : MonoBehaviour
{
    [SerializeField] private TMP_Dropdown _dropdown;
    
    private WeaponSelection _weaponSelection;

    [Inject]
    private void Construct(WeaponCharacteristics[] weaponCharacteristics, WeaponSelection weaponSelection)
    {
        _weaponSelection = weaponSelection;
        _dropdown.ClearOptions();

        foreach (var item in weaponCharacteristics)
        {
            _dropdown.options.Add((new TMP_Dropdown.OptionData() { text = item.Name }));
            _weaponSelection.AddDic(item.Name, item);
        }
    }

    private void Start() => 
        OnChoice();

    public void OnChoice() => 
        _weaponSelection.AddCurrentWeaponName(_dropdown.options[_dropdown.value].text);
}
