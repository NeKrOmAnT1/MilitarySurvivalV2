using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "New GradeSO", menuName = "GradeSO")]

public class GradeSO : ScriptableObject
{
    public string _name;
    [Space]
    [Header("Active Skills")]
    public ActiveSkill ActiveSkill_1;
    public float ActiveValue_1;
    public TypeModifier ActiveModifier_1;
    public bool ActiveIsTEmporary_1;
    public float ActiveLifetime_1;
    [Space]
    public ActiveSkill ActiveSkill_2;
    public float ActiveValue_2;
    public TypeModifier ActiveModifier_2;
    public bool ActiveIsTEmporary_2;
    public float ActiveLifetime_2;
    [Space]
    [Header("Passive Skills")]
    public PassiveSkill PassiveSkill_1;
    public float PassiveValue_1;
    public TypeModifier PassiveModifier_1;
    public bool PassiveIsTEmporary_1;
    public float PassiveLifetime_1;
    [Space]
    public PassiveSkill PassiveSkill_2;
    public float PassiveValue_2;
    public TypeModifier PassiveModifier_2;
    public bool PassiveIsTEmporary_2;
    public float PassiveLifetime_2;

}
