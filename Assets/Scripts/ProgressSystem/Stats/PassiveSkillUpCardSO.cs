using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "New PassiveSkill UpCardSO", menuName = "PassiveSkill UpCardSO")]
public class PassiveSkillUpCardSO : ScriptableObject
{
    [Space]
    [Header("Passive Skill")]
    public PassiveSkill PassiveSkill;
    public float Value;
    public TypeModifier PassiveModifier;
    public bool IsTEmporary;
    public float Lifetime;
}
