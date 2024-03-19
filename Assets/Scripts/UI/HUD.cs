using UnityEngine;
using Zenject;

public class HUD : MonoBehaviour
{
    [SerializeField] private ResourceBarUI _hpBarUI;
    [SerializeField] private ResourceBarUI _xpBarUI;

    [Inject] private readonly Player _player;

    private void Start() =>
        _player.PlayerHealth.OnHealthChangedE += UpdateHPValue;

    private void UpdateHPValue(float current, float max) =>
        _hpBarUI.SetBarAmount(current / max);

    public void UpdateXPValue(float current, float max) =>
        _xpBarUI.SetBarAmount(current / max);
}
