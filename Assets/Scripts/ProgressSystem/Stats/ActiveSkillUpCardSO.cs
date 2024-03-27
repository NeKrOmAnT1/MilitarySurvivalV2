using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
[CreateAssetMenu(fileName = "New ActiveSkill UpCardSO", menuName = "ActiveSkill UpCardSO")]
public class ActiveSkillUpCardSO : BaseSkillUpCardSO
{
    [Space]
    [Header("Active Skill")]
    //public string Name;
    public ActiveSkill ActiveSkill;
    //public float Value;
    //public TypeModifier Modifier;
    //public bool IsTEmporary;
    //public float Lifetime;
    //public Sprite Sprite;

    //todo  description 
}

public class BaseSkillUpCardSO : ScriptableObject 
{
    public string Name;
    public float Value;
    public TypeModifier Modifier;
    public bool IsTEmporary;
    public float Lifetime;
    public Sprite Sprite;
    public string Description;
}
