using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "DefaultWeaponSO", menuName = "WeaponSO")]
public class WeaponSO : ScriptableObject
{
    [field: SerializeField, Tooltip("Ranged/Melee")]
    public WeaponType WeaponType { get; private set; }

    [field: SerializeField, Tooltip("Bullet speed for Ranged Type /attack speed for Melee Type")] 
    public float   AttackSpeed { get; private set; }

    [field: SerializeField, Tooltip("Ttime between attacks/shots")] 
    public float CoolDown { get; private set; }

    [field: SerializeField, Tooltip("Damage dealt to an _enemy with this weapon")] 
    public float DamageDealt { get; private set; }

    [field: SerializeField, Tooltip("The maximum distance at which a projectile can reach a _target")]
    public float Distance { get; private set; }

    [field: SerializeField, Tooltip("The angle which projectiles can deviate from the _target when fired")]
    public float SpreadAngle  { get; private set; }

    [field: SerializeField, Tooltip("Number of bullets fired simultaneously. For some weapons such as shotguns")]
    public float BulletsNumber { get; private set; }


    [field: Space, SerializeField, Tooltip("For explosive weapons")]
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
