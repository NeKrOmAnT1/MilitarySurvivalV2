using UnityEngine;
using Zenject;

public class BootstrapInstaller : MonoInstaller
{
  
    [SerializeField] private WeaponSO[] _weaponSO;

    public override void InstallBindings()
    {
        InstallWeaponSelection();
        InstallWeapon();
    }

    private void InstallWeaponSelection() => 
        Container.Bind<WeaponSelection>().FromNew().AsSingle().NonLazy();

    private void InstallWeapon()
    {
        foreach (var weapon in _weaponSO)
        {
            Container.Bind<WeaponCharacteristics>().FromNew().AsTransient().WithArguments(weapon).NonLazy();
        }
    }
}