using System;
using UnityEngine;
using Zenject;

public class BootstrapInstaller : MonoInstaller
{
    [SerializeField] private WeaponSO[] _weaponSO;
    [SerializeField] private BaseSkillUpCardSO[] _badeSkillUpCardSO;

    public override void InstallBindings()
    {
        InstallWeaponSelection();
        InstallWeapon();

        InstallCards();
    }
       

    private void InstallCards()
    {
        foreach (var skill in _badeSkillUpCardSO)
        {
            Container.Bind<BaseSkillUpCardSO>().FromInstance(skill).AsCached();
        }

        //todo InstallOtherCards
    }

    private void InstallWeaponSelection() =>
        Container.Bind<WeaponSelection>().FromNew().AsSingle().NonLazy();

    private void InstallWeapon()
    {
        foreach (var weapon in _weaponSO)
        {
            Container.Bind<WeaponCharacteristics>().FromNew().AsCached().WithArguments(weapon);
        }
    }
}