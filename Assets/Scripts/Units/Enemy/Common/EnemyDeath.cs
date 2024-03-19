using System;
using UnityEngine;

public class EnemyDeath : MonoBehaviour
{
    [SerializeField] private Enemy _enemy;

    public event Action<int> OnDead; 
    public bool IsDead { get; private set; } //to block movement and attacks in other classes.
                                             
    public void Death()
    {
        if (IsDead) return; //we don't do anything if he's already dead

        Debug.Log("Hitted object dead");
        IsDead = true;

        OnDead?.Invoke(_enemy.XpForKilling);
    }
}
