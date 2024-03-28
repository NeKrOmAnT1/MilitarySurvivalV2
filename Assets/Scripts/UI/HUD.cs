using TMPro;
using UnityEngine;
using Zenject;

public class HUD : MonoBehaviour
{
    [SerializeField] private ResourceBarUI _hpBarUI;
    [SerializeField] private ResourceBarUI _xpBarUI;
    [SerializeField] private GameObject _upgradeMenuObj;
    [SerializeField] private UpgradeMenu _upgradeMenu;
    [SerializeField] private TMP_Text _xpLevelText;

    private Player _player;
    private XpSystem _xpSystem;

    public ProgressSystem ProgressSystem { get; private set; }
    public MoneySystem MoneySystem { get; private set; }
    public ActiveSkillUpCardSO[] ActiveSkillUpCards { get; private set; }
    public PassiveSkillUpCardSO[] PassiveSkillUpCards { get; private set; }


    [Inject]
    private void Construct(Player player, ProgressSystem progressSystem, XpSystem xpSystem, MoneySystem moneySystem)
    {
        _player = player;
        ProgressSystem = progressSystem;
        _xpSystem = xpSystem;
        MoneySystem = moneySystem;

        _upgradeMenuObj.SetActive(false);
        _player.PlayerHealth.OnHealthChangedE += UpdateHPValue;
        _xpSystem.ChangeXPE += UpdateXPValue;
        ProgressSystem.EnableUpgradeMenuE += EnableUpgradeMenu;
        ProgressSystem.DisableUpgradeMenuE += DisableUpgradeMenu;

        UpdateXPValue();
    }


    private void UpdateHPValue(float current, float max) =>
        _hpBarUI.SetBarAmount(current / max);

    public void UpdateXPValue()
    {
        _xpBarUI.SetBarAmount(_xpSystem.CurrentXp / _xpSystem.TargetXp);
        _xpLevelText.text = _xpSystem.XpLevel.ToString();
    }

    public void EnableUpgradeMenu()
    {
        _upgradeMenuObj.SetActive(true);
        //_upgradeMenu.Enable();
    }

    private void DisableUpgradeMenu() =>
        _upgradeMenuObj.SetActive(false);
}
