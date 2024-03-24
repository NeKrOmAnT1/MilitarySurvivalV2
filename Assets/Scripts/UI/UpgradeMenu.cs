using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UpgradeMenu : MonoBehaviour
{
    [SerializeField] private List<GradeSO> _gradesSO;
    [SerializeField] private TMP_Dropdown _dropdown;
    [SerializeField] private HUD _hud;

    private Dictionary<string, GradeSO> _gradesDic = new();
    private ProgressSystem _progressSystem;

    private void OnEnable()
    {
        Debug.Log(_dropdown.interactable);
    }

    private void Start()
    {
        _progressSystem = _hud.ProgressSystem;
        _dropdown.ClearOptions();

        foreach (var gradeSO in _gradesSO)
        {
            _gradesDic.Add(gradeSO._name, gradeSO);
            _dropdown.options.Add((new TMP_Dropdown.OptionData() { text = gradeSO._name }));
        }
    }

    public void GradeSelected()
    {
        _progressSystem.AcceptGrade(_gradesDic[_dropdown.options[_dropdown.value].text]);
        Debug.Log(_gradesDic[_dropdown.options[_dropdown.value].text]);
    }
}
