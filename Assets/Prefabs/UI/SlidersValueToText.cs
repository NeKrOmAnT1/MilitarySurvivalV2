using UnityEngine;
using UnityEngine.UI;

public class SlidersValueToText : MonoBehaviour
{
    [SerializeField]
    private Slider _slider;
    [SerializeField]
    private Text _text;

    private void Start() => 
        OnValueChange();

    public void OnValueChange() => 
        _text.text = _slider.value.ToString();
}
