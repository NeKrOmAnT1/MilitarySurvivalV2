using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CountdownTimerUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _timerText;
    [SerializeField] private Slider _progressSlider;
    [SerializeField] private float _maxValue = 100;

    private float remainingTime;

    private void Start()
    {
        remainingTime = _maxValue;
        _progressSlider.maxValue = _maxValue;
    }

    private void Update() =>
        TimerText();

    private void TimerText()
    {
        remainingTime -= Time.deltaTime;

        int minutes = Mathf.FloorToInt(remainingTime / 60);
        int seconds = Mathf.FloorToInt(remainingTime % 60);

        _timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        _progressSlider.value = remainingTime;
    }
}
