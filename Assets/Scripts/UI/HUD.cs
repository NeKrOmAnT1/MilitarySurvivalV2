using System;
using UnityEngine;
using Zenject;

public class HUD : MonoBehaviour
{
    [SerializeField] private ResourceBarUI _hpBarUI;
    [SerializeField] private ResourceBarUI _xpBarUI;
    [SerializeField] private GameObject _upgradeMenu;

    private Player _player;
    private XpSystem _xpSystem;

    public ProgressSystem ProgressSystem { get; private set; }


    [Inject]
    private void Construct(Player player, ProgressSystem progressSystem, XpSystem xpSystem)
    {
        _player = player;
        ProgressSystem = progressSystem;
        _xpSystem = xpSystem;

        _upgradeMenu.SetActive(false);
        _player.PlayerHealth.OnHealthChangedE += UpdateHPValue;
        _xpSystem.ChangeXPE += UpdateXPValue;
        ProgressSystem.EnableUpgradeMenuE += EnableUpgradeMenu;
        ProgressSystem.DisableUpgradeMenuE += DisableUpgradeMenu;
    }


    private void UpdateHPValue(float current, float max) =>
        _hpBarUI.SetBarAmount(current / max);

    public void UpdateXPValue(int current, int max) =>
        _xpBarUI.SetBarAmount(current / max);

    public void EnableUpgradeMenu() => 
        _upgradeMenu.SetActive(true);

    private void DisableUpgradeMenu() => 
        _upgradeMenu.SetActive(false);
}
