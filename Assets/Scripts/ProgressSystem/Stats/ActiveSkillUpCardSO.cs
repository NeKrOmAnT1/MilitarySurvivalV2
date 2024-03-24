using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "New ActiveSkill UpCardSO", menuName = "ActiveSkill UpCardSO")]
public class ActiveSkillUpCardSO : ScriptableObject
{
    public string _name;
    [Space]
    [Header("Active Skill")]
    public ActiveSkill ActiveSkill;
    public float Value;
    public TypeModifier Modifier;
    public bool IsTEmporary;
    public float Lifetime;

    //todo picture and  description 
}
