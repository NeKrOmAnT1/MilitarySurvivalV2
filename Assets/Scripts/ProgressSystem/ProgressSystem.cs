using System;
using UnityEngine;

public class ProgressSystem
{
    private readonly XpSystem _xpSystem;
    private readonly MoneySystem _moneySystem;
    private readonly PlayerCharacteristics _playerCharacteristics;
    private WeaponCharacteristics _weaponCharacteristics;
    private readonly ICoroutineRunner _coroutine;
    private readonly WeaponSelection _weaponSelection;

    public event Action EnableUpgradeMenuE;
    public event Action DisableUpgradeMenuE;

    public ProgressSystem(XpSystem xpSystem, MoneySystem moneySystem,
        PlayerCharacteristics playerCharacteristics, ICoroutineRunner coroutine, WeaponSelection weaponSelection)
    {
        _xpSystem = xpSystem;
        _moneySystem = moneySystem;
        _playerCharacteristics = playerCharacteristics;
        _coroutine = coroutine;
        _weaponSelection = weaponSelection;
        _weaponCharacteristics = _weaponSelection.CurrentWeaponCharacteristics;

        _xpSystem.XpIsFull += EnterUpgradeMenu;
        weaponSelection.CangeWeapon += CangeWeapon;
    }

    private void CangeWeapon(WeaponCharacteristics weaponCharacteristics) => 
        _weaponCharacteristics = weaponCharacteristics;

    private void EnterUpgradeMenu()
    {
        Time.timeScale = 0;
        EnableUpgradeMenuE?.Invoke();
    }

    public void AcceptGrade(GradeSO grade)
    {
        AcceptSkill(_weaponCharacteristics, grade.ActiveIsTEmporary_1,
            DefineActiveSkill(grade.ActiveSkill_1), grade.ActiveValue_1, grade.ActiveModifier_1,
            grade.ActiveLifetime_1);


        AcceptSkill(_weaponCharacteristics, grade.ActiveIsTEmporary_2,
            DefineActiveSkill(grade.ActiveSkill_2), grade.ActiveValue_2, grade.ActiveModifier_2,
            grade.ActiveLifetime_2);


        AcceptSkill(_playerCharacteristics, grade.PassiveIsTEmporary_1,
            DefinePassiveSkill(grade.PassiveSkill_1), grade.PassiveValue_1, grade.PassiveModifier_1,
            grade.PassiveLifetime_1);

        AcceptSkill(_playerCharacteristics, grade.PassiveIsTEmporary_2,
            DefinePassiveSkill(grade.PassiveSkill_2), grade.PassiveValue_2, grade.PassiveModifier_2,
            grade.PassiveLifetime_2);

        Time.timeScale = 1;
        DisableUpgradeMenuE?.Invoke();
        _xpSystem.Reset();
    }

    private void AcceptSkill(BaseCharacteristics characteristics, bool isTEmporary, Stat skill,
        float value, TypeModifier modifier, float lifetime)
    {
        if (isTEmporary)
            characteristics.BuffTemporary(skill, value, modifier, lifetime, _coroutine);
        else
            characteristics.Buff(skill, value, modifier);
    }

    private Stat DefineActiveSkill(ActiveSkill activeSkill)
    {
        Stat skill;

        switch (activeSkill)
        {
            case ActiveSkill.BulletSpeed:
                skill = _weaponCharacteristics.BulletSpeed;
                break;
            case ActiveSkill.CoolDown:
                skill = _weaponCharacteristics.CoolDown;
                break;
            case ActiveSkill.DamageDealt:
                skill = _weaponCharacteristics.DamageDealt;
                break;
            case ActiveSkill.Distance:
                skill = _weaponCharacteristics.Distance;
                break;
            case ActiveSkill.SpreadAngle:
                skill = _weaponCharacteristics.SpreadAngle;
                break;
            case ActiveSkill.BulletsNumber:
                skill = _weaponCharacteristics.BulletsNumber;
                break;

            default:
                skill = _weaponCharacteristics.DamageArea;
                break;
        }

        return skill;
    }

    private Stat DefinePassiveSkill(PassiveSkill passiveSkill)
    {
        Stat skill;

        switch (passiveSkill)
        {
            case PassiveSkill.Hp:
                skill = _playerCharacteristics.Hp;
                break;
            case PassiveSkill.Armour:
                skill = _playerCharacteristics.Armour;
                break;
            case PassiveSkill.MoveSpeed:
                skill = _playerCharacteristics.MoveSpeed;
                break;

            default:
                skill = _playerCharacteristics.Luck;
                break;
        }

        return skill;
    }
}