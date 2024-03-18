using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "DefaultPlayerSO", menuName = "PlayerSO")]
public class PlayerSO : ScriptableObject
{
    [field: SerializeField, Tooltip("Maximum Health Value")]
    public float Hp { get; private set; }

    [field: SerializeField]
    public float Armour { get; private set; }

    [field: SerializeField]
    public float MoveSpeed { get; private set; }

    [field: SerializeField]
    public float Luck { get; private set; }

    /// <summary>
    /// It's likely to have default SO remain unchanged
    /// </summary>
    /// <param name="temp">An PlayerSO that already exists and is configured with default values </param>
    /*public PlayerSO(PlayerSO temp)
    {
        Hp = temp.Hp;
        Armour = temp.Armour;
        MoveSpeed = temp.MoveSpeed;
        Luck = temp.Luck;
    }*/
}
