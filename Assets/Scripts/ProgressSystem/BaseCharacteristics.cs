using System.Collections;
using UnityEngine;

public abstract class BaseCharacteristics
{
    /// <summary>
    /// To reduce characteristics when a weapon is injured/broken, etc., you should rename the methods
    /// from Buff to Change and pass negative values ​​to them. Or create new ones, since values ​​of the
    /// State type are changed through AddModifier.
    /// </summary>
    /// <param name="param">ActiveSkill we changed</param>
    /// <param name="value">The value to which we change</param>
    /// <param name="typeModifier">Percentage or absolute value</param>
    /// <param name="lifetime">Duration of the temporary change</param>
    /// <param name="coroutineRunner">Any MonoBehaviour that implements the ICoroutineRunner interface. 
    /// To get away from unnecessary MonoBehaviour</param>

    public void BuffTemporary(Stat param, float value, TypeModifier typeModifier, float lifetime, ICoroutineRunner coroutineRunner)
    {
        StatModifier modifier = Buff(param, value, typeModifier);
        coroutineRunner.StartCoroutine(DeBuffCoroutine(param, modifier, lifetime));
    }

    protected IEnumerator DeBuffCoroutine(Stat param, StatModifier modifier, float lifetime)
    {
        yield return new WaitForSeconds(lifetime);
        param.RemoveModifier(modifier);

        Debug.Log("DeBuff");
    }

    public abstract StatModifier Buff(Stat param, float value, TypeModifier typeModifier) ;
    public abstract void DebuffAll();
}
