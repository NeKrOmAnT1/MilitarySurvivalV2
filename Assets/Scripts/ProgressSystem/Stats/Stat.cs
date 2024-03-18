using System.Collections.Generic;

public class Stat
{
    public float Value
    {
        get
        {
            if (isDirty)
            {
                lastValue = CalcStat();
                isDirty = false;
            }
            return lastValue;
        }
    }
    readonly float BaseValue;
    readonly List<StatModifier> Modifiers = new();
    readonly bool isNegativeable = false;

    bool isDirty = true;
    float lastValue;

    public Stat(float baseValue = 0, bool isNegativeable = false)
    {
        BaseValue = baseValue;
        this.isNegativeable = isNegativeable;
    }

 #region ModifierSystem
    private int CompareModFunc(StatModifier _a, StatModifier _b)
    {
        if (_a.order < _b.order)
            return -1;
        else if (_a.order > _b.order)
            return 1;
        return 0;
    }
    public void AddModifier(StatModifier _mod)
    {
        Modifiers.Add(_mod);
        isDirty = true;
        Modifiers.Sort(CompareModFunc);
    }
    
    public bool RemoveModifier(StatModifier _mod)
    {
        if (Modifiers.Remove(_mod))
        {
            isDirty = true;
            return true;
        }
        return false;
    }

    public void RemoveAllModifiers()
    {
        Modifiers.Clear();
        isDirty = true;
    }

    float CalcStat()
    {
        float finalValue = BaseValue;

        foreach (StatModifier modifier in Modifiers) 
        {
            if (modifier.type == TypeModifier.flat)
            {
                finalValue += modifier.value;
            }
            else
            {
                finalValue *= 1 + modifier.value / 100;
            }
        }
        
        if (!isNegativeable && finalValue < 0)
            return 0;

        return finalValue;
    }
 #endregion
}