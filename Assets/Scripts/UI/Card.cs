using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Card : MonoBehaviour
{
    [SerializeField] private Image _cardImage;
    [SerializeField] private TMP_Text _cardNameText;
    [SerializeField] private TMP_Text _cardDescriptionText;

    private BaseSkillUpCardSO _baseSkillUpCardSO;
    private ProgressSystem _progressSystem;

    public void FillCard(BaseSkillUpCardSO baseSkillUpCardSO, ProgressSystem progressSystem)
    {
        _baseSkillUpCardSO = baseSkillUpCardSO;
        _progressSystem = progressSystem;

        _cardImage.sprite = baseSkillUpCardSO.Sprite;
        _cardNameText.text = baseSkillUpCardSO.Name;
        _cardDescriptionText.text = baseSkillUpCardSO.Description;
    }

    public void SelectСard()
    {
        if (_baseSkillUpCardSO.ActiveSkill != ActiveSkill.No)
        {
            //_progressSystem.SkillUp(_baseSkillUpCardSO.ActiveSkill, _baseSkillUpCardSO.IsTEmporary, GetActiveStat(),
            //    _baseSkillUpCardSO.Value, _baseSkillUpCardSO.Modifier, _baseSkillUpCardSO.Lifetime, _baseSkillUpCardSO.Price);
        }
        else if(_baseSkillUpCardSO.PassiveSkill != PassiveSkill.No)
        {

        }
    }

    //private Stat GetActiveStat()
    //{
    //    Stat stat;
    //    switch (_baseSkillUpCardSO.ActiveSkill)
    //    {
    //        case ActiveSkill.No:
    //            break;
    //        case ActiveSkill.BulletSpeed:
                
    //            break;
    //        case ActiveSkill.CoolDown:
    //            break;
    //        case ActiveSkill.DamageDealt:
    //            break;
    //        case ActiveSkill.Distance:
    //            break;
    //        case ActiveSkill.SpreadAngle:
    //            break;
    //        case ActiveSkill.BulletsNumber:
    //            break;
    //        case ActiveSkill.DamageArea:
    //            break;
    //        default:
    //            break;
    //    }
    //    return stat;
    //}
}
