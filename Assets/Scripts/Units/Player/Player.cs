using UnityEngine;

[RequireComponent((typeof(CharacterController)), typeof(PlayerMove))]
[RequireComponent(typeof(PlayerAttack))]
public class Player : MonoBehaviour, IDamagable //Leadership class. Creates and adds others for the Player
{
    public PlayerHealth PlayerHealth { get; private set; }
    public SideType SideType { get; set; }

    /// <summary>
    /// Single instances of the Characteristics classes. Created here.
    /// Used by other Player parts, modified by ProgressSystem.
    /// </summary>
    public PlayerCharacteristics PlayerCharacteristics { get; private set; }
    public WeaponCharacteristics WeaponCharacteristics { get; private set; }

    /// <summary>
    /// Link to pre-created and configured ScriptableObjects with default characteristics.
    /// </summary>
    private const string playerSOPath = "SO/DefaultPlayerSO";
    private const string weaponSOPath = "SO/DefaultWeaponSO";

    public void Awake()
    {
        SideType = SideType.ALLY;

        PlayerCharacteristics = new(Resources.Load<PlayerSO>(playerSOPath));
        WeaponCharacteristics = new(Resources.Load<WeaponSO>(weaponSOPath));

        PlayerHealth = new(this);
    }
}
