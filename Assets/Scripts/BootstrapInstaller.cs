using System;
using UnityEngine;
using Zenject;

public class BootstrapInstaller : MonoInstaller
{
    [SerializeField] private WeaponSO[] _weaponSO;
    [SerializeField] private ActiveSkillUpCardSO[] _activeSkillUpCardSO;
    [SerializeField] private PassiveSkillUpCardSO[] _passiveSkillUpCardSO;

    public override void InstallBindings()
    {
        InstallWeaponSelection();
        InstallWeapon();
        InstallActiveCards();
        InstallPassiveCards();
    }

    private void InstallPassiveCards()
    {
        foreach (var skill in _passiveSkillUpCardSO)
        {
            Container.Bind<PassiveSkillUpCardSO>().FromInstance(skill).AsCached();
        }
    }

    private void InstallActiveCards()
    {
        foreach (var skill in _activeSkillUpCardSO)
        {
            Container.Bind<ActiveSkillUpCardSO>().FromInstance(skill).AsCached();
        }
    }

    private void InstallWeaponSelection()
    {
        Container.Bind<PlayersWeaponSelection>().FromNew().AsSingle().NonLazy();
        Container.Bind<AllysWeaponSelection>().FromNew().AsSingle().NonLazy();
    }

    private void InstallWeapon()
    {
        foreach (var weapon in _weaponSO)
        {
            Container.Bind<WeaponCharacteristics>().FromNew().AsCached().WithArguments(weapon);
        }
    }
}