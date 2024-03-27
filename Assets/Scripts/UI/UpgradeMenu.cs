using TMPro;
using UnityEngine;
using Zenject;

public class UpgradeMenu : MonoBehaviour
{
    //[SerializeField] private List<GradeSO> _gradesSO;
    //[SerializeField] private TMP_Dropdown _dropdown;
    [SerializeField] private HUD _hud;
    [SerializeField] private TMP_Text _moneyText;
    [SerializeField] private GameObject _UpgradeMenuObj;
    [Space]
    [SerializeField] private Card[] _cardButtons;


    private BaseSkillUpCardSO[] _skillUpCards;
    //todo other cards

    [Inject]
    private void Construct(BaseSkillUpCardSO[] activeSkillUpCards)
    {
        _skillUpCards = activeSkillUpCards;

        _hud.MoneySystem.MoneyChange += MoneyChange;
    }

    public void InitRandomCards()
    {
        foreach (var cardButton in _cardButtons)
        {
            BaseSkillUpCardSO card = RandomCard();
            cardButton.FillCard(card);
        }

        _card1 = RandomCard();
        _card2 = RandomCard();
        _card3 = RandomCard();

        //toso fill cards buttone
    }

    private BaseSkillUpCardSO RandomCard() =>
        _skillUpCards[Random.Range(0, _skillUpCards.Length)];

    public void Exit()
    {
        _UpgradeMenuObj.SetActive(false);
        Time.timeScale = 1;
    }

    private void MoneyChange(float value) =>
        _moneyText.text = value.ToString();
}
