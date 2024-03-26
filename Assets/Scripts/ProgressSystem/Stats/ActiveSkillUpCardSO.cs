using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
[CreateAssetMenu(fileName = "New ActiveSkill UpCardSO", menuName = "ActiveSkill UpCardSO")]
public class ActiveSkillUpCardSO : ScriptableObject
{
    [Space]
    [Header("Active Skill")]
    public string Name;
    public ActiveSkill ActiveSkill;
    public float Value;
    public TypeModifier Modifier;
    public bool IsTEmporary;
    public float Lifetime;
    public Sprite Sprite;

    //todo  description 
}
