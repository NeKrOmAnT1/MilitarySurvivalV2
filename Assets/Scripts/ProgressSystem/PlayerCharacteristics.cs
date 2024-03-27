using System;

public class PlayerCharacteristics : BaseCharacteristics
{
    public Stat Regen { get; private set; }
    public Stat Hp { get; private set; }
    public Stat Armour { get; private set; }
    public Stat MoveSpeed { get; private set; }
    public Stat Luck { get; private set; }

    public event Action ChangeHPE;// for updating in PlayerHealth

    public PlayerCharacteristics(PlayerSO playerStats)
    {
        Regen = new(playerStats.Regen);
        Hp = new(playerStats.Hp);
        Armour = new(playerStats.Armour);
        MoveSpeed = new(playerStats.MoveSpeed);
        Luck = new(playerStats.Luck);
    }

    public override void DebuffAll()
    {
        Hp.RemoveAllModifiers();
        Armour.RemoveAllModifiers();
        MoveSpeed.RemoveAllModifiers();
        Luck.RemoveAllModifiers();
    }

    public override StatModifier Buff(Stat param, float value, TypeModifier typeModifier)
    {
        StatModifier modifier = new(value, typeModifier);
        param.AddModifier(modifier);
        ChangeHPE?.Invoke();
        return modifier;
    }
}
