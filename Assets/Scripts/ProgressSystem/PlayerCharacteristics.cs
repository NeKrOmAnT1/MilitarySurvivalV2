using System;
using UnityEngine;

public class PlayerCharacteristics : BaseCharacteristics
{
    public Stat Hp { get; private set; }
    public Stat Armour { get; private set; }
    public Stat MoveSpeed { get; private set; }
    public Stat Luck { get; private set; }

    public event Action ChangeHPE;// for updating in PlayerHealth

    public PlayerCharacteristics(PlayerSO playerStats)
    {
        Hp = new(playerStats.Hp);
        Armour = new(playerStats.Armour);
        MoveSpeed = new(playerStats.MoveSpeed);
        Luck = new(playerStats.Luck);

        Debug.Log("Construct PlayerCharacteristics");
        //Debug.Log(Hp.Value);
        //Debug.Log(Armour.Value);
        //Debug.Log(MoveSpeed.Value);
        //Debug.Log(Luck.Value);
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

        //Debug.Log("Change PlayerCharacteristics");
        //Debug.Log(Hp.Value);
        //Debug.Log(Armour.Value);
        //Debug.Log(MoveSpeed.Value);
        //Debug.Log(Luck.Value);

        return modifier;
    }
}
