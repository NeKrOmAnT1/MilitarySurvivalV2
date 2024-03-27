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
        if (_baseSkillUpCardSO as ActiveSkillUpCardSO)
        {

        }
        else
        {

        }
    }
}
