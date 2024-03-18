using UnityEngine;

public class ProgressSystem
{
    //private readonly XpSystem _xpSystem; //will probably be needed
    //private readonly MoneySystem _moneySystem;
    private readonly PlayerCharacteristics _playerCharacteristics;
    private readonly WeaponCharacteristics _weaponCharacteristics;
    private readonly ICoroutineRunner _coroutine;

    public ProgressSystem(XpSystem xpSystem, MoneySystem moneySystem,
        PlayerCharacteristics playerCharacteristics, WeaponCharacteristics weaponCharacteristics,
        ICoroutineRunner coroutine)
    {
        //_xpSystem = xpSystem;
        //_moneySystem = moneySystem;
        _playerCharacteristics = playerCharacteristics;
        _weaponCharacteristics = weaponCharacteristics;
        _coroutine = coroutine;

        Debug.Log(xpSystem);
        Debug.Log(moneySystem);
        Debug.Log(playerCharacteristics);
        Debug.Log(weaponCharacteristics);
        Debug.Log(coroutine);
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
            case ActiveSkill.AttackSpeed:
                skill = _weaponCharacteristics.AttackSpeed;
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