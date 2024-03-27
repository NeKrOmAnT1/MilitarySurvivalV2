using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeMenu : MonoBehaviour
{
    //[SerializeField] private List<GradeSO> _gradesSO;
    //[SerializeField] private TMP_Dropdown _dropdown;
    [SerializeField] private HUD _hud;
    [SerializeField] private TMP_Text _moneyText;
    [SerializeField] private GameObject _UpgradeMenuObj;
    //[Space]
    //[SerializeField] private Image _card1Image;
    //[SerializeField] private Image _card2Image; 
    //[SerializeField] private Image _card3Image;
    //[Space]
    //[SerializeField] private TMP_Text _card1Text;
    //[SerializeField] private TMP_Text _card2Text;
    //[SerializeField] private TMP_Text _card3Text;

    private void Start() => 
        _hud.MoneySystem.MoneyChange += MoneyChange;

    public void Exit()
    {
        _UpgradeMenuObj.SetActive(false);
        Time.timeScale = 1;
    }

    private void MoneyChange(float value) => 
        _moneyText.text = value.ToString();
}
