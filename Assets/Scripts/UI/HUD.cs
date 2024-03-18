using UnityEngine;
using Zenject;

public class HUD : MonoBehaviour
{
    [SerializeField] private ResourceBarUI _resourceBarUI;

    [Inject] private readonly Player _player;

    private void Start() =>
        _player.PlayerHealth.OnHealthChangedE += UpdateValue;

    private void UpdateValue(float current, float max) =>
        _resourceBarUI.SetBarAmount(current / max);
}
