public class WeaponCharacteristics : BaseCharacteristics
{
    public WeaponType WeaponType { get; private set; }

    public Stat AttackSpeed { get; private set; }
    public Stat CoolDown { get; private set; }
    public Stat DamageDealt { get; private set; }
    public Stat Distance { get; private set; }
    public Stat SpreadAngle { get; private set; }
    public Stat BulletsNumber { get; private set; }
    public Stat DamageArea { get; private set; }


    public WeaponCharacteristics(WeaponSO weaponStats)
    {
        WeaponType = weaponStats.WeaponType;

        AttackSpeed = new(weaponStats.AttackSpeed);
        CoolDown = new(weaponStats.CoolDown);
        DamageDealt = new(weaponStats.DamageDealt);
        Distance = new(weaponStats.Distance);
        SpreadAngle = new(weaponStats.SpreadAngle);
        BulletsNumber = new(weaponStats.BulletsNumber);
        DamageArea = new(weaponStats.DamageArea);
    }

    public override void DebuffAll()
    {
        AttackSpeed.RemoveAllModifiers();
        CoolDown.RemoveAllModifiers();
        DamageDealt.RemoveAllModifiers();
        Distance.RemoveAllModifiers();
        SpreadAngle.RemoveAllModifiers();
        BulletsNumber.RemoveAllModifiers();
        DamageArea.RemoveAllModifiers();
    }

    public override StatModifier Buff(Stat param, float value, TypeModifier typeModifier)
    {
        StatModifier modifier = new(value, typeModifier);
        param.AddModifier(modifier);
        return modifier;
    }
}
