using System;
using UnityEngine;

public class EnemyDeath : MonoBehaviour
{
    [SerializeField] private Enemy _enemy;
    [SerializeField] private GameObject _deathFXPrefab;

    public event Action<int> OnDeadXP; 
    public event Action<float> OnDeadMonty;
    public bool IsDead { get; private set; } //to block movement and attacks in other classes.
                                             
    public void Death()
    {
        if (IsDead) return; //we don't do anything if he's already dead

        IsDead = true;
        OnDeadXP?.Invoke(_enemy.XpForKilling);
        OnDeadMonty?.Invoke(_enemy.MoneyForKilling);

        Instantiate(_deathFXPrefab, transform.position, Quaternion.identity);
        this.gameObject.SetActive(false);
    }
}
