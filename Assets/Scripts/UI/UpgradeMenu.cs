using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UpgradeMenu : MonoBehaviour
{
    [SerializeField] private List<GradeSO> _gradesSO;
    [SerializeField] private TMP_Dropdown _dropdown;
    [SerializeField] private HUD _hud;
    [SerializeField] private TMP_Text _moneyText;

    private readonly Dictionary<string, GradeSO> _gradesDic = new();
    private ProgressSystem _progressSystem;
    private MoneySystem _moneySystem;

    private void Start()
    {
        _progressSystem = _hud.ProgressSystem;
        _moneySystem = _hud.MoneySystem;
        _dropdown.ClearOptions();

        foreach (var gradeSO in _gradesSO)
        {
            _gradesDic.Add(gradeSO._name, gradeSO);
            _dropdown.options.Add((new TMP_Dropdown.OptionData() { text = gradeSO._name }));
        }

        _moneySystem.MoneyChange += MoneyChange;
    }

    private void MoneyChange(int value) => 
        _moneyText.text = value.ToString();


    public void GradeSelected() => 
        _progressSystem.AcceptGrade(_gradesDic[_dropdown.options[_dropdown.value].text]);
}
