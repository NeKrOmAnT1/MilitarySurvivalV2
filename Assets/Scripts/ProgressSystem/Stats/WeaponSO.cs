using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "DefaultWeaponSO", menuName = "WeaponSO")]
public class WeaponSO : ScriptableObject
{
    [field: SerializeField, Tooltip("Ranged/Melee")]
    public WeaponType WeaponType { get; private set; }

    [field: SerializeField, Tooltip("Bullet speed for Ranged Type /Attack speed for Melee Type")] 
    public float   BulletSpeed { get; private set; }

    [field: SerializeField, Tooltip("Time between attacks/shots in seconds")] 
    public float CoolDown { get; private set; }

    [field: SerializeField, Tooltip("Damage dealt to an _enemy with this weapon")] 
    public float DamageDealt { get; private set; }

    [field: SerializeField, Tooltip("Melee only: Distance of attack from player")]
    public float Distance { get; private set; }

    [field: SerializeField, Tooltip("The angle which projectiles can deviate from the _target when fired")]
    [field: Range(0, 360)]
    public float SpreadAngle  { get; private set; }

    [field: SerializeField, Tooltip("Number of bullets fired simultaneously. For some weapons such as shotguns")]
    public float BulletsNumber { get; private set; }


    [field: Space, SerializeField, Tooltip("Ranged: Area of explosive projectiles / Melee: Radius of attack")]
    public float DamageArea { get; private set; }

    /// <summary>
    /// It's likely to have default SO remain unchanged
    /// </summary>
    /// <param name="temp">An WeaponSO that already exists and is configured with default values </param>
    /*public WeaponSO(WeaponSO temp)
    {
        AttackSpeed = temp.AttackSpeed;
        CoolDown = temp.CoolDown;
        DamageDealt = temp.DamageDealt;
        Distance = temp.Distance;
        SpreadAngle = temp.SpreadAngle;
        BulletsNumber = temp.BulletsNumber;

        DamageArea = temp.DamageArea;
    }*/
}


public enum WeaponType
{
    Ranged,
    Melee
}
