using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
[CreateAssetMenu(fileName = "New ActiveSkill UpCardSO", menuName = "ActiveSkill UpCardSO")]
public class ActiveSkillUpCardSO : BaseSkillUpCardSO
{
    //[Space]
    //[Header("Active Skill")]
    //public string Name;
    //public ActiveSkill ActiveSkill;
    //public float Value;
    //public TypeModifier Modifier;
    //public bool IsTEmporary;
    //public float Lifetime;
    //public Sprite Sprite;

    //todo  description 
}

[System.Serializable]
[CreateAssetMenu(fileName = "New BaseSkill UpCardSO", menuName = "BaseSkill UpCardSO")]

public class BaseSkillUpCardSO : ScriptableObject 
{
    public string Name;
    [Header("Choose an ActiveSkill or PassiveSkill")]
    [Tooltip ("No - if in PassiveSkill")]
    public ActiveSkill ActiveSkill;
    [Tooltip("No - if in ActiveSkill")]
    public PassiveSkill PassiveSkill;
    public float Value;
    public TypeModifier Modifier;
    public bool IsTEmporary;
    public float Lifetime;
    public Sprite Sprite;
    public float Price;
    public string Description;
}
