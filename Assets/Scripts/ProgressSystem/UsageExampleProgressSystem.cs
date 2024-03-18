using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Passes itself to the descendants of the BaseCharacteristics class as a CoroutineRunner
/// </summary>
public class UsageExampleProgressSystem : MonoBehaviour, ICoroutineRunner
{
    /// <summary>
    /// UI elements for customizing characteristics upgrades
    /// </summary>
    [SerializeField]
    private Dropdown _playersDropdown;
    [SerializeField]
    private Dropdown _weaponsDropdown;
    [SerializeField]
    private Slider _valueSlider;
    [SerializeField]
    private Toggle _toggle;
    [SerializeField]
    private Slider _lifetimeSlider;

    /// <summary>
    /// UI elements to demonstrate characteristics
    /// </summary>
    [Space, SerializeField]
    private Text _hpText;
    [Space, SerializeField]
    private Text _armourText;
    [Space, SerializeField]
    private Text _moveSpeedText;
    [Space, SerializeField]
    private Text _luclText;

    [Space, SerializeField]
    private Text _attackSpeedText;
    [SerializeField]
    private Text _cdText;
    [SerializeField]
    private Text _damageText;
    [SerializeField]
    private Text _distanceText;
    [SerializeField]
    private Text _spreadText;
    [SerializeField]
    private Text _bulletsNumberText;
    [SerializeField]
    private Text _damageAreaText;

    /// <summary>
    /// Default Feature Location. Customizable in the inspector
    /// </summary>
    private const string DefWeaponSOPath = "SO/DefaultWeaponSO";
    private const string DefPlayerSOPath = "SO/DefaultPlayerSO";

    private WeaponSO _weaponStats;
    private PlayerSO _playerStats;

    public WeaponCharacteristics WeaponCharacteristics { get; private set; }
    public PlayerCharacteristics PlayerCharacteristics { get; private set; }

    /// <summary>
    /// Modifier values
    /// </summary>
    private Stat _playersCharacteristic;
    private Stat _weaponsCharacteristic;
    private float _value;
    private TypeModifier _typeModifier;
    private float _lifetime;

    /// <summary>
    /// We create objects of characteristic classes, passing them default values. 
    /// In the future, all classes - users take them from here
    /// </summary>
    private void Start()
    {
        _weaponStats = Instantiate((Resources.Load<WeaponSO>(DefWeaponSOPath)));
        _playerStats = Instantiate(Resources.Load<PlayerSO>(DefPlayerSOPath));
        WeaponCharacteristics = new WeaponCharacteristics(_weaponStats);
        PlayerCharacteristics = new PlayerCharacteristics(_playerStats);
    }

    private void Update() =>
        UpdateText();

 #region Buff/DeBuff
    public void BuffPlayerConst()
    {
        AddPlayersParamsFromUI();
        PlayerCharacteristics.Buff(_playersCharacteristic, _value, _typeModifier);
    }

    public void BuffPlayerTemporarily()
    {
        AddPlayersParamsFromUI();
        _lifetime = _lifetimeSlider.value;
        PlayerCharacteristics.BuffTemporary(_playersCharacteristic, _value, _typeModifier, _lifetime, this);
    }

    public void BuffWeaponConst()
    {
        AddWeaponsParamsFromUI();
        WeaponCharacteristics.Buff(_weaponsCharacteristic, _value, _typeModifier);
    }

    public void BuffWeaponTemporarily()
    {
        AddWeaponsParamsFromUI();
        _lifetime = _lifetimeSlider.value;
        WeaponCharacteristics.BuffTemporary(_weaponsCharacteristic, _value, _typeModifier, _lifetime, this);
    }

    public void DebuffAll()
    {
        WeaponCharacteristics.DebuffAll();
        PlayerCharacteristics.DebuffAll();
    }
 #endregion Buff/DeBuff

 #region AddFromUI
    private void AddPlayersParamsFromUI()
    {
        _playersCharacteristic = AddPlayerCharacteristic();
        AddCommonParamsFromUI();
    }

    private void AddWeaponsParamsFromUI()
    {
        _weaponsCharacteristic = AddWeaponCharacteristic();
        AddCommonParamsFromUI();
    }

    private void AddCommonParamsFromUI()
    {
        _value = _valueSlider.value;
        _typeModifier = AddTypeMod();
    }

    private TypeModifier AddTypeMod()
    {
        if (_toggle.isOn)
            return TypeModifier.persentAdd;
        else
            return TypeModifier.flat;
    }

    private Stat AddPlayerCharacteristic()
    {
        var value = _playersDropdown.value;
        Stat stat;
        switch (value)
        {
            case 0:
                stat = PlayerCharacteristics.Hp;
                break;
            case 1:
                stat = PlayerCharacteristics.Armour;
                break;
            case 2:
                stat = PlayerCharacteristics.MoveSpeed;
                break;
            case 3:
                stat = PlayerCharacteristics.Luck;
                break;
            default:
                stat = PlayerCharacteristics.Hp;
                break;
        }
        return stat;
    }

    private Stat AddWeaponCharacteristic()
    {
        int value = _weaponsDropdown.value;
        Stat stat;

        switch (value)
        {
            case 0:
                stat = WeaponCharacteristics.AttackSpeed;
                break;
            case 1:
                stat = WeaponCharacteristics.CoolDown;
                break;
            case 2:
                stat = WeaponCharacteristics.DamageDealt;
                break;
            case 3:
                stat = WeaponCharacteristics.Distance;
                break;
            case 4:
                stat = WeaponCharacteristics.SpreadAngle;
                break;
            case 5:
                stat = WeaponCharacteristics.BulletsNumber;
                break;
            case 6:
                stat = WeaponCharacteristics.DamageArea;
                break;
            default:
                stat = WeaponCharacteristics.AttackSpeed;
                break;
        }

        return stat;
    }

 #endregion AddFromUI

    /// <summary>
    /// To demonstrate characteristics
    /// </summary>
    private void UpdateText()
    {
        _hpText.text = PlayerCharacteristics.Hp.Value.ToString();
        _armourText.text = PlayerCharacteristics.Armour.Value.ToString();
        _moveSpeedText.text = PlayerCharacteristics.MoveSpeed.Value.ToString();
        _luclText.text = PlayerCharacteristics.Luck.Value.ToString();

        _attackSpeedText.text = WeaponCharacteristics.AttackSpeed.Value.ToString();
        _cdText.text = WeaponCharacteristics.CoolDown.Value.ToString();
        _damageText.text = WeaponCharacteristics.DamageDealt.Value.ToString();
        _distanceText.text = WeaponCharacteristics.Distance.Value.ToString();
        _spreadText.text = WeaponCharacteristics.SpreadAngle.Value.ToString();
        _bulletsNumberText.text = WeaponCharacteristics.BulletsNumber.Value.ToString();
        _damageAreaText.text = WeaponCharacteristics.DamageArea.Value.ToString();
    }
}
