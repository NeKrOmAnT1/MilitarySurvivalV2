using System.Collections.Generic;
using UnityEngine;
using Zenject;

[RequireComponent((typeof(CharacterController)), typeof(PlayerMove))]
[RequireComponent(typeof(PlayerAttack))]
public class Player : MonoBehaviour, IDamagable //Leadership class. Creates and adds others for the Player
{
    public PlayerHealth PlayerHealth { get; private set; }
    public SideType SideType { get; set; }

    [SerializeField] private GameObject squadPrefab;

    public PlayerCharacteristics PlayerCharacteristics { get; private set; }
    public WeaponCharacteristics WeaponCharacteristics { get; private set; }


    private readonly Dictionary<string, WeaponCharacteristics> _weapons = new();

    private DiContainer _diContainer;

    [Inject]
    private void Construct(PlayerCharacteristics playerCharacteristics, WeaponCharacteristics[] weaponCharacteristics, 
        PlayersWeaponSelection weaponSelection, DiContainer diContainer)
    {
        _diContainer = diContainer;

        PlayerCharacteristics = playerCharacteristics;

        foreach (var weapon in weaponCharacteristics)
        {
            _weapons.Add(weapon.Name, weapon);
        }
        
        WeaponCharacteristics = _weapons[weaponSelection.CurrentWeaponName];
    }


    public void Awake()
    {
        SideType = SideType.ALLY;

        PlayerHealth = new(this);


        SquadScript squadScript =
              _diContainer.InstantiatePrefabForComponent<SquadScript>
              (squadPrefab, transform.position, Quaternion.identity, null);
        squadScript.Initialization(transform);        
    }
}
