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

    [Inject]
    private void Construct(PlayerCharacteristics playerCharacteristics, WeaponCharacteristics[] weaponCharacteristics, 
        WeaponSelection weaponSelection)
    {
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

        Instantiate(squadPrefab).GetComponent<SquadScript>()
        .Initialization(transform);
    }
}
