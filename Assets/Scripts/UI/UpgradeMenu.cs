using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeMenu : MonoBehaviour
{
    [SerializeField] private List<GradeSO> _gradesSO;
    [SerializeField] private TMP_Dropdown _dropdown;
    [SerializeField] private HUD _hud;
    [SerializeField] private TMP_Text _moneyText;
    [SerializeField] private Image _card1Image;
    [SerializeField] private Image _card2Image;
    [SerializeField] private TMP_Text _card1Text;
    [SerializeField] private TMP_Text _card2Text;

    private ActiveSkillUpCardSO _randomCard1;
    private PassiveSkillUpCardSO _randomCard2;

    //private readonly Dictionary<string, GradeSO> _gradesDic = new();

    public void ShowRandomCards(ActiveSkillUpCardSO[] activeCards, PassiveSkillUpCardSO[] passiveCards)
    {
        int r = Random.Range(0, activeCards.Length);
        _randomCard1 = activeCards[r];
        r = Random.Range(0, passiveCards.Length);
        _randomCard2 = passiveCards[r];

        _card1Image.sprite = _randomCard1.Sprite;
        _card1Text.text = _randomCard1.Name;
        _card2Image.sprite = _randomCard2.Sprite;
        _card2Text.text = _randomCard2.Name;
    }

    public void OnSelectedCard1()
    {
       
    }

    public void OnSelectedCard2()
    {

    }

    private void SelectedCard(BaseCharacteristics characteristic)
    {
        
    }

    private void Start()
    {
        //_dropdown.ClearOptions();

        //foreach (var gradeSO in _gradesSO)
        //{
        //    _gradesDic.Add(gradeSO._name, gradeSO);
        //    _dropdown.options.Add((new TMP_Dropdown.OptionData() { text = gradeSO._name }));
        //}

        _hud.MoneySystem.MoneyChange += MoneyChange;

        

    }

    private void MoneyChange(float value) => 
        _moneyText.text = value.ToString();


    //public void GradeSelected() =>
    //    _hud.ProgressSystem.AcceptGrade(_gradesDic[_dropdown.options[_dropdown.value].text]);
}
