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
}
